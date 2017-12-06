using PluginXforma59.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace PluginXforma59Test
{
    public class Configuration : IConfiguration
    {
        // Singleton
        private static Configuration _instance;
        private Configuration()
        {            
        }

        public NameValueCollection AppSettings => _instance.AppSettings;

        public System.Configuration.ConnectionStringSettingsCollection ConnectionStrings => ((IConfiguration)_instance).ConnectionStrings;

        public static Configuration Instance()
        {
            if (_instance == null) _instance = new Configuration();

            return _instance;
        }

        public T GetAppSettingsAs<T>(string key)
        {
            return ((IConfiguration)_instance).GetAppSettingsAs<T>(key);
        }

        public T GetDefaultOrAppSettingAs<T>(string key)
        {
            return ((IConfiguration)_instance).GetDefaultOrAppSettingAs<T>(key);
        }

        public T GetSection<T>(string sectionName)
        {
            return ((IConfiguration)_instance).GetSection<T>(sectionName);
        }
    }
}
