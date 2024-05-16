using ApiWithJWT.Dtos.Categories;
using ApiWithJWT.Dtos.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWithJWT.Dtos.SubCategories
{
    public class SubcategoryDto
    {
        [Required]
        public Guid SubcategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        public Guid CategoryId { get; set; }

    }
}
