using AquaComputer.Plugin;
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

        ~PluginXforma()
        {
        }

        public PluginInfo info
        {
            get {
                string name = unique_plugin_identifier + " Plugin";

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

        public void start_instance()
        {
            _dataSource = new xFormaData(this.unique_plugin_identifier, Machine.Instance);
        }

        public void worker()
        {
            if (_dataSource == null) return;

            SensorGroup sensors = _dataSource.ProbeMachine();
            _sensorData = sensors;
            _sensorUpdate = true;
        }

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

        public SensorGroup get_data()
        {
            return _sensorData;
        }

        public void stop_instance()
        {
            _dataSource = null;
            _sensorData = null;
        }

    }
}
