using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Shop.Data.Interfaces
{
    public interface IWalletTransactionRepository
    {
        [HttpGet]
        public List<WalletTransaction> GetAllTransaction(string userId, DateTime time);
        public IEnumerable<WalletTransaction> GetTransaction(DateTime time, string userId, string transactionType);
    }
}
