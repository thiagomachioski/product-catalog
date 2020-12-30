namespace Products.Catalog.ViewModels.ProductViewModels
{
    public class ListProductViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public CategoryForProductViewModel Category { get; set; }
    }
}
