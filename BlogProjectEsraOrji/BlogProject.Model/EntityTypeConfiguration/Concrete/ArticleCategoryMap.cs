using BlogProject.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.EntityTypeConfiguration.Concrete
{
    public class ArticleCategoryMap : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.HasKey(a => new { a.ArticleID, a.CategoryID });
            builder.HasOne(a => a.Article).WithMany(a => a.ArticleCategories).HasForeignKey(a => a.ArticleID);
            //builder.HasOne(a => a.Category).WithMany(a => a.ArticleCategories).HasForeignKey(a => a.CategoryID);
        }
    }
}
