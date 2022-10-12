using Shop.Data.Models;
using Shop.Data.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Shop.Data.Repository
{
    public class DapperCreditCardRepository : ICreditCardRepository
    {
        private string _deffaultConnection;
        public DapperCreditCardRepository(IConfiguration configuration)
        {
            _deffaultConnection = configuration.GetConnectionString("ApplicationDbContextConnection");

        }

        public void SaveCard(string cardNumber, string expiration, int cvv, string cardName, string userId)
        {
            // SAVING USER CARD INTO CreditCard Table
            using (IDbConnection db = new SqlConnection(_deffaultConnection))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("cardNumber",cardNumber);
                param.Add("expiration", expiration);
                param.Add("cvv", cvv);
                param.Add("cardName", cardName);
                param.Add("userId", userId);
                
                db.Execute("SaveCard", param, commandType: CommandType.StoredProcedure);
            }


        }
    }
}
