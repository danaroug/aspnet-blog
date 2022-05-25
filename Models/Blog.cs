using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarbandOfTheSpiritborn.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public String BlogName { get; set; }
        public String BlogPost { get; set; }
        public String BlogAuthor { get; set; }
        public DateTime ArticleDate { get; set; }
        public Blog()
        {
            ArticleDate = DateTime.UtcNow;
        }
        public String Comment { get; set; }
        public String Reply { get; set; }
        
    }
}
