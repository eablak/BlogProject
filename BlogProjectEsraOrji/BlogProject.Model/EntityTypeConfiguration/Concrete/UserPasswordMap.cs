using BlogProject.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.EntityTypeConfiguration.Concrete
{
    public class UserPasswordMap : IEntityTypeConfiguration<UserPassword>
    {
        public void Configure(EntityTypeBuilder<UserPassword> builder)
        {
            builder.HasKey(a => new { a.AppUserID, a.PasswordID });  // composite key

            builder.HasOne(a => a.AppUser).WithMany(a => a.UserPasswords).HasForeignKey(a => a.AppUserID);

            builder.HasOne(a => a.Password).WithMany(a => a.UserPasswords).HasForeignKey(a => a.PasswordID);
        }
    }
}
