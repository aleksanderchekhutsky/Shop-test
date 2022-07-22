
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.Data.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Controllers
{
    public class CarsController : Controller
    {
        private  readonly IAllCars _allcars;
        private readonly ICarsCategory _carsCategory;

        public CarsController(IAllCars iAllCars, ICarsCategory iCarsCategory)
        {
            _allcars = iAllCars;
            _carsCategory = iCarsCategory;
        }               //constructor
        [Route("Cars/List")]
        [Route("Cars/List/{category}")]
        public ViewResult List(string category)
        {
            string _category = category;
            IEnumerable<Car> cars = null;
            string currCategory = "";
            if (string.IsNullOrEmpty(category))
            {
                cars = _allcars.Cars.OrderBy(i => i.Id);
            }
            else
            {
                if (string.Equals("electro", category, StringComparison.OrdinalIgnoreCase))
                {
                    cars = _allcars.Cars.Where(i => i.Category.CategoryName.Equals("EVCars")).OrderBy(i => i.Id);
                    currCategory = "EV";
                }
                else if (string.Equals("fuel", category, StringComparison.OrdinalIgnoreCase))
                {
                    cars = _allcars.Cars.Where(i => i.Category.CategoryName.Equals("Classic cars")).OrderBy(i => i.Id);
                    currCategory = "Classic Cars";
                }
                //currCategory = _category;
            }
            var carObj = new CarsListViewModel
            {
                AllCars = cars,
                currCategory = currCategory
            };     
            ViewBag.Title="Cars web";
            //CarsListViewModel obj = new CarsListViewModel();
            //obj.AllCars = _allcars.Cars;
            //obj.currCategory = "Automobiles";

            return View(carObj);
        }
    }
}
