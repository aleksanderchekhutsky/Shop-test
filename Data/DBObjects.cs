using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Data
{
    public class DBObjects
    {
        private static Dictionary<string, Category> category;
        public static void Initial(AppDBContent content)
        {
            //AppDBContent content;
            //using (var scope=app.ApplicationServices.CreateScope()){
            //     content = scope.ServiceProvider.GetRequiredService<AppDBContent>();

            //}
            //AppDBContent content = app.ApplicationServices.GetRequiredService<AppDBContent>();
            if (!content.Category.Any())
            {
                content.Category.AddRange(Categories.Select(c => c.Value));
            }
                //content.Category.AddRange(Categories.Select(c => c.Value));
            if (!content.Car.Any())
            {
                content.AddRange(
                    new Car
                    {
                        Name = "Tesla Models",
                        ShortDesc = "shortdesc1",
                        LongDesc = "long descripprtion1",
                        Img = "/img/tesla.jpg",
                        Price = 45000,
                        IsFavourite = true,
                        Available = true,
                        Category = Categories["EVCars"]
                    },
                    //new Car
                    //{
                    //    Name = "Ford Mustang",
                    //    ShortDesc = "shortdesc2",
                    //    LongDesc = "long descripprtion2",
                    //    Img = "/img/mustang.jpg",
                    //    Price = 60000,
                    //    IsFavourite = false,
                    //    Available = true,
                    //    Category = Categories["Classic cars"]
                    //},
                    new Car
                    {
                        Name = "BMW m*",
                        ShortDesc = "shortdesc2",
                        LongDesc = "long descripprtion2",
                        Img = "/img/bmw.jpg",
                        Price = 8000,
                        IsFavourite = true,
                        Available = true,
                        Category = Categories["Classic cars"]
                    },
                    new Car
                    {
                        Name = "Tesla model X",
                        ShortDesc = "shortdesc2",
                        LongDesc = "long descripprtion2",
                        Img = "/img/tsla.jpg",
                        Price = 9000,
                        IsFavourite = true,
                        Available = true,
                        Category = Categories["EVCars"]
                    }
                );
            }
            content.SaveChanges();
        }


        //private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)
                {
                    var list = new Category[]
                    {
                        new Category { CategoryName = "EVcars", Description ="green car"},
                        new Category { CategoryName ="Classic cars", Description ="Gasoline Cars"}
                    };
                    category = new Dictionary<string, Category>();
                    foreach (Category el in list)
                        category.Add(el.CategoryName, el);
                }
                return category;

            }
        }
    }
}
