using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRCodeAuthMobile.Models
{
	[Table("events")]
	public class Event
	{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }
		public string owner { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public string details { get; set; }
		public DateTime start_time { get; set; }
		public DateTime end_time { get; set; }
		public List<Credential> credentials_captured { get; set; }
	}
}
