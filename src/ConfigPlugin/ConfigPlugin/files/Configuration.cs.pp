using ConfigPlugin.Interfaces;

namespace $rootnamespace$.Config
{
    public class Configuration : IConfiguration
    {
        public string CurrentConfiguration
        {
            get
            {
#if __PROD
                return "Prod";
#elif __TEST
                return "Test";
#else
                return "Dev";
#endif
            }
        }

        // Create a property for each application configuration.

        public string UrlWebService { get; set; }

        public string CloudDataBaseConnectionString { get; set; }

        public string PushNotificationService { get; set; }

        public string UrlAnalyticsService { get; set; }
    }
}
