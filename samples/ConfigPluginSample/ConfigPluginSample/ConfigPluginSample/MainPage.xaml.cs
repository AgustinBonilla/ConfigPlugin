using ConfigPlugin.Managers;
using ConfigPluginSample.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConfigPluginSample
{
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
}
