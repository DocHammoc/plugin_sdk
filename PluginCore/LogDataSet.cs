using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaComputer.Plugin
{
    /// <summary>
    /// LogDataSet, represet an data connection with the current data
    /// </summary>
    [Serializable]
    public class LogDataSet
    {
        public LogDataSet() { }

        /// <summary>
        /// Data set time
        /// </summary>
        [System.Xml.Serialization.XmlIgnore]
        public DateTime time
        {
            get
            {
                if (t == null || t == string.Empty)
                    return DateTime.MinValue;
                return DateTime.ParseExact(t, "yyyy-MM-ddTHH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
            }
            set { t = value.ToString("yyyy-MM-ddTHH:mm:ss.fff"); }
        }

        /// <summary>
        /// Current time
        /// </summary>
        public string t { get; set; }

        /// <summary>
        /// current value
        /// </summary>
        public double value { get; set; }

        /// <summary>
        /// value name
        /// </summary>
        public string name { get; set; }

        //value unit
        public string unit { get; set; }

        /// <summary>
        /// type of value
        /// </summary>
        public string valueType { get; set; }

        /// <summary>
        /// device source
        /// </summary>
        public string device { get; set; }
    }
}
