using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Models.DTOs
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "bu alan boş bırakılamaz")]
        public string Name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "bu alan boş bırakılamaz")]
        public string Description { get; set; }
    }
}
