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
        public void AddStatus(string status);
        public void SetStatus(string userId, string status);
        public Guid CreatTransaction (WalletTransaction transaction);
        public void UpdateTransaction(string transactionId, string userId, string status, decimal amount, decimal currentBalance);    }
}
