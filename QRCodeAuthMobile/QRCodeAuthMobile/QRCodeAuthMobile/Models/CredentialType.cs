/*
 * Purpose: 
 * This is an enumeration declaration for a CredentialType attribute that a Credential object can have
 * 
 * Contributors: 
 * Naomi Wiggins 
 * 
 */

namespace QRCodeAuthMobile.Models
{
	public enum CredentialType
	{
		Name = 0,
		Email = 1,
		IdNumber = 2,
		Birthdate = 3,
		Address = 4,
		Phone_Number = 5,
		Major = 6,
		Classification = 7,
		WorkTitle = 8
	}
}
