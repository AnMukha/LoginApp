namespace LoginApp.Services.Identity.Dtos
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
