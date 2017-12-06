using AquaComputer.Plugin;
using Configuration.Interface;
using PluginXforma59.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PluginXforma59
{
    public class xFormaData
    {
        private string _source = string.Empty;
        private IConfigurationManagerExtension _config = null;
        private IMachine _machine = null;
        private DateTime _machineNow;
        private string plugin_id;
        private global::PluginXforma59Test.Machine machine;
        private global::PluginXforma59Test.Configuration config;

        public xFormaData(string plugin_id, IMachine machine, IConfigurationManagerExtension config)
        {
            _source = plugin_id;
            _config = config;
            _machine = machine;
        }

        public xFormaData(string plugin_id, global::PluginXforma59Test.Machine machine, global::PluginXforma59Test.Configuration config)
        {
            this.plugin_id = plugin_id;
            this.machine = machine;
            this.config = config;
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

            main_group.children.Add(new SensorNode()
            {
                source_id = _source,
                identifier = "x-average-utilization",
                is_sensor = true,
                name = "xAverage Percent System Utilization",
                unit = (int)SensorNodeBase.UnitType.Temperature,
                sensor_value = GetAverageUtilization(),
            });

            main_group.children.Add(new SensorNode()
            {
                source_id = _source,
                identifier = "x-instant-utilization",
                is_sensor = true,
                name = "xInstant Percent System Utilization",
                unit = (int)SensorNodeBase.UnitType.Temperature,
                sensor_value = GetInstantUtilization(),
            });

            for (int i = 0; i < _machine.DimmerControls.Count; i++)
            {
                main_group.children.Add(new SensorNode()
                {
                    source_id = _source,
                    identifier = "x-dimmer-control-" + i.ToString(),
                    is_sensor = true,
                    name = "xDimmer Control " + i.ToString(),
                    unit = (int)SensorNodeBase.UnitType.Temperature,
                    sensor_value = GetDimmerValue(i),
                });
            }

            for (int i = 0; i < _machine.FlowRates.Count; i++)
            {
                main_group.children.Add(new SensorNode()
                {
                    source_id = _source,
                    identifier = "x-flow-rate-" + i.ToString(),
                    is_sensor = true,
                    name = "xFlow Rate " + i.ToString(),
                    unit = (int)SensorNodeBase.UnitType.Temperature,
                    sensor_value = _machine.FlowRates[i],
                });
            }

            for (int i = 0; i < _machine.FlashersPower.Count; i++)
            {
                main_group.children.Add(new SensorNode()
                {
                    source_id = _source,
                    identifier = "x-flasher-power-" + i.ToString(),
                    is_sensor = true,
                    name = "xFlasher Power " + i.ToString(),
                    unit = (int)SensorNodeBase.UnitType.Temperature,
                    sensor_value = _machine.FlashersPower[i],
                });
            }

            return main_group;
        }

        private double GetDimmerValue(int i)
        {
            throw new NotImplementedException();
        }

        private double GetInstantUtilization()
        {
            throw new NotImplementedException();
        }

        private double GetAverageUtilization()
        {
            return _machine.CPUThreadUtilizations[0];
        }
    }
}