using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarbandOfTheSpiritborn.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please choose an image")]
        public string Picture { get; set; }
        public Gallery()
        {

        }
    }
}
