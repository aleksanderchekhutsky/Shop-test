using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private IPayRepository _payRepository;

        private ICarRepository _carRep;

        public HomeController(ICarRepository carRep, IPayRepository payRepository)
        {
            _carRep = carRep;
            _payRepository = payRepository;
        }

        public ViewResult Index()
        {
            var homeCars = new HomeViewModel
            {
                favCars = _carRep.GetFavCars
            };
            return View(homeCars);
        }
        //Return  User balance In Nav Bar (Layot)
        public IActionResult GetBalance()
        {
            //User Id 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //Get user  Balance 
            var balance = _payRepository.GetUserBalance(userId);
            //return User balance In Json
            return Json(new {balance = balance });
        }

    }
}