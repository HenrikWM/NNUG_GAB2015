using System;
using System.Configuration;
using System.Diagnostics;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

namespace GAB.Infrastructure.Azure
{
    public static class Config
    {
        private const string NewLine = "\r\n";

        public static CloudStorageAccount GetCloudStorageAccount(string name)
        {
            var key = ConfigurationString(name);
            try
            {
                return CloudStorageAccount.Parse(key);
            }
            catch (Exception e)
            {
                Trace.WriteLine(string.Format("{0}An error occurred: {1}", NewLine, e.Message));
                throw;
            }
        }

        private static string ConfigurationString(string key)
        {
            if (RoleEnvironment.IsAvailable)
            {
                return CloudConfigurationManager.GetSetting(key);
            }

            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
    }
}

