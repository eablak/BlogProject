using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.EntityTypeConfiguration.Concrete
{
    public class IdentityRoleMap : IEntityTypeConfiguration<IdentityRole>
    {

        /// <summary>
        ///  SEED DATA GOREVI GORMUS OLDU. DATABASE AYAGA KALKARKEN BU BİLGİ ZATEN OLUŞMUS OLACAK.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData( new IdentityRole { Name="Member", Id=Guid.NewGuid().ToString(), NormalizedName="MEMBER" });
        }
    }
}
