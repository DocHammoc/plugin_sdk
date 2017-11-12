using AquaComputer.Plugin;
using PluginXforma59.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PluginXforma59
{
    public class xFormaData
    {
        private string _source = string.Empty;
        private IMachine _machine = null;
        private DateTime _machineNow;

        public xFormaData(string plugin_id, IMachine machine)
        {
            _source = plugin_id;
            _machine = machine;
        }



        public SensorGroup ProbeMachine()
        {
            _machineNow = _machine.CurrentDateTime;

            SensorGroup main_group = new SensorGroup()
            {
                name = "xforma59 Plugin",
                source_id = _source,
                identifier = _source,                 
            };

            SensorNode node;

            node = new SensorNode()
            {
                source_id = _source,
                identifier = "utilization",
                is_sensor = true,
                name = "Average Percent Utilization",
                unit = (int)SensorNodeBase.UnitType.Temperature,
                sensor_value = GetUtilization(),
            };

            main_group.children.Add(node);
            
            return main_group;
        }
        
        private double GetUtilization()
        {
            return _machine.CPUThreadUtilization[0];
        }
    }
}