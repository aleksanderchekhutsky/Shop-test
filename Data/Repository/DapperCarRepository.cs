using Microsoft.Extensions.Configuration;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using System.Collections.Generic;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Shop.Data.Repository
{
    public class DapperCarRepository : ICarRepository
    {
        private string _deffaultConnection;
        public DapperCarRepository(IConfiguration configuration)
        {
            _deffaultConnection = configuration.GetConnectionString("ApplicationDbContextConnection");
        }
   
        public IEnumerable<Car> Cars
        {
            get
            {
                using(IDbConnection db = new SqlConnection(_deffaultConnection))
                {
                    return (IEnumerable<Car>)db.Query<Car>("SELECT * FROM Car");
                }
            }
        }
         
        IEnumerable<Car> ICarRepository.GetFavCars
        {
            get
            {
                using (IDbConnection db = new SqlConnection(_deffaultConnection))
                {
                    int id = 1;
                    var result = db.Query<Car>("SELECT * FROM Car WHERE IsFavourite = 1");
                    return (IEnumerable<Car>)result;  
                }
            }
        }    
    }
}
