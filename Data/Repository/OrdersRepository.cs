using Shop.Data.Interfaces;
using Shop.Data.Models;
using System;

namespace Shop.Data.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDBContent appDBContent;
        private readonly ShopCart shopCart;
        public OrdersRepository(AppDBContent appDBContent,ShopCart shopCart )
        {
            this.appDBContent = appDBContent;
            this.shopCart = shopCart;
        }
        public void CreateOrder(Order order)
        {

            order.OrderTime = DateTime.Now;
            appDBContent.Order.Add(order);
            appDBContent.SaveChanges();
            var items = shopCart.ListShopItems;
            foreach(var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    CarID = el.Car.Id,
                    OrderID = order.Id,
                    Price = el.Car.Price
                };
                appDBContent.OrderDetails.Add(orderDetail);
            }
            appDBContent.SaveChanges();

        }
    }
}
