using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Models.DTOs
{
    public class CreateAppUserDTO
    {
        [Required(ErrorMessage = "isim boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazılmış olmalı.")]
        [EmailAddress]

        public string Mail { get; set; }

        [Required(ErrorMessage ="isim boş bırakılamaz")]
        [MinLength(3,ErrorMessage ="en az 3 karakter yazılmış olmalı.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soy isim boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazılmış olmalı.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Kullanici adı boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazılmış olmalı.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Şifre adı boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazılmış olmalı.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Image { get; set; }  // fotograflar için

        [NotMapped]  // configure ederken veritabanı tarafında kolon olarak oluşturma.
        public IFormFile ImagePath { get; set; }


    }
}
