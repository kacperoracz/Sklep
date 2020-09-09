namespace Sklep.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
    }
}
