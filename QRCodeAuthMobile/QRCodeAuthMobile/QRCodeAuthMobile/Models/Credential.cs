/*
 * Purpose: 
 * This is a model class for a Credential object that has data annotations for its associated SQLite Table
 * 
 * Contributors: 
 * Naomi Wiggins 
 * 
 */

using System;
using SQLite;

namespace QRCodeAuthMobile.Models
{
	[Table("Credentials")]
	public class Credential
	{
		public Credential()
		{
		}

		// Primary Key
		[PrimaryKey]
		public int CredentialId { get; set; }
		public string Name { get; set; }
		public CredentialType CredentialType { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string Value { get; set; }
		public bool IsValid { get; set; }

		// Foreign Keys
		public string Owner { get; set; }
		public string Issuer { get; set; }
	}
}
