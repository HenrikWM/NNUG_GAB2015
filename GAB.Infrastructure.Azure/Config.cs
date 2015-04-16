using System;
using System.Configuration;
using System.Diagnostics;

using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

namespace GAB.Infrastructure.Azure
{
    using GAB.Core;

    using Microsoft.Azure;

    public static class Config
    {
        public static CloudStorageAccount GetCloudStorageAccount(string name)
        {
            var key = ResolveStorageConnectionString(name);
            try
            {
                return CloudStorageAccount.Parse(key);
            }
            catch (Exception e)
            {
                Trace.WriteLine(
                    string.Format("{0}An error occurred: {1}", TraceLinePrefixer.GetConsoleLinePrefix(), e.Message));
                throw;
            }
        }

        private static string ResolveStorageConnectionString(string key)
        {
            if (RoleEnvironment.IsAvailable)
            {
                return CloudConfigurationManager.GetSetting(key);
            }
            
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        public static string GetConfigurationSetting(string key)
        {
            string value = string.Empty;

            if (RoleEnvironment.IsAvailable)
            {
                value = CloudConfigurationManager.GetSetting(key);
            }

            if (string.IsNullOrEmpty(value))
                value = ConfigurationManager.AppSettings[key];

            Trace.TraceInformation("{0}Fetched setting '{1}': '{2}'", TraceLinePrefixer.GetConsoleLinePrefix(), key, value);

            return value;
        }
    }
}

