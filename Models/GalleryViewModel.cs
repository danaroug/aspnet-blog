using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WarbandOfTheSpiritborn.Models

{
    public class GalleryViewModel
    {
        [Required(ErrorMessage = "Please choose an image")]
        [Display(Name = "Picture")]
        public IFormFile Image { get; set; }
    }
}
