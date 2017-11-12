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
        // Singleton
        private static Machine _instance;
        public static Machine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Machine();
                }
                return _instance;
            }
        }

        Dictionary<int, PerformanceCounter> _cpuCounters = new Dictionary<int, PerformanceCounter>();

        private Machine()
        {
            for (int i = 0; i < NumCPUProcessingThreads; i++)
            {
                _cpuCounters.Add(i, new PerformanceCounter("Processor", "% Processor Time", i.ToString()));
                float nv = _cpuCounters[i].NextValue(); // Must call once to init counter.
            }
        }

        #region IMachine interface

        public DateTime CurrentDateTime { get { return DateTime.Now; } }

        public int NumCPUProcessingThreads { get { return Environment.ProcessorCount; } }

        public Dictionary<int, double> CPUThreadUtilization
        {
            get
            {
                return _cpuCounters.ToDictionary(e => e.Key, e => (double)e.Value.NextValue());
            }
        }

        #endregion
    }
}
