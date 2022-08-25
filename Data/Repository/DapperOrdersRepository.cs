using Microsoft.Extensions.Configuration;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using System.Collections.Generic;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System;


namespace Shop.Data.Repository
{
    public class DapperOrdersRepository : IOrdersRepository
    {
        private string _deffaultConnection;
        private ShopCart _cart;
        public DapperOrdersRepository(IConfiguration configuration, ShopCart shopCart)
        {
            _deffaultConnection = configuration.GetConnectionString("ApplicationDbContextConnection");
            _cart = shopCart;
        }
        public void CreateOrder(Order order)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnection))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Name", order.Name);
                param.Add("Surname", order.Surname);
                param.Add("Adress", order.Adress);
                param.Add("Phone", order.Phone);
                param.Add("Email", order.Email);
                param.Add("OrderTime", DateTime.Now);
                db.Execute("OrderCreate", param, commandType: CommandType.StoredProcedure);
                
            }
            //using (IDbConnection db = new SqlConnection(_deffaultConnection))
            //{

            //}

        }
    }
}
