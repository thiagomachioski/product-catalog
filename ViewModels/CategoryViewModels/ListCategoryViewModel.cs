using System.Collections.Generic;

namespace Products.Catalog.ViewModels.CategoryViewModels
{
    public class ListCategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<ListProductForCategoryViewModel> Products { get; set; }

    }
}
