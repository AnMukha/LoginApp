using System.Text.Json.Serialization;

namespace LoginApp.ConsoleClient.Dtos
{
    public class Credentials
    {        
        public string? UserName { get; set; }        
        public string? Password { get; set; }
    }
}
