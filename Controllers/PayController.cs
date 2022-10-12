using Microsoft.AspNetCore.Mvc;
using Shop.Data.Repository;
using Shop.Data.Interfaces;
using System.Security.Claims;
using Shop.Data.Models;

namespace Shop.Controllers
{
    public class PayController : Controller
    {
        IPayRepository _payRepository;
        IWalletRepository _walletRepository;
        public PayController(IPayRepository payRepository,IWalletRepository walletRepository)
        {
            _payRepository=payRepository;
            _walletRepository  = walletRepository;
        }
        public IActionResult BuyCar(string id)
        {
            
            //User Id
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            string operationType = "Pay";            //Operation type

            
           
            decimal totalPrice = _payRepository.GetTotalPrice(id); // get total pricec from cart cars
            
            decimal userBalance = _payRepository.GetUserBalance(userId);  //get user Balance (Current Balance)
            
            decimal newBalance;
            if (totalPrice > userBalance)
            {
                // Check user balance if USer have money
                return View("BalanceError");
            }
            else
            {
                
                newBalance = userBalance - totalPrice;
                // If User havve money

                // WITHDRAW: Add transaction and Transaction in Transactions List 
                //_walletRepository.WithDraw(userId, totalPrice,operationType, newBalance);

                //Pay functional 
                _payRepository.Pay(userId, totalPrice, operationType);
                return View("BuyCar");
            }

            
        }
    }
}
