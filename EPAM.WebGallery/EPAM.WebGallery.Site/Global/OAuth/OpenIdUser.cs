﻿using System;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;

namespace EPAM.WebGallery.Site.Global.OAuth
{
	public class OpenIdUser
	{
		public OpenIdUser(ClaimsResponse claim, string identifier)
		{
			addClaimInfo(claim, identifier);
		}

		public string Email { get; set; }
		public string Nickname { get; set; }
		public string FullName { get; set; }
		public bool IsSignedByProvider { get; set; }
		public string ClaimedIdentifier { get; set; }

		private void addClaimInfo(ClaimsResponse claim, string identifier)
		{
			Email = claim.Email;
			FullName = claim.FullName;
			Nickname = claim.Nickname ?? claim.Email;
			IsSignedByProvider = claim.IsSignedByProvider;
			ClaimedIdentifier = identifier;
		}

		private void populateFromDelimitedString(string data)
		{
			if (data.Contains(";"))
			{
				string[] stringParts = data.Split(';');
				if (stringParts.Length > 0) Email = stringParts[0];
				if (stringParts.Length > 1) FullName = stringParts[1];
				if (stringParts.Length > 2) Nickname = stringParts[2];
				if (stringParts.Length > 3) ClaimedIdentifier = stringParts[3];
			}
		}

		public override string ToString()
		{
			return String.Format("{0};{1};{2};{3}", Email, FullName, Nickname, ClaimedIdentifier);
		}
	}
}