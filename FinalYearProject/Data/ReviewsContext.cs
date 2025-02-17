using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalYearProject.Models;

namespace FinalYearProject.Data
{
    public class ReviewsContext : DbContext
    {
        public ReviewsContext (DbContextOptions<ReviewsContext> options)
            : base(options)
        {
        }

        public DbSet<FinalYearProject.Models.Review> Review { get; set; } = default!;
    }
}
