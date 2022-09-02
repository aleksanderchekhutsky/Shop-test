using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using Shop.Data.Models;
using Shop.Data;
using Shop.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Shop.Data.Interfaces;
//using Shop.Data.Models.

namespace Shop.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        IWalletRepository _walletRepository;
        //public WalletController(IWalletRepository repo)
        //{
        //    _walletRepository = repo;

        //}

        
        private AppDBContent _context;
        private UserManager<ShopUser> _userManager;
        public WalletController(UserManager<ShopUser> userManager,AppDBContent appDBContent, IWalletRepository repo)
        {
            _userManager = userManager;
            _context = appDBContent;
            _walletRepository = repo;
           
        }
        public ActionResult Deposit(int costumerId)
        {

            return View();
        }
        [HttpPost]
        public ActionResult Deposit( IdentityUser user, int balance)     //was indentityuser
        {
            //    //authorizer user id 
            //var user =  _userManager.GetUserAsync(User).Result;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);      //Authorize user id
            _walletRepository.Deposit( userId, balance);          
            return RedirectToAction("Index","Home");          
        }
        
        public ActionResult WithDraw(int costumerId)
        {
            return View();
        }
        [HttpPost]
        public ActionResult WithDraw(IdentityUser user, int amount)
        {
            string opType = "Pay";

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _walletRepository.WithDraw( userId, amount, opType);
            


            return RedirectToAction("Index", "Home");

        }

        // Get: Wallet/Pay
        public ActionResult Pay(int costumerId)
        {
            return RedirectToAction("Pay");
        }
        //Post: Wallet/Pay 
        [HttpGet]
        public ActionResult Pay(OrderDetail order)
        {
            //if (ModelState.IsValid)
            //{
            //    var user = _userManager.GetUserAsync(User).Result;
            //    var costumer = _context.Wallet.SingleOrDefault(c => c.costumerId == user.Id);
                
            //    if (order.Price < costumer.Balance)
            //    {
            //        costumer.Balance -= order.Price;
            //        _context.SaveChanges ();
            //        return View("Complite");
            //    }
            //    return View();
                    
            //}
            return View("BalanceError");

        }


    }
}
