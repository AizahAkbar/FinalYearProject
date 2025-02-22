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
        using (var context = new FypContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<FypContext>>()))
        {
            // Look for any bakes.
            if (!context.Bake.Any())
            {
                context.Bake.AddRange(
                    new Bake
                    {
                        Name = "Cosmic Brownies",
                        Category = "Brownies",
                        Description = "Fudgy, gooey brownie centre topped with a layer of chocolate ganache and rainbow chocolate sprinkles",
                        Price = 7.5
                    },
                    new Bake
                    {
                        Name = "Chocolate Chip Cookies",
                        Category = "Cookies",
                        Description = "Crispy golden cookies loaded with rich chocolate chips, perfect for any occasion",
                        Price = 4.0
                    },
                    new Bake
                    {
                        Name = "Lemon Cheesecake",
                        Category = "Cheesecake",
                        Description = "Creamy cheesecake with a tangy lemon flavor on a buttery graham cracker crust",
                        Price = 9.0
                    },
                    new Bake
                    {
                        Name = "Apple Pie",
                        Category = "Pies",
                        Description = "Homemade pie filled with spiced apples and wrapped in a flaky, buttery crust",
                        Price = 6.0
                    }
                );
            }
            if (!context.Review.Any())
            {
                context.Review.AddRange(
                    new Review
                    {
                        BakeId = 1,
                        User = "Aizah Akbar",
                        Description = "I took one bite and was in heaven! The texture is so fudgy and rich. Definitely will be buying again. ",
                        Rating = 5,
                        CreatedDate = DateTime.Now
                    },
                    new Review
                    {
                        BakeId = 1,
                        User = "Wajeeha Ikram",
                        Description = "My family loved these, the only thing I would take off one star for is they arrived a bit squished. Other than that they were perfect!  ",
                        Rating = 4,
                        CreatedDate = DateTime.Now
                    },
                    new Review
                    {
                        BakeId = 2,
                        User = "Sara Adam",
                        Description = "These are the best cookies I've ever had! Warm them up in the microwave for 15 seconds and they're even better",
                        Rating = 4,
                        CreatedDate = DateTime.Now
                    },
                    new Review
                    {
                        BakeId = 3,
                        User = "Qynaath Kokab",
                        Description = "The ratio of cheesecake to biscuit was perfect, I've had too many cheesecakes with too much biscuit. Loved this!",
                        Rating = 5,
                        CreatedDate = DateTime.Now
                    }
                );
            }
            context.SaveChanges();
        }
    }
}