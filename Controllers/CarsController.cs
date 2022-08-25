
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
        private  readonly ICarRepository _allcars;
        private readonly ICategoryRepository _carsCategory;

        public CarsController(ICarRepository iAllCars, ICategoryRepository iCarsCategory)
        {
            _allcars = iAllCars;
            _carsCategory = iCarsCategory;
        }               
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
                    //GET electric cars
                   
                    cars = _allcars.Cars.Where(i => i.CategoryID == 2);
                        
                    currCategory = "EVCars";
                }
                else if (string.Equals("fuel", category, StringComparison.OrdinalIgnoreCase))
                {
                    // GET Fuel cars
                    cars = _allcars.Cars.Where(i => i.CategoryID == 1);
                    
                    currCategory = "Classic Cars";
                }
               
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
