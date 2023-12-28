namespace WebAppDemo3.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderTime { get; set; }
        public double TotalPrice { get; set; }
        //public ICollection<Cart> Cart{ get; set; }
    }
}
