using ConfigPlugin.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ConfigPluginSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Initialize the ConfigurationManager component when the application starts.
            ConfigurationManager.Init(this);

            MainPage = new ConfigPluginSample.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
