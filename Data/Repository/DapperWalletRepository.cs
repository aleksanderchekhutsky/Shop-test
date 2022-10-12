using Dapper;
using Shop.Data.Interfaces;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Shop.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using Shop.Areas.Identity.Data;
using System.Security.Claims;

namespace Shop.Data.Repository
{
    public class DapperWalletRepository : IWalletRepository
    {     
        private string _deffaultConnction;
        public DapperWalletRepository(IConfiguration configuration)
        {
            _deffaultConnction = configuration.GetConnectionString("ApplicationDbContextConnection");      
        }
        public void CreateWallet(string UserId ,decimal amount)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                //creating wallet
                DynamicParameters param = new DynamicParameters();
                   
                param.Add("WalletId", UserId);  //user.Id
                param.Add("Balance", amount);
                param.Add("CreatedOn", DateTime.Now);
                param.Add("costumerId", UserId);
                db.Execute("CreateWallet", param, commandType: CommandType.StoredProcedure);

            }
        }

        public void WithDraw(string CostumerId,decimal amount)
        {
            // - balance 
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                DynamicParameters param = new DynamicParameters();
   
                param.Add("costumerId", CostumerId);
                param.Add("amount", amount);
                db.Execute("WithDraw", param, commandType: CommandType.StoredProcedure);
                param.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                var val = param.Get<int>("ReturnValue");
                //return val;
            }
        }

        public void Pay(OrderDetail order)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetWallets()
        {
            //return  walet userId ies list
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                return db.Query<string>("SELECT costumerId FROM Wallet").ToList();
            }     
        }

        //public void UpdateWallet(decimal currentBalance, string userId, string operationType)
        //{
        //    // this func update wallet if user have a wallet
        //    using (IDbConnection db = new SqlConnection(_deffaultConnction))
        //    {
        //        DynamicParameters param = new DynamicParameters();
        //        param.Add("Balance", currentBalance);
        //        param.Add("costumerId", userId);
        //        param.Add("OperationType", operationType);
        //        db.Execute("UpdateWallet", param, commandType: CommandType.StoredProcedure);
                
        //    }
        //}

        public decimal GetBalance(string userId)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("UserId", userId);
                return db.ExecuteScalar<decimal>("GetBalance", param, commandType: CommandType.StoredProcedure);
            }
        }

        public int UpdateWallet(string costumerId, string operationType, string pyamentStatus, decimal amount, string transactionId)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("costumerId", costumerId);
                param.Add("operationType", operationType);
                param.Add("paymentStatus", pyamentStatus);
                param.Add("amount", amount );
                param.Add("transactionId", transactionId);
                param.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                db.Execute("UpdateWallet", param, commandType: CommandType.StoredProcedure);
                //param.Add("ReturnValue", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var val = param.Get<int>("ReturnValue");
                return val;

            }
        }
    }
}
