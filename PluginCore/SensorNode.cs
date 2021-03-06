﻿using System;
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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.None'
            None = 0,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.None'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.Millisecond'
            Millisecond = 1,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.Millisecond'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.Second'
            Second = 2,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.Second'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.Minute'
            Minute = 3,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.Minute'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.Hour'
            Hour = 4,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.TimeScale.Hour'
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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.NONE'
            NONE,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.NONE'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.NONE_10'
            NONE_10,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.NONE_10'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.NONE_100'
            NONE_100,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.NONE_100'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.RPM'
            RPM,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.RPM'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.PERCENT'
            PERCENT,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.PERCENT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FPS'
            FPS,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FPS'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.CURRENT_MA'
            CURRENT_MA,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.CURRENT_MA'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.CURRENT_A'
            CURRENT_A,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.CURRENT_A'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.VOLTAGE_MV'
            VOLTAGE_MV,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.VOLTAGE_MV'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.VOLTAGE_V'
            VOLTAGE_V,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.VOLTAGE_V'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.TEMPERATURE_C'
            TEMPERATURE_C,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.TEMPERATURE_C'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.TEMPERATURE_F'
            TEMPERATURE_F,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.TEMPERATURE_F'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FLOW_LH'
            FLOW_LH,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FLOW_LH'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FLOW_LM'
            FLOW_LM,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FLOW_LM'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FLOW_GALH'
            FLOW_GALH,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FLOW_GALH'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FLOW_GALM'
            FLOW_GALM,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FLOW_GALM'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FREQ_HZ'
            FREQ_HZ,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FREQ_HZ'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FREQ_KHZ'
            FREQ_KHZ,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FREQ_KHZ'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FREQ_MHZ'
            FREQ_MHZ,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FREQ_MHZ'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FREQ_GHZ'
            FREQ_GHZ,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.FREQ_GHZ'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.MILLI_WATT'
            MILLI_WATT,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.MILLI_WATT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.WATT'
            WATT,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.WATT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.KILO_WATT'
            KILO_WATT,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.KILO_WATT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.MILLI_SEC'
            MILLI_SEC,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.MILLI_SEC'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.SEC'
            SEC,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.SEC'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.MINUTES'
            MINUTES,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.MINUTES'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.HOURS'
            HOURS,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.HOURS'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.DAYS'
            DAYS,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.DAYS'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.BIT'
            BIT,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.BIT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.KBIT'
            KBIT,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.KBIT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.MBIT'
            MBIT,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.MBIT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.GBIT'
            GBIT,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.GBIT'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.P_MILLIBAR'
            P_MILLIBAR,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.P_MILLIBAR'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.P_BAR'
            P_BAR,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.P_BAR'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.RATIO'
            RATIO,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.RATIO'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.SIZE_MB'
            SIZE_MB,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.SIZE_MB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.SIZE_GB'
            SIZE_GB,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.SIZE_GB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.SIZE_TB'
            SIZE_TB,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.SIZE_TB'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.BOOL'
            BOOL,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.BOOL'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.WEIGHT_G'
            WEIGHT_G,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.WEIGHT_G'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.WEIGHT_KG'
            WEIGHT_KG,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.WEIGHT_KG'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.WEIGHT_T'
            WEIGHT_T,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.WEIGHT_T'
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.ANGLE_DEG'
            ANGLE_DEG,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.UnitType.ANGLE_DEG'
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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.identifier_path'
        public string identifier_path
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.identifier_path'
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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.Filter(SensorNodeBase.UnitType)'
        public void Filter(UnitType f)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.Filter(SensorNodeBase.UnitType)'
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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.GetSensor(string, string)'
        public SensorNode GetSensor(string source, string Identifier)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'SensorNodeBase.GetSensor(string, string)'
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
