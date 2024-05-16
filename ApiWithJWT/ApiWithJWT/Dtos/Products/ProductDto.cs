using ApiWithJWT.Dtos.Categories;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiWithJWT.Models;
using ApiWithJWT.Dtos.SubCategories;
using ApiWithJWT.Dtos.Carts;

namespace ApiWithJWT.Dtos.Products;

public class ProductDto
{

	public Guid ProductId { get; set; }


	public required string Name { get; set; }

	public required string Description { get; set; }


	public decimal Price { get; set; }

	public int QuantityAvailable { get; set; }

	// Foreign key property for category
	public Guid CategoryId { get; set; }

	public CategoryDto Category { get; set; }
	// Foreign key property for subcategory
	public Guid SubcategoryId { get; set; }
	public SubcategoryDto SubCategory { get; set; }
	public CartDto Cart { get; set; }
	public Guid? CartId { get; set; }
}
