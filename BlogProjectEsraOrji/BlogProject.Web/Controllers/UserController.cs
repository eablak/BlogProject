using AutoMapper;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAppUserRepository appUserRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        private readonly IPasswordRepository passwordRepository;

        public UserController(IAppUserRepository appUserRepository,UserManager<IdentityUser> userManager,IMapper mapper, IPasswordRepository passwordRepository )
        {
            this.appUserRepository = appUserRepository;
            this.userManager = userManager;
            this.mapper = mapper;
            this.passwordRepository = passwordRepository;
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task< IActionResult> Create(CreateAppUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var NewId = Guid.NewGuid().ToString();
                IdentityUser identityUser = new IdentityUser { Email=model.Mail, UserName=model.UserName, Id=NewId };
                IdentityResult result = await userManager.CreateAsync(identityUser, model.Password);
                if (result.Succeeded)  // create etme basarılı ise
                {
                   
                    await userManager.AddToRoleAsync(identityUser,"Member");

                    var user = mapper.Map<AppUser>(model);
                    user.IdentityId = NewId;
                    user.Role = Model.Enums.Role.Member;
                    user.IsActive = false;
                    if (model.ImagePath!=null)
                    {
                        using var image = Image.Load(model.ImagePath.OpenReadStream());
                        image.Mutate(a=>a.Resize(256,256));  // mutate: degirtirmek demek - foto yeniden sekillensin demis olduk.
                        image.Save($"wwwroot/images/{user.UserName}.jpg");
                        user.Image= ($"/images/{user.UserName}.jpg"); // veritabaı tarafında dosya yolunu tutucak.                       

                        appUserRepository.Create(user); // veritabanı tarafında kullanıcı olussun

                        Password pp = new Password();
                        pp.UserId = user.Id;
                        pp.Text = model.Password;
                        pp.CreateDate = DateTime.Now;
                        passwordRepository.Create(pp); // şifreyi password tablosuna kaydeder

                        Password password = passwordRepository.GetDefault(a => a.Id == pp.Id);
                        password.UserPasswords.Add(new UserPassword() { AppUser = user, AppUserID = user.Id, Password = pp, PasswordID = pp.Id });
                        passwordRepository.Update(password); // user-password tablo ilişkisi
                        user.Passwords.Add(pp);

                        return Redirect("~/login"); // parametrede string url ( adresini bekler)
                    }


                }


            }
            return View(model);  // kullanıcı olusamadıysa , validasyonlar gecersizse ..
        }



    }
}
