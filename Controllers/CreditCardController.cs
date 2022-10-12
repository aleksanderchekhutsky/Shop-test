using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using System.Security.Claims;

namespace Shop.Controllers
{
    [Authorize]
    public class CreditCardController : Controller
    {
        private ICreditCardRepository _creditCardRepository;
        public CreditCardController(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }
        [HttpPost]
        public IActionResult SetCreditCard(CreditCard creditCard)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int cvvToInt = int.Parse(creditCard.CVV);   //Convert string CVV to Int for database

            
            //Saving Card in Database  
            _creditCardRepository.SaveCard(cardNumber: creditCard.CardNumber, expiration: creditCard.Expiration,
                                            cvv:cvvToInt, cardName: creditCard.CardName,userId);

            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult SetCreditCard()
        {
            return View();

        }
    }
}
