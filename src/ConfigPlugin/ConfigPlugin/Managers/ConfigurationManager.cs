using ConfigPlugin.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConfigPlugin.Managers
{
    public static class ConfigurationManager
    {
        #region Attributes

        private static Assembly appAssembly;
        private static string assemblyName;
        private static string configurationClassName;
        private static Type configurationClassType;
        private static IConfiguration configuration;
        private static bool initialized = false;

        #endregion Attributes

        #region Properties

        public static IConfiguration Configuration
        {
            get
            {
                if (!initialized)
                {
                    throw new Exception("You must call the Init method before using the Configuration.");
                }

                if (configuration == null)
                {
                    if (configurationClassType == null)
                    {
                        LoadConfigurationClassType();
                    }

                    LoadConfigurationFile();
                }

                return configuration;
            }
        }

        #endregion Properties

        #region Methods

        public static void Init(Application application)
        {
            try
            {
                appAssembly = application.GetType().GetTypeInfo().Assembly;
                LoadConfigurationClassType();
                LoadConfigurationFile();
                initialized = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        private static void LoadConfigurationClassType()
        {
            assemblyName = appAssembly.GetName().Name;
            configurationClassName = string.Format("{0}.Config.Configuration", assemblyName);
            configurationClassType = appAssembly.GetType(configurationClassName);
            if (configurationClassType == null)
            {
                throw new Exception("Configuration class not found.");
            }
        }

        public static void LoadConfigurationFile()
        {
            try
            {
                configuration = (IConfiguration)Activator.CreateInstance(configurationClassType);
                if (configuration != null)
                {
                    string fileName = string.Format("{0}.Config.{1}.json", assemblyName, configuration.CurrentConfiguration);
                    using (Stream stream = appAssembly.GetManifestResourceStream(fileName))
                    {
                        if (stream == null)
                        {
                            throw new Exception(string.Format("Configuration file not found ({0}). Verify that the configuration file has the Build Action property in Embedded Resource.", fileName));
                        }

                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string content = reader.ReadToEnd();
                            configuration = (IConfiguration)JsonConvert.DeserializeObject(content, configurationClassType);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion Methods
    }
}
