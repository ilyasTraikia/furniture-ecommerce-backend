using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    public class Cart_Items
    {



        // The reference for the cart
        public long CartIdRef { get; set; }
        [ForeignKey("CartIdRef")]
        public Cart Cart { get; set; }






        // The reference for the Product
        public long ProductIdRef { get; set; }
        [ForeignKey("ProductIdRef")]
        public Product product { get; set; }












        public long Price { get; set; }

        public long Quantity { get; set; }


    }
}
