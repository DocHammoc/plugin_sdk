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
        public double[] CPUThreadUtilizations = new double[4];
    }

    public class Machine : IMachine
    {
        private List<MachineState> _machineStates;
        private int _currentStateIndex = -1;
        
        // Singleton
        private static Machine _instance;
        private Machine(List<MachineState> machineStates)
        {
            _machineStates = machineStates;
        }

        public static Machine Instance
        {
            get {
                if (_instance == null) throw new ApplicationException("Call CreateInstance first.");
                return _instance;
            }
        }

        public static Machine CreateInstance(List<MachineState> machineStates)
        {
            if (_instance != null) throw new ApplicationException("CreateInstance may be called only once.  Use Instance to get instance.");
            _instance = new Machine(machineStates);
            return _instance;
        }

        //public void NextState() { _currentStateIndex = (_currentStateIndex + 1) % _machineStates.Count; }

        #region IMachine interface

        public DateTime CurrentDateTime
        {
            get
            {
                _currentStateIndex = (_currentStateIndex + 1) % _machineStates.Count; // Assume CurrentDateTime called first with each probe.
                return _machineStates[_currentStateIndex].CurrentDateTime;
            }
        }

        public List<double> CPUThreadUtilizations
        {
            get
            {
                MachineState state = _machineStates[_currentStateIndex];
                return state.CPUThreadUtilizations.ToList();
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
