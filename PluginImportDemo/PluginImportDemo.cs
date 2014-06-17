using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaComputer.Plugin.Demo;

namespace AquaComputer.Plugin
{
    public class PuginImportDemo : IDataImportPlugin
    {
        private DummyData data_source = null;
        private SensorGroup sensor_data = null;
        private bool sensor_update = false;
        PluginInfo m_info = null;

        private const string INFO_DE =
@"Demo Import Plugin.";

        private const string INFO_EN =
@"Demo Import Plugin.";

        public PuginImportDemo()
        {
            //init plugin informations
            m_info = new PluginInfo
            {
                Name = @"aquasuite Demo Import Plugin",
                Version = @"1.0",
                DescriptionDE = INFO_DE,
                DescriptionEN = INFO_EN,
                UseFilename = false,
                UsePath = false,
            };
        }

        /// <summary>
        /// dispose handler
        /// </summary>
        ~PuginImportDemo()
        {
        }

        public string unique_plugin_identifier
        {
            get
            {
                return "demo_import";
            }
        }

        /// <summary>
        /// plugin info
        /// </summary>
        public PluginInfo info
        {
            get { return m_info; }
        }

        /// <summary>
        /// start export plugin
        /// </summary>
        public void start_instance()
        {
            data_source = new DummyData(this.unique_plugin_identifier);
        }

        /// <summary>
        /// abort export plugin
        /// </summary>
        public void stop_instance()
        {
            data_source = null;
            sensor_data = null;
        }

        /// <summary>
        /// is called every second from the service handler
        /// </summary>
        public void worker()
        {
            if (data_source == null)
                return;

            var sensors = data_source.read_sensors();
            if (sensors != null)
            {
                sensors.name = this.info.Name;
                sensors.source_id = this.unique_plugin_identifier;
                sensors.identifier = this.unique_plugin_identifier;
                sensor_data = sensors;
                sensor_update = true;
            }
        }

        /// <summary>
        /// trigger that indicate when new data are available
        /// </summary>
        public bool new_data_available
        {
            get
            {
                if (sensor_update)
                {
                    sensor_update = false;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// service is call this function when new_data_available = true
        /// </summary>
        /// <returns>Group of sensor data, null when no data are available</returns>
        public SensorGroup get_data()
        {
            return sensor_data;
        }

    }
}
