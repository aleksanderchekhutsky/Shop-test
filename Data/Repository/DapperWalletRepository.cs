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
        public void Deposit(string UserId ,int amount)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                List<string> idies;
                idies = db.Query<string>("SELECT costumerId FROM Wallet").ToList();
                string OperationType = "Deposit";
                DynamicParameters param = new DynamicParameters();
                
                if (!idies.Contains(UserId))
                {
                    //if user have not waleet this code Create Walllet 
                   
                    param.Add("WalletId", UserId);  //user.Id
                    param.Add("Balance", amount);
                    param.Add("CreatedOn", DateTime.Now);
                    param.Add("costumerId", UserId);
                    db.Execute("Deposit", param, commandType: CommandType.StoredProcedure);

                }
                else
                {
                    //update amount if user have wallet 
                    //Proccedure name : UpdateWallet
                    param.Add("Balance", Convert.ToDecimal(amount));
                    param.Add("CreatedOn",DateTime.Now);
                    param.Add("costumerId", UserId);
                    db.Execute("UpdateWallet", param, commandType: CommandType.StoredProcedure);
                };
                WithDraw(UserId, amount, OperationType);
            }
        }

        public void WithDraw(string CostumerId, int amount, string operationType)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                
                int costumerAmount = db.ExecuteScalar<int>("SELECT Balance FROM Wallet WHERE costumerId=@CostumerId", new { costumerId = CostumerId});
                int currentBalance = costumerAmount;
                
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
    }
}
