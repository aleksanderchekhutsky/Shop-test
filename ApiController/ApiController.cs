using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using System.Security.Claims;

namespace Shop.ApiController
{
    
    //[Route("api/[controller]")]

    public class ApiController : Controller
    {
        private IWalletTransactionRepository _walletTransaction;
        private IWalletRepository _walletRepository;
        public ApiController(IWalletRepository walletRepository, IWalletTransactionRepository walletTransactionRepository)
        {
            _walletRepository = walletRepository;
            _walletTransaction = walletTransactionRepository;
        }
       
        
       
        [HttpPost]
        
        public IActionResult PaymentStatus(string paymentStatus, string userId, string transactionId, string amount)
        {

            
            //decimal userBalance = GetBalance(userId);
            decimal decAmount = decimal.Parse(amount);

            _walletRepository.UpdateWallet( userId,"Deposit", paymentStatus,decAmount,transactionId);
            
            return Json("ok");

        }
       
        [HttpPost]
        public IActionResult WithDrawStatus(string paymentStatus, string transactionId, string userId, string amount)
        {
            var pay = paymentStatus;
            
            decimal _amount = decimal.Parse(amount);
            var val = _walletRepository.UpdateWallet(userId, "Withdraw", paymentStatus, _amount, transactionId);

            if(val == 0)
            {
                return Json(new
                {
                    StatusCode = 200
                });
            }


            return Json(new
            {
                StatusCode = 500
            });
        }
        decimal GetBalance(string userId)
        {
            return _walletRepository.GetBalance(userId);
        }
        

    }
}
