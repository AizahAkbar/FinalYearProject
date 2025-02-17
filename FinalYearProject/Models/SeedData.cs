using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FinalYearProject.Data;
using System;
using System.Linq;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace FinalYearProject.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BakesContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<BakesContext>>()))
        {
            // Look for any movies.
            if (context.Bake.Any())
            {
                return;   // DB has been seeded
            }
            context.Bake.AddRange(
                new Bake
                {
                    Name = "Cosmic Brownies",
                    Category = "Brownies",
                    Description = "Fudgy, gooey brownie centre topped with a layer of chocolate ganache and rainbow chocolate sprinkles",
                    Price = 7.5,
                    Rating = 4.0
                },
                new Bake
                {
                    Name = "Chocolate Chip Cookies",
                    Category = "Cookies",
                    Description = "Crispy golden cookies loaded with rich chocolate chips, perfect for any occasion",
                    Price = 4.0,
                    Rating = 4.5
                },
                new Bake
                {
                    Name = "Lemon Cheesecake",
                    Category = "Cheesecake",
                    Description = "Creamy cheesecake with a tangy lemon flavor on a buttery graham cracker crust",
                    Price = 9.0,
                    Rating = 4.8
                },
                new Bake
                {
                    Name = "Apple Pie",
                    Category = "Pies",
                    Description = "Homemade pie filled with spiced apples and wrapped in a flaky, buttery crust",
                    Price = 6.0,
                    Rating = 4.3
                }
            );
            context.SaveChanges();
        }
    }
}