using ConfigPlugin.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace ConfigPlugin.Managers
{
    /// <summary>
    /// Class to manage application configurations.
    /// </summary>
    public static class ConfigurationManager
    {
        #region Attributes

        /// <summary>
        /// Application assembly.
        /// </summary>
        private static Assembly appAssembly;

        /// <summary>
        /// Assembly name.
        /// </summary>
        private static string assemblyName;

        /// <summary>
        /// Configuration class name.
        /// </summary>
        private static string configurationClassName;

        /// <summary>
        /// Configuration class type.
        /// </summary>
        private static Type configurationClassType;

        /// <summary>
        /// Configuration.
        /// </summary>
        private static IConfiguration configuration;

        /// <summary>
        /// Configuration manager is initialized.
        /// </summary>
        private static bool initialized = false;

        #endregion Attributes

        #region Properties

        /// <summary>
        /// Get the current configuration for the running environment.
        /// </summary>
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

        /// <summary>
        /// Initialize the configuration plugin.
        /// </summary>
        /// <param name="application">Xamarin application instance.</param>
        public static void Init(object application)
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

        /// <summary>
        /// Load configuration class type.
        /// </summary>
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

        /// <summary>
        /// Load configuration file.
        /// </summary>
        private static void LoadConfigurationFile()
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
