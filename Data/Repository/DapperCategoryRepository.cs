using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using System.Collections.Generic;
using System.Data;

namespace Shop.Data.Repository
{
    public class DapperCategoryRepository : ICategoryRepository
    {
        private string _deffaultConnection;
        public DapperCategoryRepository(IConfiguration configuration)
        {
            _deffaultConnection = configuration.GetConnectionString("ApplicationDbContextConnection");

        }

        //IEnumerable<Category> ICategoryRepository.AllCategories => throw new System.NotImplementedException();

         IEnumerable<Category> ICategoryRepository.AllCategories

            {
                get
                {
                    using (IDbConnection db = new SqlConnection(_deffaultConnection))
                    {
                        //var cat = db.Query("SELECT * FROM Category");
                        //return (IEnumerable<Category>)cat;

                        return db.Query<Category>("SELECT * FROM Car "); ///Category

                    }
                }
            }
    }
}
