using MessagePack;
using Stripe;

namespace test.Models
{
    public class Product
    {

        public Product(long Id,string Name,string Description,string Category,long Price,string Additional_inf,int Quantity) {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Category = Category;
            this.Price = Price;
            this.Additional_inf = Additional_inf;
            this.Quantity = Quantity;

        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public long Price { get; set; }
        public string Additional_inf { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }


        




    }
}
