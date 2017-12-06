using AquaComputer.Plugin;
using Configuration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginXforma59
{
    public class PluginXforma : IDataImportPlugin
    {
        private xFormaData _dataSource = null;

        private SensorGroup _sensorData = null;
        private bool _sensorUpdate = false;

        #region "Identity Properties"

        public string unique_plugin_identifier
        {
            get
            {
                return "xForma59";
            }
        }

        /// <summary>
        /// dispose handler
        /// </summary>
        ~PluginXforma()
        {
        }

        /// <summary>
        /// plugin info
        /// </summary>
        public PluginInfo info
        {
            get {
                string name = unique_plugin_identifier + " Plugin";

                //init plugin informations
                return new PluginInfo()
                {
                    Name = name,
                    Version = @"1.0",
                    DescriptionDE = name,
                    DescriptionEN = name,
                    UseFilename = false,
                    UsePath = false,
                };
            }
        }

        #endregion

        /// <summary>
        /// start export plugin
        /// </summary>
        public void start_instance()
        {
            _dataSource = new xFormaData(this.unique_plugin_identifier, Machine.Instance(Configuration.Instance()), Configuration.Instance());
        }

        /// <summary>
        /// is called every second from the service handler
        /// </summary>
        public void worker()
        {
            if (_dataSource == null) return;

            SensorGroup sensors = _dataSource.ProbeMachine();
            _sensorData = sensors;
            _sensorUpdate = true;
        }

        /// <summary>
        /// trigger that indicate when new data are available
        /// </summary>
        public bool new_data_available
        {
            get
            {
                if (_sensorUpdate)
                {
                    _sensorUpdate = false;
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
            return _sensorData;
        }

        /// <summary>
        /// abort export plugin
        /// </summary>
        public void stop_instance()
        {
            _dataSource = null;
            _sensorData = null;
        }

    }
}
