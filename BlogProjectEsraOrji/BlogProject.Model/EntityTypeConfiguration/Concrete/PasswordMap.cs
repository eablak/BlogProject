using BlogProject.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.EntityTypeConfiguration.Concrete
{
    public class PasswordMap : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> builder)
        {

            builder.HasKey(x => x.Id);
            builder.HasOne(a => a.AppUser).WithMany(a => a.Passwords).HasForeignKey(a => a.UserId);

        }
    }
}
