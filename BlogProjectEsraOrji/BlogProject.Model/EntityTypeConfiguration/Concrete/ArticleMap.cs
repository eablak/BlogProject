using BlogProject.Model.Entities.Concrete;
using BlogProject.Model.EntityTypeConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Model.EntityTypeConfiguration.Concrete
{
    public class ArticleMap : BaseMap<Article>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(a=>a.Title).HasMaxLength(150).IsRequired(true);
            builder.Property(a=>a.Content).IsRequired(true);
            builder.Property(s=>s.Image).IsRequired(true);

            builder.HasOne(a => a.AppUser).WithMany(a => a.Articles).HasForeignKey(a => a.AppUserId).OnDelete(DeleteBehavior.Restrict);
            // parent child ( ebeveyn- çocuk ilişkiis gibi düşünülebilinr. yani makale silindiğinde sıkıntı yok ama o makelelrin sahibi user ı silinmeye kalkıldığında hata verir.

            //builder.HasOne(a => a.Category).WithMany(a => a.Articles).HasForeignKey(a => a.CategoryId).OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
