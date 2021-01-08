﻿namespace Products.Catalog.UI.Products.Dtos
{
    public class ProductCreateResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public CategoryForProductResult Category { get; set; }
    }
}
