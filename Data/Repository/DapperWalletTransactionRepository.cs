using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Shop.Data.Repository
{
    public class DapperWalletTransactionRepository : IWalletTransactionRepository
    {
        private string _deffaultConnction;
        public DapperWalletTransactionRepository(IConfiguration configuration)
        {
            _deffaultConnction = configuration.GetConnectionString("ApplicationDbContextConnection");
        }
        public List<WalletTransaction> GetAllTransaction(string userId)
        {
            //Get: Return All Transaction List Without Parameters  [HttpGet]
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("userId", userId);
                var trans = db.Query<WalletTransaction>("GetAllTransaction", param, commandType: CommandType.StoredProcedure);

                //return db.ExecuteScalar<WalletTransaction>("GetAllTransaction", param, commandType: CommandType.StoredProcedure);
                return trans.AsList();

            }
                
           
        }
        public IEnumerable<WalletTransaction> GetTransaction(DateTime time, string userId, string OperationType)
        {
            using(IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                //string userid = userId;
                //List<WalletTransaction> idies;
                //idies = (List<WalletTransaction>)db.Query<WalletTransaction>("SELECT * FROM WalletTransaction WHERE customerId = @userId");
                ////List<WalletTransaction> trans = db.ExecuteScalar("SELECT * FROM WalletTransaction WHERE OperationType ='Deposit'").ToString();
                ///
                ///
                DynamicParameters param2 = new DynamicParameters();
                param2.Add("userId", userId);
                var trans2 = db.Query<WalletTransaction>("GetAllTransaction", param2, commandType: CommandType.StoredProcedure);
                IEnumerable<WalletTransaction> res2 = from item in trans2
                                                where item.OperationType == OperationType
                                                select item;
                //------------------------------------------------
                DynamicParameters param = new DynamicParameters();
                param.Add("userId", userId);
                param.Add("OperationType", OperationType);
                var trans = db.Query<WalletTransaction>("GetTransactionByparam", param, commandType: CommandType.StoredProcedure);
                //List<WalletTransaction> =

                //return new List<WalletTransaction>(trans.AsEnumerable());
                return res2;

            }
            
        }
    }
}
