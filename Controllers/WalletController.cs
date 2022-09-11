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
using System.IdentityModel.Tokens.Jwt;


namespace Shop.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        IWalletRepository _walletRepository;
        IPayRepository _payRepository;
        

        
        private AppDBContent _context;
        

        public WalletController(AppDBContent appDBContent, IWalletRepository repo, IPayRepository payRepository)
        {
           
            _context = appDBContent;
            _walletRepository = repo;
            _payRepository = payRepository;
           
           
        }
        public ActionResult Deposit(int costumerId)
        {

            return View();
        }
        [HttpPost]
        public ActionResult Deposit( IdentityUser user, decimal balance)     //was indentityuser
        {
            
            //operation type for transaction list
            string operationType = "Deposit";
            var costumerIdList = _walletRepository.GetWallets(); // costumerId  list in wallet table
      
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //user id
            decimal userBalance = _payRepository.GetUserBalance(userId); // get User Current Balance
            decimal newBalance = userBalance + balance; // user New Balance

            if (!costumerIdList.Contains(userId))
            {
                //if user have not wallet
                //creating wallet
                _walletRepository.CreateWallet(userId, newBalance);

                //add transaction in transaction list 
                _walletRepository.WithDraw(userId, balance, operationType, newBalance);

            }
            else
            {
                //update crrent Wallet
                _walletRepository.UpdateWallet(newBalance, userId, operationType);

                //add Transaction in transaction list 
                _walletRepository.WithDraw(userId, balance, operationType, newBalance);

            }

            
            return RedirectToAction("Index","Home");          
        }
    }
}
