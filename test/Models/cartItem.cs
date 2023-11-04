using Stripe;

namespace test.Models
{
    public class cartItem
    {
        public long id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public long price { get; set; }

        public int quantity { get; set; }

       public string category { get; set; }

        public string additional_inf { get; set; }

        public int CartQuantity { get; set; }

        public int OriginalQuantity { get; set; }



        public override string ToString()
        {
            return $"Price is : {price}, Quantity is : {CartQuantity}";
        }


    }
}
