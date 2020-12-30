using System;
using System.Collections.Generic;

namespace Products.Catalog.ViewModels.ProductViewModels
{
    public class FindProductByIdViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public CategoryForProductViewModel Category { get; set; }
    }
}
