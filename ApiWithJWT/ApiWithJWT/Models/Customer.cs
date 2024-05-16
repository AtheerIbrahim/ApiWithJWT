using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWithJWT.Models;

public class Customer : Account
{
	public Cart? Cart { get; set; }

	public List<Order> Orders { get; set; }
}
