using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WarbandOfTheSpiritborn.Models
{
    public class Events
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string EventName { get; set; }
        [Display(Name = "Info")]
        public string EventInfo { get; set; }
        [Display(Name = "Time")]
        public string Time { get; set; }
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public Events()
        {

        }
    }
}
