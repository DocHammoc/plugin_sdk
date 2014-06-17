using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaComputer.Logdata
{
    /// <summary>
    /// Log data Export class
    /// </summary>
    [Serializable]
    public class LogDataExport
    {
        public LogDataExport() { }

        public string name { get; set; }
        public DateTime exportTime { get; set; }
        public List<Plugin.LogDataSet> logdata { get; set; }

        public void SaveToFile(string filename)
        {
            System.IO.StreamWriter writer = null;
            try
            {
                writer = new System.IO.StreamWriter(filename);
                Serialize<LogDataExport>(this, writer.BaseStream);
                writer.Close();
            }
            catch
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static void Serialize<T>(T obj, System.IO.Stream s)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(s, Encoding.UTF8);
                xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
                xs.Serialize(xmlTextWriter, obj);
            }
            catch { }
        }


    }
}

