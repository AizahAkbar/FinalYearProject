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
                        Category = "Cheesecakes",
                        Description = "Creamy cheesecake with a tangy lemon flavor on a buttery graham cracker crust",
                        Price = 9.0
                    },
                    new Bake
                    {
                        Name = "Apple Pie",
                        Category = "Pies",
                        Description = "Homemade pie filled with spiced apples and wrapped in a flaky, buttery crust",
                        Price = 6.0
                    },
                    new Bake
                    {
                        Name = "Red Velvet Cake",
                        Category = "Cakes",
                        Description = "Classic red velvet cake with smooth cream cheese frosting and a rich, velvety texture",
                        Price = 12.0
                    },
                    new Bake
                    {
                        Name = "Blueberry Muffins",
                        Category = "Muffins",
                        Description = "Fluffy muffins bursting with fresh blueberries and topped with a sweet streusel crumble",
                        Price = 3.5
                    },
                    new Bake
                    {
                        Name = "Cinnamon Rolls",
                        Category = "Pastries",
                        Description = "Soft, swirled pastries filled with cinnamon-sugar and topped with vanilla glaze",
                        Price = 5.0
                    },
                    new Bake
                    {
                        Name = "Strawberry Shortcake",
                        Category = "Cakes",
                        Description = "Light vanilla sponge layered with fresh strawberries and whipped cream",
                        Price = 10.0
                    },
                    new Bake
                    {
                        Name = "Double Chocolate Brownies",
                        Category = "Brownies",
                        Description = "Rich dark chocolate brownies with chunks of premium chocolate throughout",
                        Price = 8.0
                    },
                    new Bake
                    {
                        Name = "Oatmeal Raisin Cookies",
                        Category = "Cookies",
                        Description = "Chewy oatmeal cookies packed with plump raisins and a hint of cinnamon",
                        Price = 4.0
                    },
                    new Bake
                    {
                        Name = "Carrot Cake",
                        Category = "Cakes",
                        Description = "Moist spiced cake with fresh carrots, topped with cream cheese frosting and chopped walnuts",
                        Price = 11.0
                    },
                    new Bake
                    {
                        Name = "Tiramisu",
                        Category = "Cakes",
                        Description = "Italian dessert with coffee-soaked ladyfingers layered with mascarpone cream and dusted with cocoa",
                        Price = 13.0
                    },
                    new Bake
                    {
                        Name = "Raspberry Danish",
                        Category = "Pastries",
                        Description = "Flaky pastry filled with sweet raspberry preserves and drizzled with vanilla glaze",
                        Price = 4.5
                    },
                    new Bake
                    {
                        Name = "Chocolate Eclairs",
                        Category = "Pastries",
                        Description = "Light choux pastry filled with vanilla cream and topped with dark chocolate ganache",
                        Price = 5.5
                    },
                    new Bake
                    {
                        Name = "Pecan Pie",
                        Category = "Pies",
                        Description = "Rich and gooey filling loaded with toasted pecans in a buttery crust",
                        Price = 8.5
                    },
                    new Bake
                    {
                        Name = "Cherry Cheesecake",
                        Category = "Cheesecakes",
                        Description = "Smooth vanilla cheesecake topped with sweet cherry compote",
                        Price = 9.5
                    },
                    new Bake
                    {
                        Name = "Snickerdoodle Cookies",
                        Category = "Cookies",
                        Description = "Soft and chewy cookies rolled in cinnamon sugar with a perfect crackly top",
                        Price = 4.0
                    },
                    new Bake
                    {
                        Name = "Banana Bread",
                        Category = "Breads",
                        Description = "Moist bread made with ripe bananas and a hint of vanilla, perfect for breakfast",
                        Price = 6.5
                    },
                    new Bake
                    {
                        Name = "Peanut Butter Brownies",
                        Category = "Brownies",
                        Description = "Fudgy brownies swirled with creamy peanut butter and topped with chocolate chips",
                        Price = 8.0
                    },
                    new Bake
                    {
                        Name = "Lemon Bars",
                        Category = "Bars",
                        Description = "Buttery shortbread crust topped with tangy lemon curd and dusted with powdered sugar",
                        Price = 5.5
                    },
                    new Bake
                    {
                        Name = "Chocolate Croissants",
                        Category = "Pastries",
                        Description = "Flaky butter croissants filled with rich dark chocolate",
                        Price = 4.5
                    },
                    new Bake
                    {
                        Name = "Black Forest Cake",
                        Category = "Cakes",
                        Description = "Chocolate layers filled with cherry compote and whipped cream, decorated with chocolate shavings",
                        Price = 14.0
                    },
                    new Bake
                    {
                        Name = "Almond Biscotti",
                        Category = "Cookies",
                        Description = "Twice-baked Italian cookies with toasted almonds, perfect for dipping in coffee",
                        Price = 4.5
                    },
                    new Bake
                    {
                        Name = "Blueberry Pie",
                        Category = "Pies",
                        Description = "Fresh blueberries baked in a flaky crust with a lattice top",
                        Price = 7.5
                    },
                    new Bake
                    {
                        Name = "Maple Pecan Danish",
                        Category = "Pastries",
                        Description = "Buttery pastry filled with maple-sweetened pecans and finished with a maple glaze",
                        Price = 5.0
                    },
                    new Bake
                    {
                        Name = "Oreo Cheesecake",
                        Category = "Cheesecakes",
                        Description = "Creamy cheesecake loaded with Oreo pieces on a chocolate cookie crust",
                        Price = 10.0
                    },
                    new Bake
                    {
                        Name = "Cranberry Orange Scones",
                        Category = "Pastries",
                        Description = "Buttery scones with dried cranberries and orange zest, drizzled with orange glaze",
                        Price = 4.5
                    },
                    new Bake
                    {
                        Name = "Salted Caramel Cupcakes",
                        Category = "Cupcakes",
                        Description = "Moist vanilla cupcakes filled with salted caramel and topped with caramel buttercream",
                        Price = 4.5
                    },
                    new Bake
                    {
                        Name = "Nutella Swirl Pound Cake",
                        Category = "Cakes",
                        Description = "Classic pound cake marbled with rich Nutella spread",
                        Price = 9.0
                    }
                );
            }
            //if (!context.Review.Any())
            //{
            //    context.Review.AddRange(
            //        new Review
            //        {
            //            BakeId = 1,
            //            User = "Aizah Akbar",
            //            Description = "I took one bite and was in heaven! The texture is so fudgy and rich. Definitely will be buying again. ",
            //            Rating = 5,
            //            CreatedDate = DateTime.Now
            //        },
            //        new Review
            //        {
            //            BakeId = 1,
            //            User = "Wajeeha Ikram",
            //            Description = "My family loved these, the only thing I would take off one star for is they arrived a bit squished. Other than that they were perfect!  ",
            //            Rating = 4,
            //            CreatedDate = DateTime.Now
            //        },
            //        new Review
            //        {
            //            BakeId = 2,
            //            User = "Sara Adam",
            //            Description = "These are the best cookies I've ever had! Warm them up in the microwave for 15 seconds and they're even better",
            //            Rating = 4,
            //            CreatedDate = DateTime.Now
            //        },
            //        new Review
            //        {
            //            BakeId = 3,
            //            User = "Qynaath Kokab",
            //            Description = "The ratio of cheesecake to biscuit was perfect, I've had too many cheesecakes with too much biscuit. Loved this!",
            //            Rating = 5,
            //            CreatedDate = DateTime.Now
            //        }
            //    );
            //}
            if (!context.User.Any())
            {
                context.User.AddRange(
                    new User
                    {
                        FirstName = "Aizah",
                        LastName = "Akbar",
                        Email = "aizahakbar@gmail.com",
                        Password = "password123",
                        Role = "Admin"
                    },
                    new User
                    {
                        FirstName = "Wajeeha",
                        LastName = "Ikram",
                        Email = "wajeehaikram@gmail.com",
                        Password = "pass123!",
                        Role = "User"
                    },
                    new User
                    {
                        FirstName = "Sara",
                        LastName = "Adam",
                        Email = "saraadam@gmail.com",
                        Password = "sara123!",
                        Role = "User"
                    },
                    new User
                    {
                        FirstName = "Qynaath",
                        LastName = "Kokab",
                        Email = "qynaathkokab@gmail.com",
                        Password = "qynaath123!",
                        Role = "User"
                    }
                );
            }
            context.SaveChanges();
        }
    }
}