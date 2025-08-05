namespace WebSıtesı.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }

        // Ürüne ait yorumlar
        public List<Comment> Comments { get; set; }
    }
}
