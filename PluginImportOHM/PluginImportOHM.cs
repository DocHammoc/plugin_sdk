using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AquaComputer.Plugin.OHM;

namespace AquaComputer.Plugin
{
    public class PuginImportOHM : IDataImportPlugin
    {
        private OpenHardwareMonitor data_source = null;
        private SensorGroup sensor_data = null;
        private bool sensor_update = false;
        PluginInfo m_info = null;

        private const string INFO_DE =
@"Open Hardware Monitor Plugin. Importiert Daten aus der WMI Schnittstelle.";

        private const string INFO_EN =
@"Open Hardware Monitor Plugin. Import data from Open Hardware Monitor WMI interface.";

        public PuginImportOHM()
        {
            //init plugin informations
            m_info = new PluginInfo
            {
                Name = @"Open Hardware Monitor",                
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
        ~PuginImportOHM()
        {
        }

        public string unique_plugin_identifier
        {
            get
            {
                return "ohm";
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
            data_source = new OpenHardwareMonitor(this.unique_plugin_identifier);
        }

        /// <summary>
        /// abort export plugin
        /// </summary>
        public void stop_instance()
        {
            data_source = null;
            sensor_data = null;            
        }

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

        public SensorGroup get_data()
        {
            return sensor_data;
        }

    }
}
