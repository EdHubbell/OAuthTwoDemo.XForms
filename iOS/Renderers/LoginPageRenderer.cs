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

