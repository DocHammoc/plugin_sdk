using System;
using System.Collections.Generic;

namespace AquaComputer.Plugin
{
    /// <summary>
    /// Aquasuite Export Plugin Interface
    /// </summary>
    public interface ILogDataExportPlugin
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
        /// is called after start_instance
        /// </summary>
        /// <param name="name">parameter filename from host</param>
        /// <param name="path">parameter path from host</param>
        void setup_plugin(string name, string path);
        
        /// <summary>
        /// is called cyclic with the given interval and
        /// provide the new added log data
        /// </summary>
        /// <param name="data">log data items</param>
        void add_log_data(List<LogDataSet> data);
    }
}
