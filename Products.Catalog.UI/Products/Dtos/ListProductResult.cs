namespace Products.Catalog.UI.Products.Dtos
{
    public class ListProductResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public CategoryForProductResult Category { get; set; }
    }
}
