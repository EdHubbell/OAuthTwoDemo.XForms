using System;
using Xamarin.Auth;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using OAuthTwoDemo.XForms;
using OAuthTwoDemo.XForms.iOS;

[assembly: ExportRenderer (typeof (LoginPage), typeof (LoginPageRenderer))]


namespace OAuthTwoDemo.XForms.iOS
{
	public class LoginPageRenderer : PageRenderer
	{

		bool done = false;

		public override void ViewDidAppear (bool animated)
		{
			if (!done) {
			
				base.ViewDidAppear (animated);


				var auth = new AzureAdOAuth2Authenticator (
					          clientId: App.Instance.OAuthSettings.ClientId, // your OAuth2 client id
					          scope: App.Instance.OAuthSettings.Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
					          authorizeUrl: new Uri (App.Instance.OAuthSettings.AuthorizeUrl), // the auth URL for the service
					          redirectUrl: new Uri (App.Instance.OAuthSettings.RedirectUrl)); // the redirect URL for the service
								

				auth.Completed += (sender, eventArgs) => {
					// We presented the UI, so it's up to us to dimiss it on iOS.
					//DismissViewController(true, null);
					App.Instance.SuccessfulLoginAction.Invoke ();


					if (eventArgs.IsAuthenticated) {
						// eventArgs	{Xamarin.Auth.AuthenticatorCompletedEventArgs}	Xamarin.Auth.AuthenticatorCompletedEventArgs
						//Account	{__username__=&token=%7B%22user%22%3A%7B%22userId%22%3A%22Aad%3AyX1sEIBPqlE6yz098wFy_eZdzI618W1BLFzIiiwQFhI%22%7D%2C%22authenticationToken%22%3A%22eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjAifQ.eyJleHAiOjE0MTYzMjg3NjIsImlzcyI6InVybjptaWNyb3NvZnQ6d2luZG93cy1henVyZTp6dW1vIiwidmVyIjoyLCJhdWQiOiJBYWQiLCJ1aWQiOiJBYWQ6eVgxc0VJQlBxbEU2eXowOTh3RnlfZVpkekk2MThXMUJMRnpJaWl3UUZoSSIsInVybjptaWNyb3NvZnQ6Y3JlZGVudGlhbHMiOiJra0oyOEJIMDRQMkR2QkdqQkRJUDZKKzRxWTdwdkJQN1VLcndMRG9VdHR4NTlwa2ZkVlM3L3VWWHpERGlaczVZeS96b3hacGorckhWSFNlN2dXVFU0dDAvK25EZk1hNkxJbmF2QStVWlZyOWszeWwxaC8wb2M5MS9idkFSdkZBTkNWcW5vTFlHQlB0c0ZYVmd3WUVJYkE9PSJ9.C38kB3u9Bipa4Y-KW4hACe9COlRR-kKx3OC_PGI5eGE%22%7D}	Xamarin.Auth.Account
						// Use eventArgs.Account to do wonderful things
						App.Instance.SaveToken (eventArgs.Account.Properties ["token"]);
					} else {
						// The user cancelled
					}
				};
		

				done = true;
				PresentViewControllerAsync (auth.GetUI (), true);
			}
		}
	}
}

