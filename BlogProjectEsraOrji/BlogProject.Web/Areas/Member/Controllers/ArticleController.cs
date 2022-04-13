using AutoMapper;
using BlogProject.Infrastructure.Context;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.DTOs;
using BlogProject.Web.Areas.Member.Models.ViewEntity;
using BlogProject.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;
        private readonly IAppUserRepository appUserRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ICategoryRepository categoryRepository;
        private readonly AppDbContext appDbContext;
        private readonly ICommentRepository commentRepository;
        private readonly ILikeRepository likeRepository;

        public ArticleController(IArticleRepository articleRepository, IMapper mapper, IAppUserRepository appUserRepository, UserManager<IdentityUser> userManager, ICategoryRepository categoryRepository, AppDbContext appDbContext, ICommentRepository commentRepository, ILikeRepository likeRepository)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
            this.appUserRepository = appUserRepository;
            this.userManager = userManager;
            this.categoryRepository = categoryRepository;
            this.appDbContext = appDbContext;
            this.commentRepository = commentRepository;
            this.likeRepository = likeRepository;
        }

        public async Task<IActionResult> Create()
        {
            IdentityUser ıdentityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);


            CreateArticleDTO model = new CreateArticleDTO()
            {
                Categories = categoryRepository.GetByDefaults
                 (
                     selector: a => new GetCategoryVM
                     {
                         Id = a.Id,
                         Name = a.Name,
                         Description = a.Description
                     },
                     expression: a => a.Statu != Model.Enums.Statu.Passive
                 ),
                AppUserId = appUser.Id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateArticleDTO model,List<ArticleCategoryVM> articleCategoryVMs)
        {

            if (ModelState.IsValid)
            {

                var article = mapper.Map<Article>(model);

                article.IsActive = true;

                if (article.ImagePath != null) // fotograf geldiyse
                {
                    using var image = Image.Load(model.ImagePath.OpenReadStream());
                    image.Mutate(a => a.Resize(256, 256));
                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");
                    article.Image = ($"/images/{guid}.jpg");

                    articleRepository.Create(article);

                    List<ArticleCategoryVM> selecteds = articleCategoryVMs.Where(a => a.IsSelected).ToList<ArticleCategoryVM>();
                    //article.ArticleCategories = new List<ArticleCategory>();
                    
                    foreach (ArticleCategoryVM item in selecteds)
                    {
                        ArticleCategory articleCategory;                      
                        articleCategory = new ArticleCategory();
                        articleCategory.ArticleID = article.Id;
                        articleCategory.Article = article;
                        articleCategory.CategoryID = item.CategoryId;
                        articleCategory.Category = categoryRepository.GetDefault(a => a.Id == item.CategoryId);
                     
                        //categoryRepository.Create(articleCategory); yeni repoo yaz

                        article.ArticleCategories.Add(articleCategory);

                         articleRepository.Update(article);

                        //bunu tablolarda da oluşturman lazım

                    }
                 
                    return RedirectToAction("List");
                }                

            }
            return View(model);
        }



        public async Task<IActionResult> List()
        {
            IdentityUser ıdentityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);

            // makalenin tüm proplarını göstermek istemediğiden bazı proplarını alabilmek için GetArticleVM oluşturalım.

            var articleList = articleRepository.GetByDefaults
             (
                selector: a => new GetArticleVM()
                {
                    Id = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    Image = a.Image,
                    //CategoryName = a.Category.Name,
                    AuthorName = a.AppUser.FullName
                },
                expression: a => a.Statu != Model.Enums.Statu.Passive && a.AppUserId == appUser.Id,
                include: a => a.Include(a => a.AppUser)/*.Include(a => a.Category)*/

              );

            return View(articleList);

        }

        public async Task<IActionResult> Detail(int id)
        {
            IdentityUser ıdentityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);            

            var article = articleRepository.GetByDefault(
                 selector: a => new GetArticleDetailVM()
                 {
                     ArticleId = a.Id,
                     Title = a.Title,
                     Content = a.Content,
                     Image = a.Image,
                     AuthorName = a.AppUser.FullName,
                     AuthorImage = a.AppUser.Image,
                     CreateDate = a.CreateDate,
                     View = a.viewCount,
                     minute = articleRepository.ArticleTime(id)
                     
                 },
                 expression: a => a.Id == id,
                 include: a => a.Include(b => b.AppUser));
            TempData["makaleid"] = id;
            TempData["kullaniciid"] = appUser.Id;

            var makale = articleRepository.GetDefault(a => a.Id == id);
            makale.viewCount += 1;
            articleRepository.Update(makale);

            return View(article);

        }


        [HttpPost]
        public async Task<IActionResult> Detail(int id, string comment)
        {
            //var yorumum = TempData["yorum"];
            IdentityUser ıdentityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);
            CreateCommentDTO model = new CreateCommentDTO()
            {
                ArticleId = id,
                AppUserId = appUser.Id,
                Text = comment,
                IsActive = true
                
            };
            
            var model2 = model;
            var mapleme = mapper.Map<Comment>(model2);
            commentRepository.Create(mapleme);

            return RedirectToAction("List");
        }


        public IActionResult Update(int id)
        {
            Article article = articleRepository.GetDefault(a => a.Id == id);
            var articleObject = mapper.Map<UpdateArticleDTO>(article);
            articleObject.Categories = categoryRepository.GetByDefaults
                 (
                     selector: a => new GetCategoryVM
                     {
                         Id = a.Id,
                         Name = a.Name,
                         Description = a.Description
                     },
                     expression: a => a.Statu != Model.Enums.Statu.Passive, null, null
                 );
            return View(articleObject);

        }


        [HttpPost]
        public IActionResult Update(UpdateArticleDTO model)
        {
            if (ModelState.IsValid)
            {
                var article = mapper.Map<Article>(model);
                article.IsActive = true;
                if (article.ImagePath != null)
                {
                    using var image = Image.Load(model.ImagePath.OpenReadStream());
                    image.Mutate(a => a.Resize(256, 256));
                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");
                    article.Image = ($"/images/{guid}.jpg");
                    articleRepository.Update(article);
                    return RedirectToAction("List");
                }
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            Article article = articleRepository.GetDefault(a => a.Id == id);
            articleRepository.Delete(article);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Like(int id)
        {

            IdentityUser ıdentityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);
            TempData["makaleid"] = id;
            TempData["kullaniciid"] = appUser.Id;
            CreateLikeDTO model = new CreateLikeDTO()
            {
                AppUserId = appUser.Id,
                ArticleId = id
            };
            var begeni = mapper.Map<Like>(model);
            likeRepository.Create(begeni);
            return RedirectToAction("List");
        }

    }
}
