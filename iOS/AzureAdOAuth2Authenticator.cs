using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Utilities;
using System.Net;
using System.Text;
using Xamarin.Auth;

namespace OAuthTwoDemo.XForms.iOS
{
	public class AzureAdOAuth2Authenticator : Xamarin.Auth.OAuth2Authenticator
	{
		bool IsImplicit { get { return AccessTokenUrl == null; } }

		public AzureAdOAuth2Authenticator (string clientId, string scope, Uri authorizeUrl, Uri redirectUrl, GetUsernameAsyncFunc getUsernameAsync = null) :
		base (clientId, scope, authorizeUrl, redirectUrl, getUsernameAsync) {}

		/// <summary>
		/// Raised when a new page has been loaded.
		/// </summary>
		/// <param name='url'>
		/// URL of the page.
		/// </param>
		/// <param name='query'>
		/// The parsed query string of the URL.
		/// </param>
		/// <param name='fragment'>
		/// The parsed fragment of the URL.
		/// </param>
		protected override void OnRedirectPageLoaded (Uri url, IDictionary<string, string> query, IDictionary<string, string> fragment)
		{

			//
			// Look for the access_token
			//
			if (fragment.ContainsKey("token")) {
				//
				// We found an access_token
				//
				OnRetrievedAccountProperties (fragment);
			} else {
				OnError ("Expected token in response, but did not receive one.");
				return;
			}
		}

	}
}

