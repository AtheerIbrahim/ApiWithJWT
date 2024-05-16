using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiWithJWT.Dtos.Products;

public class CreateProductDto
{

	[Required]
	[MaxLength(100)]
	public  string Name { get; set; }

	public  string Description { get; set; }

	[Column(TypeName = "decimal(10, 2)")]
	public decimal Price { get; set; }


	// Foreign key property for category
	public Guid CategoryId { get; set; }

	// Foreign key property for subcategory
	public Guid SubcategoryId { get; set; }

	public Guid? CartId { get; set; }
}
