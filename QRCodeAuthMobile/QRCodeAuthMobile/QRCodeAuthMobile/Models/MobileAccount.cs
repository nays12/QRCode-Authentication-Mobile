/*
 * Purpose: 
 * This is a model class for a MobileAccount object that has data annotations for its associated SQLite Table
 * 
 * Contributors: 
 * Naomi Wiggins 
 * 
 */

using SQLite;

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
