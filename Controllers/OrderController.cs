using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.Data.Models;


namespace Shop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersRepository allOrders;

        private readonly ShopCart shopCart;
        public OrderController(IOrdersRepository allOrders, ShopCart shopCart)
        {
            this.allOrders = allOrders;
            this.shopCart = shopCart;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
        ///---
        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Order order)
        {
            shopCart.ListShopItems = shopCart.getShopItem();
            if(shopCart.ListShopItems.Count == 0)
            {
                ModelState.AddModelError("","you have not cars");
            }
            if (ModelState.IsValid)
            {
                
                allOrders.CreateOrder(order);
                return RedirectToAction("Pay","Wallet");
            }
            return View(order);
        }
        public IActionResult Complite()
        {
            ViewBag.Message = "order successfully processed";
            return View();
                
        }
    }
}
