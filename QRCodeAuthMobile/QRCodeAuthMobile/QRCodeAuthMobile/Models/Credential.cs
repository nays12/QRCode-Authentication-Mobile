using System;
using SQLite;

namespace QRCodeAuthMobile.Models
{
	[Table("Credentials")]
	public class Credential
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public CredentialType CredentialType { get; set; }
		public virtual Account Issuer { get; set; }
		public virtual Account Owner { get; set; }
		public DateTime IssueDate { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string Value { get; set; }
		public bool IsValid { get; set; }

		public Credential()
		{
		}

		public Credential(int id, string name, CredentialType credentialType, Account issuer, Account owner, DateTime issueDate, DateTime expirationDate, string value, bool isValid)
		{
			Id = id;
			Name = name;
			CredentialType = credentialType;
			Issuer = issuer;
			Owner = owner;
			IssueDate = issueDate;
			ExpirationDate = expirationDate;
			Value = value;
			IsValid = isValid;
		}
		public override string ToString()
		{
			return base.ToString();
		}
	}
}
