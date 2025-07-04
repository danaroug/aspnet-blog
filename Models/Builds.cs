using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarbandOfTheSpiritborn.Models
{
    public class Builds
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Profession is required")]
        public string Profession { get; set; }
        public string BuildName { get; set; }
        public string ShortDescription { get; set; }
        public string BuildAuthor { get; set; }
        public string Item { get; set; }
        public string Stat { get; set; }
        public string WeaponSet { get; set; }
        public string OtherItems { get; set; }
        public string Rotation { get; set; }
        public string MainSkills { get; set; }
        public string SecondarySkills { get; set; }

        public DateTime BuildDate { get; set; }

        public Builds()
        {
            BuildDate = DateTime.UtcNow;
        }
    }
}
