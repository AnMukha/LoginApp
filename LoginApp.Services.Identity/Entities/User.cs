namespace LoginApp.Services.Identity.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public virtual string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public virtual string? Email { get; set; }        
    }
}
