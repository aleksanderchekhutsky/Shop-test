using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Shop.Areas.Identity.Data;
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

        public void AddStatus(string status)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                
            }
        }

        public Guid CreatTransaction(WalletTransaction transaction)
        {
            //add transaction withot status
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                var ccreatedOn = transaction.createdOn;
                var transactionId = transaction.TransactionId;
                var amount = transaction.Amount;
                string status= transaction.Status;
                DateTime time = DateTime.Now;
                DynamicParameters param = new DynamicParameters();
                param.Add("createdOn",DateTime.Now);
                param.Add("Status", status);
                param.Add("Amount",amount);
                param.Add("TransactionId",dbType:DbType.Guid,direction: ParameterDirection.Output);
                param.Add("ReturnValue",dbType:DbType.Int32,direction: ParameterDirection.ReturnValue);
                
                db.Execute("Fake", param, commandType: CommandType.StoredProcedure);
                var ret = param.Get<int>("ReturnValue"); 

                return param.Get<Guid>("TransactionId");

            }
        }

        public List<WalletTransaction> GetAllTransaction(string userId, DateTime time)
        {
            //Get: Return All Transaction List Without Parameters  [HttpGet]
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("userId", userId);
                param.Add("time", time);
                //param.Add("returnvalue",);
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

        public void SetStatus( string userId, string status)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                db.Execute("UPDATE WalletTransaction SET Status=@status ORDER BY createdOn DESC LIMIT 1 WHERE userId=@userId");
            }


        }

        public void UpdateTransaction(string transactionId, string userId, string status, decimal amount, decimal currentBalance)
        {
            using (IDbConnection db = new SqlConnection(_deffaultConnction))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("TransactionId", transactionId);
                param.Add("UserId", userId);
                param.Add("Status", status);
                param.Add("Amount", amount);
                param.Add("CurrentBalance",currentBalance);
                db.Execute("UpdateTransaction", param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
