using System;
using System.Collections.Generic;
using System.Text;

namespace QRCodeAuthMobile.Models
{
	public class Account
	{
		public string AccountId { get; set; }
		public AccountType AccountType { get; set; }
		public string Department { get; set; }
		public bool IsActive { get; set; }
		public bool IsCredentialAuthority { get; set; }
		public bool IsAttendanceManager { get; set; }
		public bool IsInformationCollector { get; set; }
		public virtual User AccountOwner { get; set; }
		public virtual List<Credential> CredentialsOwned { get; set; }
		public virtual List<Event> EventsOwned { get; set; }

		public Account()
		{
		}

		public Account(string accountId, AccountType accountType, string department, bool isActive, bool isCredentialAuthority, bool isAttendanceManager, bool isInformationCollector, User accountOwner, List<Credential> credentialsOwned, List<Event> eventsOwned)
		{
			AccountId = accountId;
			AccountType = accountType;
			Department = department;
			IsActive = isActive;
			IsCredentialAuthority = isCredentialAuthority;
			IsAttendanceManager = isAttendanceManager;
			IsInformationCollector = isInformationCollector;
			AccountOwner = accountOwner;
			CredentialsOwned = credentialsOwned;
			EventsOwned = eventsOwned;
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}
