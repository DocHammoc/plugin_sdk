using Configuration.Core;
using PluginXforma59.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginXforma59
{
    public class Configuration : ConfigurationManagerAdapter, IConfiguration
    {
        // Singleton
        private static Configuration _instance;
        private Configuration()
        {            
        }

        public static Configuration Instance()
        {
            if (_instance == null) _instance = new Configuration();

            return _instance;
        }
    }
}
