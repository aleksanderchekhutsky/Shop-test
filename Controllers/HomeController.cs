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
        
        public IActionResult GetBalance()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var balance = _payRepository.GetUserBalance(userId);
            return Json(new {balance = balance });
        }

    }
}