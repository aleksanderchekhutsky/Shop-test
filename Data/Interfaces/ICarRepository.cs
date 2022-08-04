using Shop.Data.Models;
using System.Collections.Generic;

namespace Shop.Data.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> Cars { get;  }
        IEnumerable<Car> GetFavCars { get;  }
        Car GetObjectCAr(int CarId);
    }
}
