using AutoMapper;
using BlogProject.Infrastructure.Context;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.DTOs;
using BlogProject.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Controllers
{
    [Area("Member")]

    public class AppUserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAppUserRepository appUserRepository;
        private readonly IMapper mapper;
        private readonly IPasswordRepository passwordRepository;

        public AppUserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAppUserRepository appUserRepository, IMapper mapper, IPasswordRepository passwordRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appUserRepository = appUserRepository;
            this.mapper = mapper;
            this.passwordRepository = passwordRepository;
        }

        public async Task<IActionResult> Index()
        {
            IdentityUser ıdentityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);
            if (appUser != null)
            {
                return View(appUser);
            }
            return Redirect("~/");  // areasız mvc projesinin home/indexine götürür.
        }

        public async Task<IActionResult> LogOut()
        {

            await signInManager.SignOutAsync();

            return Redirect("~/");  // aresasız home indexe yönlendiriyor
        }

        public async Task<IActionResult> Account()
        {
            IdentityUser ıdentityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);
            if (appUser != null)
            {
                return View(appUser);
            }
            return Redirect("~/");
        }



        [HttpGet]
        public async Task<IActionResult> Update()
        {
            IdentityUser ıdentityUser = await userManager.GetUserAsync(User);
            ıdentityUser.Email = await userManager.GetEmailAsync(ıdentityUser);

            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);

            UpdateProfileDTO myprofile = mapper.Map<UpdateProfileDTO>(appUser);

            return View(myprofile);
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateProfileDTO model)

        {
            IdentityUser user = await userManager.GetUserAsync(User);
            
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user,model.Password); // PasswordHash değişikliği!!
            
            if (ModelState.IsValid)
            {
                AppUser profil = mapper.Map<AppUser>(model);
                profil.IsActive = true;
                profil.Role = Model.Enums.Role.Member;
                if (profil.ImagePath != null)
                {
                    using var image = Image.Load(model.ImagePath.OpenReadStream());
                    image.Mutate(a => a.Resize(256, 256));
                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");
                    profil.Image = ($"/images/{guid}.jpg");

                    Password pp = new Password();
                    pp.UserId = model.Id;
                    pp.Text = model.Password;
                    pp.CreateDate = DateTime.Now;

                    
                        if (passwordRepository.PasswordText(model.Id).Contains(model.Password))
                        {
                            return View(model);
                        }
                        else
                        {
                            appUserRepository.Update(profil); // kişiyi app-user tablosuna kaydeder
                            
                            passwordRepository.Create(pp); // şifreyi password tablosuna kaydeder

                            Password password = passwordRepository.GetDefault(a => a.Id == pp.Id);
                            password.UserPasswords.Add(new UserPassword() { AppUser = profil, AppUserID = profil.Id, Password = pp, PasswordID = pp.Id });
                            passwordRepository.Update(password); // user-password tablo ilişkisi
                        }
                    
               
                    return RedirectToAction("Account"); //hocanın dediğinden önce böyleydi
                }

                // Id'den yakalayabilsin diye UpdateProfileDTO  Id prop'u ekledim - IdentityId!

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserPassive()
        {
            IdentityUser ıdentityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);

            if (appUser != null)
            {
                appUser.Statu = Model.Enums.Statu.Passive;
                appUserRepository.Update(appUser);
            }
            return Redirect("~/");
        }

        public IActionResult HomePage()
        {
            return RedirectToAction("Index");
        }

    }
}
