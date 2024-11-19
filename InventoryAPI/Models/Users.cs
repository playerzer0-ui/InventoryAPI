using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
	public class Users
	{
		public Guid UsersId { get; set; }

		[Required(ErrorMessage = "User name is required")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Range(0, 1)]
		public int UserType { get; set; }

	}
}
