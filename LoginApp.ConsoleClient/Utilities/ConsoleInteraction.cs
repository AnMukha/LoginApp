using LoginApp.ConsoleClient.Dtos;

namespace LoginApp.ConsoleClient.Utilities
{
    public static class ConsoleInteraction
    {
        public static (string userName, string password) GetCredentials()
        {
            Console.Write("User name: ");
            var userName = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();
            return (userName!, password!);
        }
        public static void DisplayLoginSuccess(LoginResponse loginResponse)
        {            
            Console.WriteLine("Login successful");
            Console.WriteLine("User profile:");
            Console.WriteLine($"User name: {loginResponse!.UserName}");
            Console.WriteLine($"Email: {loginResponse!.Email}");
        }
        public static void DisplayLoginFailure()
        {
            Console.WriteLine("Failed to authenticate.");
        }
        public static void DisplayLoginError(string errorMessage)
        {
            Console.WriteLine("Failed to authenticate.");
            if (errorMessage != null)
            {
                Console.WriteLine("Error: " + errorMessage);
            }
        }

        public static void DisplayPostLoginSuccess()
        {
            Console.WriteLine("Post login information successfully sent.");
        }
        public static void DisplayPostLoginFailure()
        {
            Console.WriteLine("Failed to send post login information.");
        }
    }
}
