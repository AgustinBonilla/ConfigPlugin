# ConfigPlugin for Xamarin Applications

![ConfigPlugin Logo](https://raw.githubusercontent.com/AgustinBonilla/ConfigPlugin/master/art/icon.png)

Config plugin for Xamarin.Forms, Xamarin.Android and Xamarin.iOS applications.

This plugin allows to define the settings of the application for each environment in which it will run. This plugin uses json files to maintain the settings, so a file will be configured for each environment.

For example: three json files are configured, one with the development environment settings, another with the testing environment settings and finally one with the production environment settings.

### Setup
* Available on NuGet: http://www.nuget.org/
* Install into your PCL project.


### API Usage

**Initialize the ConfigurationManager**
Initialize the **ConfigurationManager** component when the application starts.
```csharp
public App()
{
    InitializeComponent();

    // Initialize the ConfigurationManager component when the application starts.
    ConfigurationManager.Init(this);

    MainPage = new ConfigPluginSample.MainPage();
}
```

**Implement Configuration class**
CurrentConfiguration must be implemented, using build directives to link the json files for each enviroment.
Create a property for each application configuration.
```csharp
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
```

**Create configurations in json files**
The configurations must be created in each of the json files, the properties defined in the Configuration class must be exactly the same as those defined in the json files.

Dev.json
```json
{
  "UrlWebService": "https://myservicedev.azurewebsites.net",
  "CloudDataBaseConnectionString": "myserverdev.database.windows.net",
  "PushNotificationService": "notificationhubdev.servicebus.windows.net",
  "UrlAnalyticsService": "https://mobile.azure.com/apps/appdev"
}
```

Test.json
```json
{
  "UrlWebService": "https://myservicetest.azurewebsites.net",
  "CloudDataBaseConnectionString": "myservertest.database.windows.net",
  "PushNotificationService": "notificationhubtest.servicebus.windows.net",
  "UrlAnalyticsService": "https://mobile.azure.com/apps/apptest"
}
```

Prod.json
```json
{
  "UrlWebService": "https://myserviceprod.azurewebsites.net",
  "CloudDataBaseConnectionString": "myserverprod.database.windows.net",
  "PushNotificationService": "notificationhubprod.servicebus.windows.net",
  "UrlAnalyticsService": "https://mobile.azure.com/apps/appprod"
}
```

**Access to Configurations**
Get the configurations defined for the running environment.
```csharp
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        // Get the configurations defined for the running environment.

        lblUrlWebService.Text = ((Configuration)ConfigurationManager.Configuration).UrlWebService;

        lblCloudDataBaseConnectionString.Text = ((Configuration)ConfigurationManager.Configuration).CloudDataBaseConnectionString;

        lblPushNotificationService.Text = ((Configuration)ConfigurationManager.Configuration).PushNotificationService;

        lblUrlAnalyticsService.Text = ((Configuration)ConfigurationManager.Configuration).UrlAnalyticsService;
    }
}
```

#### Important:
* Configuration class and json files must be in the folder {PCL_Project}\Config\.
* Json files must be configured with Build Action Embedded Resource.

#### Possible exceptions:
* You must call the Init method before using the Configuration. You need to initialize the ** ConfigurationManager ** component when the application starts.
* Configuration class not found. You must validate that the Configuration class has been implemented and is located in {PCL_Project}\Config\.
* Configuration file not found ({0}). Verify that the configuration file has the Build Action property in Embedded Resource. You must validate that the json files are configured with Build Action Embedded Resource and that their names correspond to those in the Configuration class with the build directives.

#### Contributions
Contributions are welcome! If you find a bug please report it and if you want a feature please report it.
