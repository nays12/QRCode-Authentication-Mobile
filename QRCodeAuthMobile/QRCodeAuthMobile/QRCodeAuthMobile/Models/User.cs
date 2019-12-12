/*
 * Purpose: 
 * This is a model class for a User object that has data annotations for its associated SQLite Table
 * 
 * Contributors: 
 * Naomi Wiggins 
 * 
 */

using SQLite;

namespace QRCodeAuthMobile.Models
{
	[Table("Users")]
	public class User
	{
		public User()
		{
		}

		// Primary Key
		[PrimaryKey]
		public string UserId { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public UserType UserType { get; set; }
	}
}
