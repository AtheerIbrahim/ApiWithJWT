using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWithJWT.Models;

public class Subcategory
{
	[Key]
	public Guid SubcategoryId { get; set; }


	[MaxLength(100)]
	public  string Name { get; set; }

	public Guid CategoryId { get; set; }

	[ForeignKey("CategoryId")]
	public virtual Category? Category { get; set; }
	public List<Product>? Products { get; set; }
}

