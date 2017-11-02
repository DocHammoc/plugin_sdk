using System;
using System.Collections.Generic;

namespace AquaComputer.Plugin
{
    /// <summary>
    /// Is a Sensor Value
    /// </summary>
    public class SensorNode : SensorNodeBase
    {
        /// <summary>
        /// init
        /// </summary>
        public SensorNode()
            : base()
        {

        }
    }

    /// <summary>
    /// Group with nodes
    /// Contains other Groups or sensor data
    /// </summary>
    public class SensorGroup : SensorNodeBase
    {
        /// <summary>
        /// init
        /// </summary>
        public SensorGroup()
            : base()
        {

        }
    }

    /// <summary>
    /// Sensor Node
    /// Repersent a sensor value or sensor group node with children
    /// </summary>
    public class SensorNodeBase
    {
        const int SENSOR_UPDATE_TIMEOUT = 60;

        /// <summary>
        /// init
        /// </summary>
        public SensorNodeBase()
        {
            is_sensor = false;            
            name = string.Empty;
            identifier = string.Empty;
            idx = 0;
            identifier = string.Empty;
            unit = (int)UnitType.Number;
            range = (int)Range.None;
            time_scale = (int)TimeScale.None;
            children = new List<SensorNodeBase>();
            sensor_value = double.NaN;
            m_last_update = DateTime.MinValue;
        }

        /// <summary>
        /// value time scale
        /// </summary>
        public enum TimeScale : int
        {
            None = 0,
            Millisecond = 1,
            Second = 2,
            Minute = 3,
            Hour = 4,
        }

        /// <summary>
        /// value range
        /// </summary>
        public enum Range : int
        {
            /// <summary>10^-6 scale</summary>
            Micro = 0,
            /// <summary>10^-3 scale</summary>
            Milli = 1,
            /// <summary>10^1 scale</summary>
            Cen = 2,
            /// <summary>10^2 scale</summary>
            Dez = 3,
            /// <summary>10^0 scale</summary>
            None = 4,
            /// <summary>10^3 scale</summary>
            Kilo = 5,
            /// <summary>10^6 scale</summary>
            Mega = 6,
            /// <summary>10^9 scale</summary>
            Giga = 7,
            /// <summary>10^12 scale</summary>
            Terra = 8,
        }

        /// <summary>
        /// Value unit
        /// </summary>
        public enum UnitType : int
        {
            /// <summary> normal number</summary>
            Number = 0,
            /// <summary>Temperature in °C</summary>
            Temperature = 1,
            /// <summary>Temperature difference in °C</summary>
            TemperatureDifference = 2,
            /// <summary>Flow rate in l/h</summary>
            Flow = 3,
            /// <summary>Volume in l</summary>
            Volume = 4,
            /// <summary>Power in W</summary>
            Power = 5,
            /// <summary>Work in Wh</summary>
            Work = 6,
            /// <summary>Voltage in V</summary>
            Voltage = 7,
            /// <summary>Current in A</summary>
            Current = 8,
            /// <summary>Rotation speed in rotation per minute</summary>
            RotationSpeed = 9,
            /// <summary>Relative Humidity in %</summary>
            Humidity = 10,
            /// <summary>Pressure in Bar</summary>
            Pressure = 11,
            /// <summary>Lenght in mm</summary>
            Length = 12,
            /// <summary>Percent value</summary>
            Percent = 13,
            /// <summary>Frequency in Hz</summary>
            Frequency = 14,
            /// <summary>Resistance in Ohm</summary>
            Resistance = 15,
            /// <summary>Force in N</summary>
            Force = 16,
            /// <summary>State as number</summary>
            State = 17,
            /// <summary>Data volume in Bytes</summary>
            DataVolume = 18,
            /// <summary>Data speed in Bytes per second</summary>
            DataSpeed = 19,
            /// <summary>Timespan in seconds</summary>
            Timespan = 20,

            //forecd units
            NONE,
            NONE_10,
            NONE_100,
            RPM,
            PERCENT,
            FPS,
            CURRENT_MA,
            CURRENT_A,
            VOLTAGE_MV,
            VOLTAGE_V,
            TEMPERATURE_C,
            TEMPERATURE_F,
            FLOW_LH,
            FLOW_LM,
            FLOW_GALH,
            FLOW_GALM,
            FREQ_HZ,
            FREQ_KHZ,
            FREQ_MHZ,
            FREQ_GHZ,
            MILLI_WATT,
            WATT,
            KILO_WATT,
            MILLI_SEC,
            SEC,
            MINUTES,
            HOURS,
            DAYS,
            BIT,
            KBIT,
            MBIT,
            GBIT,
            P_MILLIBAR,
            P_BAR,
            RATIO,
            SIZE_MB,
            SIZE_GB,
            SIZE_TB,
            BOOL,
            WEIGHT_G,
            WEIGHT_KG,
            WEIGHT_T,
            ANGLE_DEG,
        }

