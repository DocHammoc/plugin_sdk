using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using AquaComputer.Plugin;

namespace AquaComputer.Plugin.Demo
{
    public class DummyData
    {
        private string source = string.Empty;
        Random rnd_number;

        public DummyData(string plugin_id)
        {
            source = plugin_id;
            rnd_number = new Random((int)DateTime.Now.TimeOfDay.TotalMilliseconds);
        }

        public SensorGroup read_sensors()
        {
            SensorGroup main_group = new SensorGroup();
            main_group.source_id = source;
            main_group.name = source;

            SensorNode node;

            node = new SensorNode();
            node.source_id = source;
            node.idx = 0;
            node.identifier = "hdkasdk";
            node.is_sensor = true;
            node.name = "Dummy Sensor 1";
            node.unit = (int)SensorNodeBase.UnitType.Number;
            node.range = (int)SensorNodeBase.Range.None;
            node.time_scale = (int)SensorNodeBase.TimeScale.None;
            node.sensor_value = rnd_number.Next(2000, 10000) / 100.0;
            main_group.children.Add(node);

            node = new SensorNode();
            node.source_id = source;
            node.idx = 0;
            node.identifier = "loz9324zld";
            node.is_sensor = true;
            node.name = "Dummy Sensor 2";
            node.unit = (int)SensorNodeBase.UnitType.Temperature;
            node.range = (int)SensorNodeBase.Range.None;
            node.time_scale = (int)SensorNodeBase.TimeScale.None;
            node.sensor_value = rnd_number.Next(5000, 7000) / 100.0;
            main_group.children.Add(node);

            return main_group;
        }

    }
}
