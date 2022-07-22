using System;
namespace Shop.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }     
        public string ShortDesc { get; set; }                               //<--short description 
        public string LongDesc { get; set; }                                // <--long description
        public string Img { get; set; }                                     //<--- image url
        public ushort Price { get; set; }
        public bool IsFavourite { get; set; }
        public bool Available { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

    }
}
