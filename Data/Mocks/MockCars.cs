using Shop.Data.Interfaces;
using Shop.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Data.Mocks
{
    public class MockCars : IAllCars
    {
        private readonly ICarsCategory _CategoryCars = new MockCategory();
        public IEnumerable<Car> Cars
        {
            get
            {
                return new List<Car>
                {
                    new Car
                    {
                        Name = "Tesla Models",
                        ShortDesc ="shortdesc1",
                        LongDesc ="long descripprtion1",
                        Img ="/img/tesla.jpg",
                        Price = 45000,
                        IsFavourite = true,
                        Available = true,
                        Category = _CategoryCars.AllCategories.Last()
                    },
                    new Car
                    {
                        Name = "Ford Mustang",
                        ShortDesc ="shortdesc2",
                        LongDesc ="long descripprtion2",
                        Img ="/img/mustang.jpg",
                        Price = 60000,
                        IsFavourite = true,
                        Available = true,
                        Category = _CategoryCars.AllCategories.First()
                    },
                    new Car
                    {
                        Name = "BMW m*",
                        ShortDesc ="shortdesc2",
                        LongDesc ="long descripprtion2",
                        Img ="/img/bmw.jpg",
                        Price = 8000,
                        IsFavourite = true,
                        Available = true,
                        Category = _CategoryCars.AllCategories.First()
                    }

                };
                
            }
        }
        public IEnumerable<Car> GetFavCars { get; set; }

        public Car GetObjectCAr(int CarId)
        {
            throw new System.NotImplementedException();
        }
    }
}
