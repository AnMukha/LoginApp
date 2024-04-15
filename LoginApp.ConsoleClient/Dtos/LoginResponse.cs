using System.Text.Json.Serialization;

namespace LoginApp.ConsoleClient.Dtos
{
    public class LoginResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
