using System;
using Shop.Data.Repository;
using Shop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.ViewModels;
using System.Linq;
using Shop.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Controllers
{
    public class ShopCartController : Controller
    {
        public int idies;
        private readonly ICarRepository _carRep;
        private readonly ShopCart _shopCart;
        public ShopCartController(ICarRepository carRep, ShopCart shopCart)
        {
            _carRep = carRep;
            _shopCart = shopCart;
        }
        public ViewResult Index()
        {
            var Items = _shopCart.getShopItem();
            _shopCart.ListShopItems  = Items;
            var obj = new ShopCartViewModel
            {
                shopCart = _shopCart
            };

            return View(obj);

        }
        
        public RedirectToActionResult addToCart(int id )
        {
            //idies.Append(id);
            idies= id;
            var item = _carRep.Cars.FirstOrDefault(i => i.Id == id);
            if(item != null)
            {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");

        }
        public RedirectToActionResult Pay(int idies)
        {
            int car = idies;
            //int carId = id;
            return RedirectToAction("Pay", "BuyCar", car);
            
        }

    }
}
