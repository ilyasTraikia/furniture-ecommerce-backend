using MessagePack;

namespace test.Models
{
    public class Product
    {


        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public long Price { get; set; }
        public string Additional_inf { get; set; }

        public int Quantity { get; set; }


        




    }
}
