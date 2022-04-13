using BlogProject.Model.Entities.Concrete;
using BlogProject.Model.EntityTypeConfiguration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.EntityTypeConfiguration.Concrete
{
  public class AppUserMap : BaseMap <AppUser>  // basemap geeneric type olduğu için hangi sınıfı configure ettiğimizi söyledik.
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(a=>a.FirstName).HasMaxLength(40).IsRequired(true);
            builder.Property(a=>a.LastName).HasMaxLength(40).IsRequired(true);
            builder.Property(a=>a.UserName).IsRequired(true);
            builder.Property(a=>a.Password).IsRequired(true);
            builder.Property(a=>a.Role).IsRequired(true);
            builder.Property(a=>a.Image).HasMaxLength(120).IsRequired(true);

            base.Configure(builder);
        }
    }
}
