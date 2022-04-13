using BlogProject.Model.Entities.Concrete;
using BlogProject.Model.EntityTypeConfiguration.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Article> Articles { get; set; }

        public DbSet<AppUser>  AppUsers { get; set; }

        public DbSet<Category>  Categories { get; set; }

        public DbSet<Comment>  Comments { get; set; }

        public DbSet<Like>  Likes { get; set; }

        //public DbSet<Password> Passwords { get; set; }

        public DbSet<UserFollowedCategory>  UserFollowedCategories { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }

        public DbSet<UserPassword> UserPasswords { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new LikeMap());
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserFollowedCategoryMap());
            modelBuilder.ApplyConfiguration(new IdentityRoleMap());
            //modelBuilder.ApplyConfiguration(new PasswordMap());
            modelBuilder.ApplyConfiguration(new ArticleCategoryMap());
            modelBuilder.ApplyConfiguration(new UserPasswordMap());


            //modelBuilder.Entity<AppUser>().Property(a => a.RowVersion).IsRowVersion();

            base.OnModelCreating(modelBuilder);
        }

        // navigation propertleri virtual işsaretlediğimizde lazy loading yapılır fakat birşey söylemezsek defaultta aeger loading yaptığını düşünür. 
        // lazy loading yaılmak istenirse navigation propların virtual işsaretlenmesi ve proxies nugettınn yüklenmesi ve hatta startup dosyasındada configure metotu içeriisnde lazy loading yapıldığının söylenmesi gerekir.


    }
}
