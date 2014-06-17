using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PluginExportShmTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            xml_data.Document.Blocks.Clear();
            string file = filename_edit.Text;

            bool timeout = false;
            bool initial_owned;
            System.Threading.Mutex shm_mutex = new System.Threading.Mutex(true, "Global\\" + file + "_mutex", out initial_owned);
            if (!initial_owned)
            {
                bool mutex_is_free = shm_mutex.WaitOne(50);
                if (mutex_is_free == false)
                {
                    timeout = true;
                    return;
                }
            }
            else
            {
                shm_mutex.ReleaseMutex();
                shm_mutex.Dispose();
                return;
            }
            if (timeout)
                return;

            string text_xml = "";
            using (var shm = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting(file, System.IO.MemoryMappedFiles.MemoryMappedFileRights.Read))
            {
                using (var read_stream = shm.CreateViewStream(0,0, System.IO.MemoryMappedFiles.MemoryMappedFileAccess.Read))
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(read_stream);
                    text_xml = reader.ReadToEnd();
                    text_xml = text_xml.Trim('\0');
                }                
            }
            shm_mutex.ReleaseMutex();

            xml_data.Document.Blocks.Add(new Paragraph(new Run(text_xml)));
        }
    }
}
