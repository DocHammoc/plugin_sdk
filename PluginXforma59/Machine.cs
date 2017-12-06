using Configuration.Interface;
using PluginXforma59.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginXforma59
{
    public class Machine : IMachine
    {
        IConfigurationManagerExtension _config;
        List<PerformanceCounter> _cpuCounters = new List<PerformanceCounter>();

        // Singleton
        private static Machine _instance;
        private Machine(IConfigurationManagerExtension config)
        {
            _config = config;

            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                _cpuCounters.Add(new PerformanceCounter("Processor", "% Processor Time", i.ToString()));
                float nv = _cpuCounters[i].NextValue(); // Must call once to init counter.
            }
        }

        public static Machine Instance(IConfigurationManagerExtension config)
        {
            if (_instance == null)
            {
                _instance = new Machine(config);
            }
            return _instance;
        }

        public static Machine Instance()
        {
            if (_instance == null) throw new ArgumentNullException("config", "Please call Instance with the correct argument");

            return _instance;
        }

        #region IMachine interface

        public DateTime CurrentDateTime { get { return DateTime.Now; } }

        public List<double> CPUThreadUtilizations
        {
            get
            {
                return _cpuCounters.Select(c => (double)c.NextValue()).ToList();
            }
        }

        public List<double> GPUUtilizations
        {
            get
            {
                return new List<double>() { 0.0 };
            }
        }

        public List<double> FlowRates
        {
            get
            {
                return new List<double>() { 0.0 };
            }
        }

        public List<double> FlashersPower
        {
            get
            {
                return new List<double>() { 0.0 };
            }
        }

        public List<IDimmerControl> DimmerControls
        {
            get
            {
                return new List<IDimmerControl>() { new DimmerControl() };
            }
        }

        #endregion
    }
}
