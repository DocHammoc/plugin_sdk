using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaComputer.Plugin
{
    public class PluginExportXML : ILogDataExportPlugin
    {
        private bool _in_progress = false;
        PluginInfo m_info = null;
        private string export_name = string.Empty;
        private string export_path = string.Empty;

        private const string INFO_DE =
@"Es wird eine XML Datei mit dem gewählten Dateinamen in den angegebenen Pfad erzeugt.
Der Inhalt der Datei wird mit dem eingestellen Intervall aktualisiert.";

        private const string INFO_EN =
@"Export data to a XML file with the given file name and path name.
The content of the file is refreshed with the given interval.";

        public PluginExportXML()
        {
            //init plugin informations
            m_info = new PluginInfo
            {
                Name = @"XML File Export",                
                Version = @"1.0",
                DescriptionDE = INFO_DE,
                DescriptionEN = INFO_EN,
                UseFilename = true,
                UsePath = true,
            };
            _in_progress = false;
        }

        /// <summary>
        /// dispose handler
        /// </summary>
        ~PluginExportXML()
        {
        }

        /// <summary>
        /// plugin info
        /// </summary>
        public PluginInfo info
        {
            get { return m_info; }
        }

        /// <summary>
        /// start export plugin
        /// </summary>
        public void start_instance()
        {

        }

        /// <summary>
        /// abort export plugin
        /// </summary>
        public void stop_instance()
        {

        }

        /// <summary>
        /// Update settings from parent process
        /// </summary>
        /// <param name="name">shered memory file name</param>
        /// <param name="path">export path</param>
        public void setup_plugin(string name, string path)
        {
            export_name = name;
            export_path = path;
        }

        /// <summary>
        /// this funtion is calles from parent process to
        /// transfer the new data to the plugin class
        /// </summary>
        /// <param name="data">list with all new log data items</param>
        public void add_log_data(List<LogDataSet> data)
        {
            AquaComputer.Logdata.LogDataExport export = new Logdata.LogDataExport();
            export.logdata = data;
            export.name = export_name;
            export.exportTime = DateTime.Now;

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(export_path);
            if (!dir.Exists)
                return; //dir is not valid
            if (export_name == null || export_name == string.Empty)
                return; //file name not valid

            if (_in_progress)
                return; //the current function is running in an other thread

            //export data to xml file
            _in_progress = true;
            string file = dir.FullName + "//" + export_name + ".xml";
            export.SaveToFile(file);
            _in_progress = false;
        }

    }
}
