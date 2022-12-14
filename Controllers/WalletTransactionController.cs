using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

using Shop.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shop.Data.Models;
using System.Collections.Generic;
using System.Web.WebPages.Html;
using Newtonsoft.Json;
using Shop.Areas.Identity.Data;
using System.Globalization;
using System.Threading.Tasks;
using System.Security.Claims;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity;
//using System.Web.Mvc.JsonRequestBehavior;

namespace Shop.Controllers
{
    [Authorize]
    public class WalletTransactionController : Controller
    {

        
        IWalletTransactionRepository _repo;
        


        public WalletTransactionController( IWalletTransactionRepository repo)
        {
            _repo = repo;
           
        }
        
            
        [HttpGet]
        public ActionResult Transactions()
        {
            return View();
        }


        [HttpPost]
        public IActionResult GetTransactions(string dateTime)
        {

            DateTime searchTime = dateTime == null ? DateTime.Now : DateTime.Parse(dateTime);  //check datatime 
            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // user id
            
            var result = _repo.GetAllTransaction(userId, searchTime);
           
            
            return Json(new { data = result});

          }

        [HttpPost]
        public IActionResult TransactionListByTime(DateTime dateTime, string transactionType)
        {

             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
         


            return View(_repo.GetTransaction(dateTime, userId, transactionType));

        }
        public void AddStatus(string status)
        {
            
        }
        
    }
}
