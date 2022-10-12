using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Shop.Data;
using Shop.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Shop.Data.Interfaces;
using System.Net;
using System.Collections.Specialized;
using Shop.Data.Models;

namespace Shop.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        IWalletRepository _walletRepository;
        IPayRepository _payRepository;
        IWalletTransactionRepository _walletTransactionRepository;
        

        
        private AppDBContent _context;
        

        public WalletController(AppDBContent appDBContent, IWalletRepository repo, IPayRepository payRepository, IWalletTransactionRepository transactionRepo)
        {
           
            _context = appDBContent;
            _walletRepository = repo;
            _payRepository = payRepository;
           _walletTransactionRepository = transactionRepo;
           
        }
        public ActionResult Deposit(int costumerId)
         {

            return View();
        }
        public string CreateTransaction(WalletTransaction trans ) //was decimal amount
        {
            //WalletTransaction transaction = new WalletTransaction();
            //transaction.Amount = amount;
            trans.createdOn = DateTime.Now;
            
            //transaction.Status = "unknown";
            //trans.Status ==null ? trans.Status =="unknown": trans.Status = trans.Status;
            if (trans.Status == null)
            {
                trans.Status = "unknown";
            }
            var test = _walletTransactionRepository.CreatTransaction(trans); // added 



            // return _walletTransactionRepository.CreatTransaction(transaction).ToString();// old
            return test.ToString();

        }
        //new return balance 
        //Was Pay Detail
        [HttpPost]
        public IActionResult Deposit( WalletTransaction transaction)
          {
            WalletTransaction trans = new WalletTransaction { };
            // var transId = CreateTransaction(transaction.Amount);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var transId = CreateTransaction(transaction);
          //  _walletRepository.WithDraw(userId, transaction.Amount);


            string adress = "https://localhost:44392/Payments/Deposit";
           // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           // string trasnId = transaction.TransactionId.ToString();
            var amount = transaction.Amount;
            //----------------------

            //--------------------
            string newaddress = String.Format("{0}?amount={1}&&userId={2}&&transactionId={3}",adress,amount.ToString(), userId,transId);
            //return Redirect("https://localhost:44392/Payments/Deposit?balance=435");
            
            return Redirect(newaddress);
            
        }
        public IActionResult WithDraw()
        {
            return View();
        }
        [HttpPost]
        public IActionResult WithDraw(WalletTransaction transaction)
        {
            WalletTransaction trans = new WalletTransaction { };
            var transId = CreateTransaction(transaction);

            string adress = "https://localhost:44392/Payments/WithDraw";
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // string trasnId = transaction.TransactionId.ToString();
            var amount = transaction.Amount;
            //----------------------

            //--------------------
            string newaddress = String.Format("{0}?amount={1}&&userId={2}&&transactionId={3}", adress, amount.ToString(), userId, transId);
            

            return Redirect(newaddress);
        }
    
    }
}
