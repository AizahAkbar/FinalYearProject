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
                        Name = "Cosmic Brownie",
                        Category = "Brownies",
                        Description = "A fudgy, gooey brownie topped with chocolate ganache and colorful sugar-coated chocolate pieces",
                        Price = 7.5,
                        AltText = "A cosmic brownie topped with colorful sugar-coated chocolate pieces on a white surface."
                    },
                    new Bake
                    {
                        Name = "Chocolate Chip Cookie",
                        Category = "Cookies",
                        Description = "A crispy golden cookie loaded with rich chocolate chips, perfect for any occasion",
                        Price = 4.0,
                        AltText = "A freshly baked chocolate chip cookie with golden brown edges and gooey chocolate chips."
                    },
                    new Bake
                    {
                        Name = "Lemon Cheesecake",
                        Category = "Cheesecakes",
                        Description = "A creamy cheesecake with a tangy lemon flavor on a buttery graham cracker crust",
                        Price = 9.0,
                        AltText = "A slice of lemon cheesecake with graham cracker crust, lemon glaze, and whipped cream."
                    },
                    new Bake
                    {
                        Name = "Apple Pie",
                        Category = "Pies",
                        Description = "A homemade pie filled with spiced apples and wrapped in a flaky, buttery crust",
                        Price = 6.0,
                        AltText = "A slice of apple pie with a golden flaky crust and spiced apple filling."
                    },
                    new Bake
                    {
                        Name = "Red Velvet Cake",
                        Category = "Cakes",
                        Description = "A classic red velvet cake with smooth cream cheese frosting and a rich, velvety texture",
                        Price = 12.0,
                        AltText = "A slice of red velvet cake with crimson layers and creamy white frosting."
                    },
                    new Bake
                    {
                        Name = "Blueberry Muffin",
                        Category = "Muffins",
                        Description = "A fluffy muffin bursting with fresh blueberries and topped with a sweet streusel crumble",
                        Price = 3.5,
                        AltText = "A blueberry muffin with a golden top and visible fresh blueberries."
                    },
                    new Bake
                    {
                        Name = "Cinnamon Roll",
                        Category = "Pastries",
                        Description = "A soft, swirled pastry filled with cinnamon-sugar and topped with vanilla glaze",
                        Price = 5.0,
                        AltText = "A cinnamon roll topped with cream cheese icing on a white plate."
                    },
                    new Bake
                    {
                        Name = "Strawberry Shortcake",
                        Category = "Cakes",
                        Description = "A light vanilla sponge layered with fresh strawberries and whipped cream",
                        Price = 10.0,
                        AltText = "A slice of vanilla sponge cake with whipped cream and fresh strawberries."
                    },
                    new Bake
                    {
                        Name = "Double Chocolate Brownie",
                        Category = "Brownies",
                        Description = "A rich dark chocolate brownie with chunks of premium chocolate throughout",
                        Price = 8.0,
                        AltText = "A double chocolate brownie with a crackly top and melted chocolate chunks."
                    },
                    new Bake
                    {
                        Name = "Oatmeal Raisin Cookie",
                        Category = "Cookies",
                        Description = "A chewy oatmeal cookie packed with plump raisins and a hint of cinnamon",
                        Price = 4.0,
                        AltText = "A golden-brown oatmeal raisin cookie with visible raisins."
                    },
                    new Bake
                    {
                        Name = "Carrot Cake",
                        Category = "Cakes",
                        Description = "A moist spiced cake with fresh carrots, topped with cream cheese frosting and chopped walnuts",
                        Price = 11.0,
                        AltText = "A slice of carrot cake with cream cheese frosting and chopped nuts."
                    },
                    new Bake
                    {
                        Name = "Tiramisu",
                        Category = "Cakes",
                        Description = "An Italian dessert with coffee-soaked ladyfinger layers and mascarpone cream dusted with cocoa",
                        Price = 13.0,
                        AltText = "A slice of tiramisu with cocoa-dusted mascarpone cream."
                    },
                    new Bake
                    {
                        Name = "Raspberry Danish",
                        Category = "Pastries",
                        Description = "A flaky pastry filled with sweet raspberry preserves and drizzled with vanilla glaze",
                        Price = 4.5,
                        AltText = "A raspberry Danish pastry topped with almonds and glaze."
                    },
                    new Bake
                    {
                        Name = "Chocolate Eclair",
                        Category = "Pastries",
                        Description = "A light choux pastry filled with vanilla cream and topped with dark chocolate ganache",
                        Price = 5.5,
                        AltText = "A chocolate éclair filled with cream and topped with ganache."
                    },
                    new Bake
                    {
                        Name = "Pecan Pie",
                        Category = "Pies",
                        Description = "A rich slice with gooey pecan filling in a buttery crust",
                        Price = 8.5,
                        AltText = "A slice of pecan pie topped with whipped cream."
                    },
                    new Bake
                    {
                        Name = "Cherry Cheesecake",
                        Category = "Cheesecakes",
                        Description = "A smooth vanilla cheesecake slice topped with sweet cherry compote",
                        Price = 9.5,
                        AltText = "A slice of cherry cheesecake with a golden crust and cherry topping."
                    },
                    new Bake
                    {
                        Name = "Snickerdoodle Cookie",
                        Category = "Cookies",
                        Description = "A soft and chewy cookie rolled in cinnamon sugar with a perfect crackly top",
                        Price = 4.0,
                        AltText = "A golden-brown snickerdoodle cookie dusted with cinnamon sugar."
                    },
                    new Bake
                    {
                        Name = "Peanut Butter Brownie",
                        Category = "Brownies",
                        Description = "A fudgy brownie swirled with creamy peanut butter and topped with chocolate chips",
                        Price = 8.0,
                        AltText = "A peanut butter brownie with swirls and chocolate chips."
                    },
                    new Bake
                    {
                        Name = "Chocolate Croissant",
                        Category = "Pastries",
                        Description = "A flaky butter croissant filled with rich dark chocolate",
                        Price = 4.5,
                        AltText = "A golden chocolate croissant dusted with powdered sugar."
                    },
                    new Bake
                    {
                        Name = "Black Forest Cake",
                        Category = "Cakes",
                        Description = "A chocolate cake slice filled with cherry compote and whipped cream, topped with chocolate shavings",
                        Price = 14.0,
                        AltText = "A slice of Black Forest cake with cherry filling and whipped cream."
                    },
                    new Bake
                    {
                        Name = "Almond Biscotti",
                        Category = "Cookies",
                        Description = "A twice-baked Italian cookie with toasted almonds, perfect for dipping in coffee",
                        Price = 4.5,
                        AltText = "A single almond biscotti with visible almond pieces."
                    },
                    new Bake
                    {
                        Name = "Blueberry Pie",
                        Category = "Pies",
                        Description = "A slice of fresh blueberry pie with a flaky crust and lattice top",
                        Price = 7.5,
                        AltText = "A slice of blueberry pie with a lattice crust and juicy filling."
                    },
                    new Bake
                    {
                        Name = "Maple Pecan Danish",
                        Category = "Pastries",
                        Description = "A buttery pastry filled with maple-sweetened pecans and finished with a maple glaze",
                        Price = 5.0,
                        AltText = "A maple pecan Danish with drizzled icing on top."
                    },
                    new Bake
                    {
                        Name = "Oreo Cheesecake",
                        Category = "Cheesecakes",
                        Description = "A creamy cheesecake loaded with Oreo pieces on a chocolate cookie crust",
                        Price = 10.0,
                        AltText = "A slice of Oreo cheesecake topped with whipped cream and an Oreo."
                    },
                    new Bake
                    {
                        Name = "Salted Caramel Cupcake",
                        Category = "Cupcakes",
                        Description = "A moist vanilla cupcake filled with salted caramel and topped with caramel buttercream",
                        Price = 4.5,
                        AltText = "A salted caramel cupcake with caramel buttercream and drizzle."
                    },
                    new Bake
                    {
                        Name = "Nutella Swirl Pound Cake",
                        Category = "Cakes",
                        Description = "A classic pound cake slice marbled with rich Nutella spread",
                        Price = 9.0,
                        AltText = "A slice of pound cake marbled with rich Nutella, served on a beige plate with a fork."
                    },
                    new Bake
                    {
                        Name = "Pumpkin Spice Muffin",
                        Category = "Muffins",
                        Description = "A moist muffin spiced with pumpkin pie spices and topped with a crumbly streusel",
                        Price = 3.5,
                        AltText = "A pumpkin spice muffin with a golden-brown top, shown with a bite taken out."
                    },
                    new Bake
                    {
                        Name = "Lemon Raspberry Cupcake",
                        Category = "Cupcakes",
                        Description = "A zesty lemon cupcake with raspberry filling and a swirl of lemon buttercream frosting",
                        Price = 4.5,
                        AltText = "A lemon raspberry cupcake topped with swirled lemon buttercream, a fresh raspberry, and a lemon slice."
                    },
                    new Bake
                    {
                        Name = "Strawberry Shortcake Cupcake",
                        Category = "Cupcakes",
                        Description = "A light vanilla cupcake filled with strawberry compote and topped with whipped cream frosting",
                        Price = 4.25,
                        AltText = "A strawberry shortcake cupcake with whipped cream frosting, strawberry pieces, and a mint leaf on top."
                    }
                );
            }
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