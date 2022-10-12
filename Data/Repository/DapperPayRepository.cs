using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
namespace Shop.Data.Repository
{
    public class DapperPayRepository: IPayRepository
    {
        private string _deffaultConnection;
        public DapperPayRepository(IConfiguration configuration)
        {
            _deffaultConnection = configuration.GetConnectionString("ApplicationDbContextConnection");

        }

        public decimal GetTotalPrice(string CartId)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnection))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("shopCartId", CartId);
                return db.ExecuteScalar<decimal>("GetCarTotalPrice", param, commandType: CommandType.StoredProcedure);
            }


        }

        public decimal GetUserBalance(string userId)
        {
            //Return user Balance
            using (IDbConnection db = new SqlConnection(_deffaultConnection))
            
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("userId", userId);
                string usserId = userId;
                decimal balance = db.ExecuteScalar<decimal>("GetUserBalance", param, commandType: CommandType.StoredProcedure);
                
                return Convert.ToDecimal(balance);
            }

        }
        

        public void Pay(string userId,  decimal carPrice, string OperationType)
        {
            // Updating User Balanace afTer Buy car
            using (IDbConnection db = new SqlConnection(_deffaultConnection))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("costumerId", userId);
                param.Add("Balance", carPrice);
                param.Add("OperationType", OperationType);
                param.Add("paymentStatus", "Submit");

                db.Execute("UpdateWallet", param, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
