using Shop.Areas.Identity.Data;
using Shop.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Data.Models
{
    public class Wallet
    {

        //public int Id { get; set; }
        public string Id { get; set; }
        public string WalletId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } //Amount
        [DataType(DataType.DateTime)]
        public DateTime CreateOn { get; set; }
        public string costumerId { get; set; }
        //[Column(TypeName = "decimal(18,2)")]
        //public decimal Balance { get; set; }
        

    }

}
