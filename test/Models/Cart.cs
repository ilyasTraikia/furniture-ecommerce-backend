using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    public class Cart
    {



        public long CartId { get; set; }



        public string UserIdRef { get; set; }
        [ForeignKey("UserIdRef")]
        public User user { get; set; }



        public string status { get; set; }

        public ICollection<Cart_Items> CartItems { get; set; }



    }
}
