using System.Collections.Generic;

namespace Products.Catalog.UI.Categories.Dtos
{
    public class ListCategoryResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<ListProductForCategoryResult> Products { get; set; }

    }
}
