using LoginApp.ConsoleClient.Dtos;
using System.Text.Json;
using System.Text;

namespace LoginApp.ConsoleClient.Services
{
    public class AuthenticationService
    {

        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public AuthenticationService(HttpClient client, string baseUrl)
        {
            _client = client;
            _baseUrl = baseUrl;
        }

        public async Task<LoginResponse?> AuthenticateUserAsync(string userName, string password)
        {
            var credentials = new Credentials() { UserName = userName, Password = password };
            var json = JsonSerializer.Serialize(credentials);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync(new Uri(_baseUrl + "api/login"), content);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<LoginResponse>(responseBody);
                }
                else
                {
                    Console.WriteLine($"Failed to authenticate. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Message :{e.Message}");
            }
            return null;
        }

    }    
}
