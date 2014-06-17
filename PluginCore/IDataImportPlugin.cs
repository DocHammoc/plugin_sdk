using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaComputer.Plugin
{
    /// <summary>
    /// Aquasuite Export Plugin Interface
    /// </summary>
    public interface IDataImportPlugin
    {
        /// <summary>
        /// info item with plugin general informations
        /// </summary>
        PluginInfo info { get; }

        /// <summary>
        /// called when the plugin is initialized
        /// from the host process
        /// </summary>
        void start_instance();

        /// <summary>
        /// called when the plugin is stopped
        /// from the host process
        /// </summary>
        void stop_instance();        

        /// <summary>
        /// plugin identifier to identify this plugin as data source
        /// </summary>
        string unique_plugin_identifier { get; }

        /// <summary>
        /// is called cyclic from the service before 
        /// new_data_available is checked and get_data is called  
        /// </summary>
        void worker();

        /// <summary>
        /// indicate when new or updated data are available
        /// </summary>
        bool new_data_available { get; }

        /// <summary>
        /// is called cyclic from the service        
        /// </summary>
        /// <returns>List with all sensor data</returns>
        SensorGroup get_data();
    }
}
