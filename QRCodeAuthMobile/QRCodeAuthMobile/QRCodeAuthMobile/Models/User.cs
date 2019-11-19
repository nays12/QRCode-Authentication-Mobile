using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
