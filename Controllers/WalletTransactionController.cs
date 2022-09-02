using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using Shop.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shop.Data.Models;
using System.Collections.Generic;
using System.Web.WebPages.Html;
using Newtonsoft.Json;
using Shop.Areas.Identity.Data;
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
        public IActionResult GetTransactions()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            





            var result = _repo.GetAllTransaction(userId);
            
            return Json(new { data = result });

        }

        [HttpPost]
        public IActionResult TransactionListByTime(DateTime dateTime, string transactionType)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            


            return View(_repo.GetTransaction(dateTime, userId, transactionType));

        }
        
    }
}
