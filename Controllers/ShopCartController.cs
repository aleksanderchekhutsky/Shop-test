using System;
using Shop.Data.Repository;
using Shop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Shop.ViewModels;
using System.Linq;
using Shop.Data.Interfaces;

namespace Shop.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IAllCars _carRep;
        private readonly ShopCart _shopCart;
        public ShopCartController(IAllCars carRep, ShopCart shopCart)
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
            var item = _carRep.Cars.FirstOrDefault(i => i.Id == id);
            if(item != null)
            {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");

        }

    }
}
