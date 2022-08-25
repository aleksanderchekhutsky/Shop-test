using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Data.Models
{
    public class WalletTransaction
    {
        public int Id { get; set; }
        public string customerId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurentBalance { get; set; }      //wel be balance
        
        public DateTime createdOn { get; set; }
        public decimal Amount { get; set; }
        public string OperationType { get; set; }   //addedd
    }
}
