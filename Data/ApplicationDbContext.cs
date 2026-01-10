using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WarbandOfTheSpiritborn.Models;
using WarbandOfTheSpiritborn.Services;

namespace WarbandOfTheSpiritborn.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WarbandOfTheSpiritborn.Models.Events> Events { get; set; }
        public DbSet<WarbandOfTheSpiritborn.Models.Blog> Blog { get; set; }
        public DbSet<WarbandOfTheSpiritborn.Models.About> About { get; set; }
        public DbSet<WarbandOfTheSpiritborn.Models.Gallery> Gallery { get; set; }
        public DbSet<WarbandOfTheSpiritborn.Models.Builds> Builds { get; set; }
    }
}
