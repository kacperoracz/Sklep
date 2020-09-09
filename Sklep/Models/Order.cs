namespace Sklep.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public float? Price { get; set; }
        public int StatusId { get; set; }
    }
}
