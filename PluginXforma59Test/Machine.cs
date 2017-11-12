using PluginXforma59.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginXforma59Test
{
    public class MachineState
    {
        public DateTime CurrentDateTime;
        public double[] CPUThreadUtilization = new double[Machine.Instance.NumCPUProcessingThreads];
    }

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

        private Machine() { }

        public Machine(List<MachineState> machineStates)
        {
            _machineStates = machineStates;
        }

        //public void NextState() { _currentStateIndex = (_currentStateIndex + 1) % _machineStates.Count; }

        #region IMachine interface

        public DateTime CurrentDateTime
        {
            get
            {
                _currentStateIndex = (_currentStateIndex + 1) % _machineStates.Count; // Assume CurrentDateTime called first with each probe.
                return _machineStates.ElementAt(_currentStateIndex).CurrentDateTime;
            }
        }

        public int NumCPUProcessingThreads { get { return 4; } }

        public Dictionary<int, double> CPUThreadUtilization
        {
            get
            {
                MachineState state = _machineStates.ElementAt(_currentStateIndex);
                int i = 0;
                return state.CPUThreadUtilization.ToDictionary(e => i++, e => e);
            }
        }

        #endregion

        private List<MachineState> _machineStates;
        private int _currentStateIndex = -1;
    }
}
