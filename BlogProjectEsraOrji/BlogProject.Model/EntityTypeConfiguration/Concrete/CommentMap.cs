using BlogProject.Model.Entities.Concrete;
using BlogProject.Model.EntityTypeConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.EntityTypeConfiguration.Concrete
{
    public class CommentMap :BaseMap<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(a=>a.Text).HasMaxLength(300).IsRequired(true);

            builder.HasKey(a=> new { a.AppUserId,a.ArticleId });

            base.Configure(builder);
        }
    }
}
