using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using AquaComputer.Plugin;

namespace AquaComputer.Plugin.OHM
{
    public class OpenHardwareMonitor
    {
        private OhmGroup[] groups;
        private OhmSensor[] sensors;
        private string source = string.Empty;
        private DateTime last_read = DateTime.MinValue;
        private int read_error_count = 0;

        public OpenHardwareMonitor(string plugin_id)
        {
            source = plugin_id;
            last_read = DateTime.MinValue;
            read_error_count = 0;
        }

        public SensorGroup read_sensors()
        {
            if(read_error_count > 10)
            {
                TimeSpan diff = DateTime.Now - last_read;
                if (diff.TotalSeconds > 30)
                    read_error_count--; //allow one read out
                else
                    return null;
            }

            last_read = DateTime.Now;
            bool read_result = read();
            if (!read_result)
            {
                read_error_count++;
                return null;
            }
            if (sensors == null || sensors.Length == 0 || groups == null || groups.Length == 0)
            {
                read_error_count++;
                return null;
            }
            read_error_count = 0;
            
            SensorGroup main_group = new SensorGroup();
            main_group.source_id = source;
            main_group.name = source;
            foreach (OhmGroup g in groups)
            {
                if (g != null)
                {
                    SensorGroup newGroup = new SensorGroup();
                    newGroup.name = g.Name;
                    newGroup.source_id = source;
                    newGroup.children.AddRange(GetSensorsFromGroup(g.Identifier));
                    if (newGroup.children.Count > 0)
                    {
                        newGroup.children = newGroup.children.OrderBy(x => x.name).ToList();
                        main_group.children.Add(newGroup);
                    }
                }
            }
            return main_group;
        }

        private List<SensorNode> GetSensorsFromGroup(string Parent)
        {
            if (sensors == null || sensors.Length == 0)
                return null;
            List<SensorNode> result = new List<SensorNode>();
            foreach (OhmSensor s in sensors)
            {
                if (s != null && s.Parent == Parent)
                {
                    SensorNode sens = new SensorNode();
                    sens.name = s.Name;
                    sens.identifier = s.Identifier;
                    sens.unit = (int)SensorNode.UnitType.Number;
                    sens.time_scale = (int)SensorNode.TimeScale.None;
                    sens.range = (int)SensorNode.Range.None;
                    sens.is_sensor = true;
                    sens.source_id = source;
                    int round = 3;

                    if (!double.IsNaN(s.Value))
                        sens.sensor_value = s.Value;
                    else
                        sens.sensor_value = double.NaN;

                    switch (s.SensorType)
                    {
                        case "Temperature":
                            sens.unit = (int)SensorNode.UnitType.Temperature;
                            round = 2;
                            break;
                        case "Clock":
                            sens.unit = (int)SensorNode.UnitType.Frequency;
                            if (!Double.IsNaN(sens.sensor_value))
                                sens.sensor_value *= 1000000.0;
                            round = 1;
                            break;
                        case "Volt":
                            sens.unit = (int)SensorNode.UnitType.Voltage;
                            round = 2;
                            break;
                        case "Fan":
                            sens.unit = (int)SensorNode.UnitType.RotationSpeed;
                            sens.time_scale = (int)SensorNode.TimeScale.Minute;
                            round = 0;
                            break;
                        case "Flow":
                            sens.unit = (int)SensorNode.UnitType.Flow;
                            round = 2;
                            break;
                        case "Load":
                        case "Control":
                        case "Level":
                            sens.unit = (int)SensorNode.UnitType.Percent;
                            round = 2;
                            break;
                    }
                    if (!Double.IsNaN(sens.sensor_value))
                    {
                        sens.sensor_value = Math.Round(sens.sensor_value, round);
                        result.Add(sens);
                    }
                }
            }
            return result;
        }

        private bool read()
        {
            ObjectQuery query;
            ManagementObjectSearcher searcher;

            ConnectionOptions connection = new ConnectionOptions();
            connection.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope scope = new ManagementScope("\\\\.\\Root\\OpenHardwareMonitor", connection);
            try
            {
                scope.Connect();
            }
            catch
            {
                return false;
            }

            query = new ObjectQuery("SELECT * FROM Hardware");
            searcher = new ManagementObjectSearcher(scope, query);

            //check if we have items
            int count = 0;
            try
            {
                count = searcher.Get().Count;
            }
            catch { }
            if (count == 0)
                return false;

            List<OhmGroup> g_items = new List<OhmGroup>();
            foreach (ManagementObject queryObj in searcher.Get())
            {
                try
                {
                    OhmGroup group = new OhmGroup();
                    group.Name = queryObj["Name"].ToString();
                    group.HardwareType = queryObj["HardwareType"].ToString();
                    group.Identifier = queryObj["Identifier"].ToString();
                    g_items.Add(group);
                }
                catch { }
            }
            groups = g_items.ToArray();


            query = new ObjectQuery("SELECT * FROM Sensor");
            searcher = new ManagementObjectSearcher(scope, query);

            //check if we have items
            count = 0;
            try
            {
                count = searcher.Get().Count;
            }
            catch { }
            if (count == 0)
                return false;

            List<OhmSensor> items = new List<OhmSensor>();
            foreach (ManagementObject queryObj in searcher.Get())
            {
                try
                {
                    OhmSensor sensor = new OhmSensor();
                    sensor.Name = queryObj["Name"].ToString();
                    sensor.Identifier = queryObj["Identifier"].ToString();
                    sensor.Parent = queryObj["Parent"].ToString();
                    sensor.SensorType = queryObj["SensorType"].ToString();

                    object o_Value = queryObj["Value"];
                    if (o_Value != null && o_Value.GetType() == typeof(double))
                        sensor.Value = (double)o_Value;
                    else if (o_Value != null && o_Value.GetType() == typeof(Single))
                        sensor.Value = (Single)o_Value;
                    else if (o_Value != null && o_Value.GetType() == typeof(int))
                        sensor.Value = (int)o_Value;
                    else
                        sensor.Value = Double.NaN;

                    items.Add(sensor);
                }
                catch { }
            }
            sensors = items.ToArray();

            return true;
        }

        #region OhmGroup, OhmSensor
        internal class OhmGroup
        {
            public string Name { get; set; }
            public string HardwareType { get; set; }
            public string Identifier { get; set; }
        }

        internal class OhmSensor
        {
            public string Name { get; set; }
            public string Identifier { get; set; }
            public string Parent { get; set; }
            public string SensorType { get; set; }
            public double Value { get; set; }
        }
        #endregion

    }
}
