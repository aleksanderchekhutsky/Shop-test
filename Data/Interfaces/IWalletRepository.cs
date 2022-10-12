using Dapper;
using Shop.Data.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Shop.Areas.Identity.Data;

namespace Shop.Data.Interfaces
{
    public interface IWalletRepository
    {
        void CreateWallet( string UserId, decimal amount );          //identityUser
        void Pay(OrderDetail order);
        void WithDraw(string CostumerId, decimal amount);
        List<string> GetWallets();
        int UpdateWallet(string costumerId, string operationType, string pyamentStatus, decimal amount, string transactionId);
        decimal GetBalance(string userId);

    }
}
