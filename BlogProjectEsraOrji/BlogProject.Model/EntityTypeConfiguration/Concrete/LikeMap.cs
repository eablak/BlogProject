using BlogProject.Model.Entities.Concrete;
using BlogProject.Model.EntityTypeConfiguration.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.EntityTypeConfiguration.Concrete
{
   public  class LikeMap : IEntityTypeConfiguration<Like>
    {
        public  void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(a=> new { a.AppUserId,a.ArticleId });  
            // primary ve foreign key gibi not null alanlar.
            
        }
    }
}
