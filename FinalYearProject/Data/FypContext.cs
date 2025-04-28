using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalYearProject.Models;

namespace FinalYearProject.Data
{
    public class FypContext : DbContext
    {
        public FypContext(DbContextOptions<FypContext> options)
            : base(options)
        {
        }

        public DbSet<FinalYearProject.Models.Bake> Bake { get; set; } = default!;
        public DbSet<FinalYearProject.Models.Review> Review { get; set; } = default!;
        public DbSet<FinalYearProject.Models.Basket> Basket { get; set; } = default!;
        public DbSet<FinalYearProject.Models.User> User { get; set; } = default!;
        public DbSet<FinalYearProject.Models.Order> Order { get; set; } = default!;
        public DbSet<FinalYearProject.Models.DeliveryInformation> DeliveryInformation { get; set; } = default!;
    }
}
