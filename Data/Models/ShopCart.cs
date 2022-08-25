using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Shop.Data.Models
{
    public class ShopCart
    {
        private readonly AppDBContent appDBContent;
        private string _deffaultConnection = "Data Source=DESKTOP-VSHHQ0D;Database=Shop; Persist Security Info=false; User ID = 'sa'; Password='sa'; MultipleActiveResultSets=True; Trusted_Connection=False;TrustServerCertificate=True; Encrypt = True";
        public ShopCart(AppDBContent appDBContent)
        {
            //_deffaultConnection = configuration.GetConnectionString("ApplicationDbContextConnection");
            this.appDBContent = appDBContent;
        }
        public ShopCart(IConfiguration configuration, AppDBContent appDBContent)
        {
            _deffaultConnection = configuration.GetConnectionString("ApplicationDbContextConnection");
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
            //appDBContent.ShopCartItem.Add(new ShopCartItem
            //{
            //    ShopCartId = ShopCartId,
            //    Car = car,
            //    Price = car.Price
            //});

            //appDBContent.SaveChanges();
            using(IDbConnection db = new SqlConnection(_deffaultConnection))
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("ShopCartId", ShopCartId);
                param.Add("Price", car.Price);
                param.Add("CarId", car.Id);
                db.Execute("AddToCart", param, commandType: CommandType.StoredProcedure);


            }
        }
        public List<ShopCartItem> getShopItem()
        {
            return appDBContent.ShopCartItem.Where(c=> c.ShopCartId== ShopCartId).Include(s=> s.Car).ToList();
        }
    }
}
