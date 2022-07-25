using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
	//imzalama anlamı taşımaktadır.
	public class SigningCredentialsHelper
		 
	{
		public SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
		{
			return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
		}
	}
}
