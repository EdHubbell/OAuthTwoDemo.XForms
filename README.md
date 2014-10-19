## A simple demo of Xamarin.Auth in Xamarin.Forms

### Forked to focus on Azure AD authentication

Getting Azure AD authentication to run in a Xamarin forms app using Xamarin.Auth can be challenging.  At least is has been for me.  There are 100 ways for it not to work, of which I have found several.  This seems to be working, although at present it only allows you to authenticate.  Not actually calling any services with the token sent back.

This example connects to a mobile service at http://byodir.azure-mobile.net/

As of 10/19/2014, this demo works using a usename of tuser@gsdware.com and a password of FooBar1212.  Yes, the client ID is right there in the code sitting up on github.  I wanted to make it easy for other programmers to clone and test this, as I'm still not happy with the structure and the code (first real week of Xamarin dev work - Cut me some slack).  Eventually I'll cycle back thru and make this a more generic example.  Or I won't. 

Both the iOS and Android implementations seem to be working (at the moment).  

### AzureAdOAuth2Authenticator

Another thing I found I had to do for Azure AD was to override Xamarin.Auth.OAuth2Authenticator with AzureAdOAuth2Authenticator so we could look for 'token' instead of the hard coded 'access_token'.  

The fact that there are 2 instances of AzureAdOAuth2Authenticator.cs (one for each platform) bothers me greatly.  I'm new to Xamarin development, so I'm certain there is a better way to do this.  I just don't know what it is.  Probably changing Xamarin.Auth to accept settings for all the string constants in there.

I also had to change the renderers for both to also look for 'token'.  Seems like too many places to be editing strings, yeah?  Well, it is an example, not foundational code or anything.  I think changes to Xamarin.Auth would be a better place to address issues like these.

### What About MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory?


I've seen examples on Azure sites that claim you can just use the WindowsAzureActiveDirectory MobileServiceAuthenticationProvider - Like so:

```
try
  {
    user = await client.LoginAsync(view, MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory);
  }
```

All the code I have says WindowsAzureActiveDirectory isn't a member of MobileServiceAuthenticationProvider.  So I think the example here is garbage:
http://azure.microsoft.com/en-us/documentation/articles/partner-xamarin-mobile-services-ios-get-started-users/


### Getting Mobile Sevice Set Up

Follow the instructions here to link a mobile service to your Azure Active Directory:

http://azure.microsoft.com/en-us/documentation/articles/mobile-services-how-to-register-active-directory-authentication/

Be sure you choose Web Service and not Native (although in the future, if we could nail down native using a Javascript Mobile Service, I'm all for it).



### Now Back to the Pre-Forked Readme.md:


This demo does not cover pulling back any profile data (or any other data) from your given auth provider's API (Facebook, Instagram, Twitter, Google, etc, etc, etc), but it will get you authenticated. After successful authentication, it's up to you to make subsequent calls against your chosen API with the auth token you receive.

### Edit the App.cs class in the core project:
You'll need to edit the following in the App.cs in order to get the sample working with your OAuth provider:

    _Instance.OAuthSettings = 
        new OAuthSettings (
            clientId: "",       // your OAuth2 client id 
            scope: "",          // The scopes for the particular API you're accessing. The format for this will vary by API.
            authorizeUrl: "",   // the auth URL for the service
            redirectUrl: "");   // the redirect URL for the service
            
            // If you'd like to know more about how to integrate with an OAuth provider, 
            // I personally like the Instagram API docs: http://instagram.com/developer/authentication/

### What you get:

![Xamarin.Auth with Xamarin.Forms iOS example](http://www.joesauve.com/content/images/2014/Jun/XamarinAuthXamarinFormsExample-1.gif)
![Xamarin.Auth with Xamarin.Forms Android example](http://www.joesauve.com/content/images/2014/Jun/Xamarin-Auth_Xamarin-Forms_example_Android.gif)
