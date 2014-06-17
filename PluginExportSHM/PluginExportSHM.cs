using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaComputer.Plugin
{
    public class PluginExportSHM : ILogDataExportPlugin
    {
        bool _in_progress = false;
        PluginInfo m_info = null;
        private string export_name = string.Empty;
        private string export_path = string.Empty;
        private System.IO.MemoryMappedFiles.MemoryMappedFile memory_file = null;
        private int memory_file_size = 0;

        private const string INFO_DE =
@"Exportiert die Daten als XML in den Hauptspeicher.
Der Inhalt der Datei wird mit dem eingestellen Intervall aktualisiert.";

        private const string INFO_EN =
@"Export data as XML content in a shared memory file.
The content of the file is refreshed with the given interval.";

        public PluginExportSHM()
        {
            //init plugin informations
            m_info = new PluginInfo
            {
                Name = @"Shared Memory Export",                
                Version = @"1.0",
                DescriptionDE = INFO_DE,
                DescriptionEN = INFO_EN,
                UseFilename = true,
                UsePath = false,
            };
        }

        /// <summary>
        /// dispose handler
        /// </summary>
        ~PluginExportSHM()
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
        /// free all used ressources,
        /// abort all work
        /// </summary>
        public void stop_instance()
        {
            //delete old mutex
            bool initial_owned;
            System.Threading.Mutex shm_mutex = new System.Threading.Mutex(true, "Global\\" + export_name + "_mutex", out initial_owned);
            if (!initial_owned)
            {
                shm_mutex.WaitOne(250);
            }

            if (memory_file != null && memory_file.SafeMemoryMappedFileHandle.IsInvalid != true)
                memory_file.Dispose();
            memory_file = null;

            shm_mutex.ReleaseMutex();
            shm_mutex.Dispose();
            shm_mutex = null;
            
            memory_file_size = 0;
            _in_progress = false;
        }

        /// <summary>
        /// Update settings from parent process
        /// </summary>
        /// <param name="name">shered memory file name</param>
        /// <param name="path">not used</param>
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
            if (_in_progress)   //the current process is locked
                return;

            //export current data to a buffer for file writing
            _in_progress = true;
            AquaComputer.Logdata.LogDataExport export = new Logdata.LogDataExport();
            export.logdata = data;
            export.name = export_name;
            export.exportTime = DateTime.Now;
            byte[] buffer = export.ToBuffer();

            if (buffer == null || export_name == null || export_name == string.Empty)
            {
                _in_progress = false;
                return;
            }

            //try to create a new shared memory file
            if (memory_file == null || memory_file.SafeMemoryMappedFileHandle.IsClosed || memory_file.SafeMemoryMappedFileHandle.IsInvalid)
            {
                try
                {
                    memory_file = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateNew(export_name, buffer.LongLength * 2);
                    memory_file_size = buffer.Length * 2;
                }
                catch
                {
                    memory_file = null;
                    memory_file_size = 0;
                }                
            }

            //not able to craeate to use a shared memory file with the current settings
            if (memory_file == null)
            {
                _in_progress = false;
                return;
            }

            //check if the file used from external ressources
            //wait until all locks are released or timeout elapsed
            bool initial_owned;
            System.Threading.Mutex shm_mutex = new System.Threading.Mutex(true, "Global\\" + export_name + "_mutex", out initial_owned);
            if (!initial_owned)
            {
                bool mutex_is_free = shm_mutex.WaitOne(50);
                if (mutex_is_free == false)
                {
                    _in_progress = false;
                    return;
                }
            } 

            if (memory_file_size < buffer.Length)
            {
                //file buffer is too small, create file buffer
                memory_file.Dispose();
                memory_file = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateNew(export_name, buffer.LongLength * 2);
                memory_file_size = buffer.Length * 2;
            }

            //acces file to write data to ram segement
            using(var accessor = memory_file.CreateViewAccessor(0, memory_file_size))
            {
                if (accessor != null)
                {
                    accessor.WriteArray<byte>(0, buffer, 0, buffer.Length);

                    //reset rest of data array to 0
                    byte[] reset_buffer = new byte[memory_file_size - buffer.Length];
                    for (int i = 0; i < reset_buffer.Length; i++)
                        reset_buffer[i] = 0;
                    accessor.WriteArray<byte>(buffer.Length, reset_buffer, 0, reset_buffer.Length);
                }                
            }

            //release ressources
            shm_mutex.ReleaseMutex();   //release mutex
            _in_progress = false;
        }

    }
}
