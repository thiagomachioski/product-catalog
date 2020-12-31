namespace Products.Catalog.UI.Categories.Dtos
{
    public class ListProductForCategoryResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
