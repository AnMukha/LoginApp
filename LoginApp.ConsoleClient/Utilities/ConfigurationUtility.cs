namespace LoginApp.ConsoleClient.Utilities
{
    public static class ConfigurationUtility
    {
        const string baseUrl = "https://LoginApp.com";

        public static string GetBaseURL(string[] args)
        {
#if DEBUG
            if (args.Length != 0)
            {
                Console.WriteLine($"Builded in Debug Mode. Base URL: {args[0]}");
                return args[0];
            }
#endif
            return baseUrl;
        }
    }
}
