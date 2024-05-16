using System.ComponentModel.DataAnnotations;

namespace ApiWithJWT.Dtos.SubCategories;

public class CreateSubcategoryDto
{
	[Required]
	[MaxLength(100)]
	public required string Name { get; set; }

	public Guid CategoryId { get; set; }
}