        /// <summary>
        /// true when the value is a sensor value, otherwise this is only a 
        /// node to a sensor value
        /// </summary>
        public bool is_sensor { get; set; }

        /// <summary>
        /// source_id
        /// source id name
        /// </summary>
        public string source_id { get; set; }

        /// <summary>
        /// Node or sensor name ti display in ui
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// sensor index
        /// </summary>
        public int idx { get; set; }

        public string identifier_path
        {
            get { return m_identifier + "_" + idx.ToString(); }
        }

        #region identifier
        private string m_identifier = string.Empty;

        /// <summary>
        /// sensor or path identifier to build the correct data connection to the sensor
        /// this value identify the sensor, olny normal characters allowed, a-z and 0-9!
        /// each idetifier is unique per plugin
        /// </summary>        
        public string identifier
        {
            get
            {
                return m_identifier;
            }
            set
            {
                string id = value;
                id = id.ToLower();
                System.Text.RegularExpressions.Regex regex = 
                    new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_/]");
                id = regex.Replace(id, "");
                m_identifier = id;
            }
        }
        #endregion

        /// <summary>
        /// sensor unit type
        /// describe the actual unit
        /// </summary>
        public int unit { get; set; }

        /// <summary>
        /// range of actual value
        /// when value is scaled in Kilo, Mega...
        /// </summary>
        public int range { get; set; }

        /// <summary>
        /// time scale
        /// </summary>
        public int time_scale { get; set; }

        /// <summary>
        /// child nodes
        /// </summary>
        public List<SensorNodeBase> children { get; set; }

        #region public double sensor_value
        private double m_sensor_value = double.NaN;
        /// <summary>
        /// value from actual sensor
        /// the sensor value is set to nan when the last write is older than SENSOR_UPDATE_TIMEOUT
        /// </summary>
        public double sensor_value
        {
            get
            {
                if (!Double.IsNaN(m_sensor_value) && (DateTime.Now - m_last_update).TotalSeconds > SENSOR_UPDATE_TIMEOUT)
                {
                    m_sensor_value = Double.NaN;
                }
                return m_sensor_value;
            }
            set
            {
                if (m_sensor_value != value)
                {
                    m_sensor_value = value;
                }
                m_last_update = DateTime.Now;
            }
        }
        #endregion

        #region public DateTime UpdateTime
        private DateTime m_last_update = DateTime.MinValue;
        /// <summary>
        /// date and time of last update
        /// </summary>
        public DateTime last_update
        {
            get { return m_last_update; }
        }
        #endregion

        #region public void Filter(UnitType f)
        public void Filter(UnitType f)
        {
            List<SensorNodeBase> remove = new List<SensorNodeBase>();
            foreach (SensorNodeBase node in children)
            {
                if (node != null && node.GetType() == typeof(SensorGroup))
                {
                    SensorGroup g = node as SensorGroup;
                    if (g != null)
                    {
                        g.Filter(f);
                        if (g.children.Count == 0)
                            remove.Add(node);
                    }
                }
                else if (node != null && (int)f != (int)node.unit)
                {
                    remove.Add(node);
                }
            }

            foreach (SensorNodeBase x in remove)
            {
                children.Remove(x);
            }
        }
        #endregion

        #region public SensorNode GetSensor(string source, string Identifier)
        public SensorNode GetSensor(string source, string Identifier)
        {
            SensorNode result = null;
            if (source_id != source)
                return null;
            foreach (SensorNodeBase node in children)
            {
                if (node.GetType() == typeof(SensorGroup))
                {
                    SensorGroup g = node as SensorGroup;
                    result = g.GetSensor(source, Identifier);
                }
                else
                {
                    SensorNode sens = node as SensorNode;
                    if (sens != null)
                    {
                        if (sens.identifier == Identifier)
                            result = sens;
                    }
                }
                if (result != null)
                    break;
            }
            return result;
        }
        #endregion

        #region public List<SensorNode> GetSensors()
        /// <summary>
        /// Return a flat list with all sensor nodes in the current tree
        /// </summary>
        /// <returns>List with all sensors</returns>
        public List<SensorNode> GetSensors()
        {
            List<SensorNode> result = GetSensors(this);
            return result;
        }
        private List<SensorNode> GetSensors(SensorNodeBase tree)
        {
            List<SensorNode> result = new List<SensorNode>();
            if (tree == null || tree.children == null || tree.children.Count == 0)
                return result;

            foreach (SensorNodeBase node in tree.children)
            {
                if (node.GetType() == typeof(SensorGroup))
                {
                    result.AddRange(GetSensors(node));
                }
                else
                {
                    SensorNode sens = node as SensorNode;
                    if (sens != null)
                    {
                        result.Add(sens);
                    }
                }
            }
            return result;
        }
        #endregion

    }
}
