using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Data.Models
{
    public class Order
    {
        [BindNever]
        public int Id { get; set; }
        [Display(Name = "Enter Your Name")]
        [StringLength(25)]
        [Required(ErrorMessage = "Name Length Error")]
        public string Name { get; set; }
        [Display(Name = "Enter Your Surname")]
        [StringLength(25)]
        [Required(ErrorMessage = "Surame Length Error")]
        public string Surname { get; set; }
        [Display(Name = "Enter Your Adress")]
        [StringLength(35)]
        [Required(ErrorMessage = "Adress Length Error")]
        public string Adress { get; set; }
        [Display(Name = "Enter Your Phone Number")]
        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Number Length Error")]
        public string Phone { get; set; }
        [Display(Name = "Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(15)]
        [Required(ErrorMessage = "Email Length Error")]
        public string Email { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }
        public List <OrderDetail> OrdeDetails { get; set; }
    }
}
