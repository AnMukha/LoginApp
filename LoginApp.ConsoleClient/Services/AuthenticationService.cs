using LoginApp.ConsoleClient.Dtos;
using System.Text.Json;
using System.Text;

namespace LoginApp.ConsoleClient.Services
{
    public class AuthenticationService(HttpClient client, string baseUrl)
    {
        private const string apiLoginPath = "/api/login";
        private readonly HttpClient _client = client;
        private readonly string _baseUrl = baseUrl;

        public async Task<(LoginResponse?, string? errorMessage)> AuthenticateUserAsync(string userName, string password)
        {
            var credentials = new Credentials() { UserName = userName, Password = password };
            var json = JsonSerializer.Serialize(credentials);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync(new Uri(_baseUrl + apiLoginPath), content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return (JsonSerializer.Deserialize<LoginResponse>(responseBody), null);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();                    
                    return (null, $"Reason phrase: {response.ReasonPhrase} {errorContent}");
                }
            }
            catch (HttpRequestException e)
            {
                return (null, e.Message);
            }            
        }

    }    
}
