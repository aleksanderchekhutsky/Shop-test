using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace Shop.Data.Models
{
    public class ShopCart
    {
        private readonly AppDBContent appDBContent;
        public ShopCart(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        //public int Id { get; set; }
        public string ShopCartId { get; set; }
        /// damateba
        //public int Money { get; set; }
        /// damateba 
        public List<ShopCartItem> ListShopItems { get; set; }

        //public static ShopCart GetCart(IServiceProvider services)
        //{
        //    ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        //    var context = services.GetService<AppDBContent>();
        //    string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

        //    session.SetString("CartId", shopCartId);
        //    return new ShopCart(context) { ShopCartId = shopCartId };

        //}
        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDBContent>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", shopCartId);

            return new ShopCart(context) { ShopCartId = shopCartId };
        }
        public void AddToCart(Car car)
        {
            appDBContent.ShopCartItem.Add(new ShopCartItem
            {
                ShopCartId = ShopCartId,
                Car = car,
                Price = car.Price
            });

            appDBContent.SaveChanges();
        }
        public List<ShopCartItem> getShopItem()
        {
            return appDBContent.ShopCartItem.Where(c=> c.ShopCartId== ShopCartId).Include(s=> s.Car).ToList();
        }
    }
}
