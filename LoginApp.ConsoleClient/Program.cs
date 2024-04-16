using LoginApp.ConsoleClient.Services;
using LoginApp.ConsoleClient.Utilities;

var httpClient = new HttpClient();
var appBaseUrl = ConfigurationUtility.GetBaseURL(args);
var (userName, password) = ConsoleInteraction.GetCredentials();

var authService = new AuthenticationService(httpClient, appBaseUrl);

var (loginResponse, errorMessage) = await authService.AuthenticateUserAsync(userName, password);

if (loginResponse == null)
{    
    ConsoleInteraction.DisplayLoginError(errorMessage!); // we show some restricted information about the error to let user understand what's wrong
}
else if (!loginResponse.Success || loginResponse.Token == null)
{    
    ConsoleInteraction.DisplayLoginFailure(); // we show just general message without any details to make login procedure more seсure
}
else 
{
    ConsoleInteraction.DisplayLoginSuccess(loginResponse);

    var postLoginService = new PostLoginService(httpClient, appBaseUrl);
    var postSuccess = await postLoginService.SendLoginInfoAsync(loginResponse.Token);    
    if (postSuccess) 
    {
        ConsoleInteraction.DisplayPostLoginSuccess();
    }
    else
    {
        ConsoleInteraction.DisplayPostLoginFailure(); // we don't show error details here, because we guess that user is not interested in details of the post-login activity
    }
}

Console.Read();

