using System;

namespace WarbandOfTheSpiritborn.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string BlogName { get; set; }
        public string BlogPost { get; set; }
        public string BlogAuthor { get; set; }
        public DateTime ArticleDate { get; set; }
        public Blog()
        {
            ArticleDate = DateTime.UtcNow;
        }
        public string Comment { get; set; }
        public string Reply { get; set; }
        
    }
}
