using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Data.Models
{
    public class CreditCard
    {
        public int CardId { get; set; }
        [Display(Name = "Enter Card Number")]
        //[MaxLength(16), MinLength(16)]
        [Required(ErrorMessage = "Card Number Error")]
        [MinLength(16), MaxLength(16)]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Card Number expiration")]
        public string Expiration { get; set; }
        [Display(Name = "Enter Cvv")]
        [MaxLength(4), MinLength(4)]
        [Required(ErrorMessage = "Card Number Error")]
        
        public string CVV { get; set; }
        [Display(Name = "Enter Card Name")]
        [StringLength(25)]
        [Required(ErrorMessage = "Card Name Error")]
        public string CardName { get; set; }
        public string UserId { get; set; }
    }
}
