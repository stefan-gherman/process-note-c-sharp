using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            List<CustomProcess> stats = new List<CustomProcess>();

             
            stats = populateStats();
            stats.Sort((x, y) => y.Memory.CompareTo(x.Memory));
            statsSource.ItemsSource = stats;
                        

        }

        public event PropertyChangedEventHandler PropertyChanged;

        List<CustomProcess> populateStats()
        {
            List<CustomProcess> result = new List<CustomProcess>();

            Process[] remoteAll = Process.GetProcesses("potato345");
            foreach (var item in remoteAll)
            {
                int id = item.Id;
                string name = item.ProcessName;
                string note = "...";

                int cpu = 0;
                try
                {
                    cpu = Convert.ToInt32(item.TotalProcessorTime);
                }
                catch (Exception e)
                {
                    cpu = 0;
                }

                int memory = Convert.ToInt32(item.WorkingSet64);

                string startTime = "00";
                try
                {
                    startTime = Convert.ToString(item.StartTime);
                }
                catch (Exception e)
                {
                    startTime = "security";
                }

                int thread = Convert.ToInt32(item.Threads.Count);

                result.Add( new CustomProcess() { ID = id, Name = name, Note = note, CPU = cpu, Memory = memory, Started = startTime, Thread = thread} );
            }


            return result;
        }


    }

    public class CustomProcess
    {
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int CPU { get; set; }
        public int Memory { get; set; }
        public string Started { get; set; }
        public int Thread { get; set; }
    }

    
}
