using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWithJWT.Models;

public class Cart
{

	public Guid CartId { get; set; }
	public Guid CustomerId { get; set; }
	[ForeignKey("CustomerId")]
	public Customer? Customer { get; set; }//1-1
	public List<Product>? Products { get; set; }//1-many
	public int Quantity { get; set; }
}
