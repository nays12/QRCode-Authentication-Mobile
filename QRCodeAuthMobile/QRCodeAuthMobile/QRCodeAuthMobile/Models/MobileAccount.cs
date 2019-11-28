using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRCodeAuthMobile.Models
{
	[Table("MobileAccounts")]
	public class MobileAccount
	{
		public MobileAccount()
		{
		}

		//Primary Key
		[PrimaryKey]
		public string MobileId { get; set; }
		public bool IsActive { get; set; }
	}
}
