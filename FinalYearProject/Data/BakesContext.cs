using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalYearProject.Models;

namespace FinalYearProject.Data
{
    public class BakesContext : DbContext
    {
        public BakesContext (DbContextOptions<BakesContext> options)
            : base(options)
        {
        }

        public DbSet<FinalYearProject.Models.Bake> Bake { get; set; } = default!;
    }
}
