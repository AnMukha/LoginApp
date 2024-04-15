using LoginApp.ConsoleClient.Services;
using LoginApp.ConsoleClient.Utilities;

var httpClient = new HttpClient();
var appBaseUrl = ConfigurationUtility.GetBaseURL(args);
var (userName, password) = ConsoleInteraction.GetCredentials();

var authService = new AuthenticationService(httpClient, appBaseUrl);

var loginResponse = await authService.AuthenticateUserAsync(userName, password);

if (loginResponse!=null && loginResponse.Success && loginResponse.Token!=null)
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
        ConsoleInteraction.DisplayPostLoginFailure();
    }
}
else
{
    ConsoleInteraction.DisplayLoginFailure();
}

Console.Read();

