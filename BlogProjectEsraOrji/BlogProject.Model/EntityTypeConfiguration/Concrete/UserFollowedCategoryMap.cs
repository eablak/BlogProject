using BlogProject.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.EntityTypeConfiguration.Concrete
{
    public class UserFollowedCategoryMap : IEntityTypeConfiguration<UserFollowedCategory>
    {
        public void Configure(EntityTypeBuilder<UserFollowedCategory> builder)
        {
            builder.HasKey(a=> new { a.AppUserID,a.CategoryID });  // composite key

            builder.HasOne(a => a.AppUser).WithMany(a => a.UserFollowedCategories).HasForeignKey(a=>a.AppUserID);

            builder.HasOne(a => a.Category).WithMany(a => a.UserFollowedCategories).HasForeignKey(a => a.CategoryID);
        }
    }
}
