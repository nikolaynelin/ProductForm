using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Valtech.ProductForm.Models
{
    public class ProductVm
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public CategoryVm Category { get; set; }
        [Range(0.1, 99999999)]
        public decimal Price { get; set; }

        public List<CategoryVm> Categories { get; set; }
    }
}