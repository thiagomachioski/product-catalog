using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Products.Catalog.UI.Extensions
{
    public static class  ModelStateExtension
    {
        public static Dictionary<string, string[]> GetErrors(this ModelStateDictionary modelState) =>
            modelState.Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    e => e.Key,
                    e => e.Value.Errors.Select(i => i.ErrorMessage).ToArray()
                );
    }
}