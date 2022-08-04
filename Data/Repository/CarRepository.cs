using Microsoft.EntityFrameworkCore;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Data.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDBContent appDBContent;
        public CarRepository(AppDBContent appDbContent)
        {
            this.appDBContent = appDbContent;

        }
        public IEnumerable<Car> Cars => appDBContent.Car.Include(c=> c.Category);

        public IEnumerable<Car> GetFavCars => appDBContent.Car.Where(p => p.IsFavourite).Include(c=>c.Category);

        public Car GetObjectCAr(int CarId) => appDBContent.Car.FirstOrDefault(p=>p.Id== CarId);
        
    }
}
