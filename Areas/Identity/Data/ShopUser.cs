using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Shop.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ShopUser class
    public class ShopUser : IdentityUser
    {
        public String Id { get; set; }  //was be comented 
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
//Abcd!123456