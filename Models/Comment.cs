namespace WebSıtesı.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        // Hangi ürüne ait olduğunu gösteren alan
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
