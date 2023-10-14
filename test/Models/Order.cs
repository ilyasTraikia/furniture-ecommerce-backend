using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    public class Order
    {

        public long OrderId { get; set; }




       // the ref for product
        public long ProductIdRef { get; set; }
        [ForeignKey("ProductIdRef")]
        public Product product { get; set; }




        // The ref for user
        public string UserIdRef { get; set; }
        [ForeignKey("UserIdRef")]
        public User user { get; set; }



        public int Quantity { get; set; }
        
    }
}
