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

        public void WithDraw(string CostumerId, decimal amount, string operationType,decimal currentBalance)
        {
            // Add transaction and Transaction in Transactions List 
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                DynamicParameters param = new DynamicParameters();
   
                param.Add("costumerId", CostumerId);
                param.Add("CurentBalance", currentBalance);
                param.Add("Amount", amount);
                param.Add("createdOn", DateTime.Now);
                param.Add("OperationType", operationType);
                db.Execute("WithDraw", param, commandType: CommandType.StoredProcedure);
                
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

        public void UpdateWallet(decimal currentBalance, string userId, string operationType)
        {
            // this func update wallet if user have a wallet
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Balance", currentBalance);
                param.Add("costumerId", userId);
                param.Add("OperationType", operationType);
                db.Execute("UpdateWallet", param, commandType: CommandType.StoredProcedure);
                
            }
        }
    }
}
