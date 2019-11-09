using System;
using SQLite;

namespace QRCodeAuthMobile.Models
{
	[Table("credentials")]
	public class Credential
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string name { get; set; }
		public string issuer { get; set; }
		public DateTime issue_date { get; set; }
		public DateTime expiration_date { get; set; }
		public string value { get; set; }
		public bool is_valid { get; set; }
	}
}
