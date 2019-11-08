using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRCodeAuthMobile.Models
{
	[Table("users")]
	class User
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string userId { get; set; }
		public string last_name { get; set; }
		public string first_name { get; set; }
		public string group { get; set; }

	}
}
