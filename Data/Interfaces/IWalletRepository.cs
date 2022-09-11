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
        void WithDraw(string UserId, decimal amount, string operationType, decimal currentBalance);
        List<string> GetWallets();
        void UpdateWallet(decimal currentBalance, string userId, string operationType);

    }
}
