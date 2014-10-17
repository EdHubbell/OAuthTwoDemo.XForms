using System;
using Xamarin.Forms;

namespace OAuthTwoDemo.XForms
{
	public class App
	{
		// just a singleton pattern so I can have the concept of an app instance
		static volatile App _Instance;
		static object _SyncRoot = new Object();

		public static App Instance
		{
			get 
			{
				if (_Instance == null) 
				{
					lock (_SyncRoot) 
					{
						// ySyAtylYsAqdNPbRksHnXbecCemPBG68 Application Key
						// ClbWoaBfqVBbCFadAkLpFWSwOvXolw48 Master Key
						// https://login.windows.net/gsdware.onmicrosoft.com this needs to go somewhere....

						//https://byodir.azure-mobile.net/login/done#token=%7B%22user%22%3A%7B%22userId%22%3A%22Aad%3A1sJA7m5epswV0DRRF2t-tiG-TYXi4J7Hpfs3KCr87zo%22%7D%2C%22authenticationToken%22%3A%22eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjAifQ.eyJleHAiOjE0MTYwNzMxNzIsImlzcyI6InVybjptaWNyb3NvZnQ6d2luZG93cy1henVyZTp6dW1vIiwidmVyIjoyLCJhdWQiOiJBYWQiLCJ1aWQiOiJBYWQ6MXNKQTdtNWVwc3dWMERSUkYydC10aUctVFlYaTRKN0hwZnMzS0NyODd6byIsInVybjptaWNyb3NvZnQ6Y3JlZGVudGlhbHMiOiJra0oyOEJIMDRQMkR2QkdqQkRJUDZKKzRxWTdwdkJQN1VLcndMRG9VdHR4NTlwa2ZkVlM3L3VWWHpERGlaczVZTHk4Z0ZKRXdPczM5b042WEpXaHVvSUpkaDdMNjhVSlIzK2dNcWF0emV4Yk9oVHZRMjNOU1ZrMm9GSkc0bE5BSExPcXQ3UFVRTFp0Z2lNdjBhTlg0ZWc9PSJ9.KLJByu9QmOSeHQk8EDp-1vXvJPRJH4vBcRQ4ap1faYg%22%7D
						if (_Instance == null) {
							_Instance = new App ();
							_Instance.OAuthSettings = 
								new OAuthSettings ( 
									clientId: "e820c0de-52b5-49db-942e-c9f14c341c91",  		// your OAuth2 client id 
									scope: "",  		// The scopes for the particular API you're accessing. The format for this will vary by API.
									authorizeUrl: "https://byodir.azure-mobile.net/login/aad",  	// the auth URL for the service
									redirectUrl: "https://byodir.azure-mobile.net/login/done");   // the redirect URL for the service

							        // If you'd like to know more about how to integrate with an OAuth provider, 
									// I personally like the Instagram API docs: http://instagram.com/developer/authentication/
						}
					}
				}

				return _Instance;
			}
		}

		public OAuthSettings OAuthSettings { get; private set; }

		NavigationPage _NavPage;

		public Page GetMainPage ()
		{
			var profilePage = new ProfilePage();

			_NavPage = new NavigationPage(profilePage);

			return _NavPage;
		}

		public bool IsAuthenticated {
			get { return !string.IsNullOrWhiteSpace(_Token); }
		}

		string _Token;
		public string Token {
			get { return _Token; }
		}

		public void SaveToken(string token)
		{
			_Token = token;

			// broadcast a message that authentication was successful
			MessagingCenter.Send<App> (this, "Authenticated");
		}

		public Action SuccessfulLoginAction
		{
			get {
				return new Action (() => _NavPage.Navigation.PopModalAsync ());
			}
		}
	}
}

