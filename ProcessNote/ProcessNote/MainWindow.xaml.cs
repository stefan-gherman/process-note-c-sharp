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
using System.Windows.Threading;


namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private BrowserView browserViewWindow;
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            this.Topmost = false;
            List<CustomProcess> stats = new List<CustomProcess>();


            stats = populateStats();
            stats.Sort((x, y) => y.Memory.CompareTo(x.Memory));
            statsSource.ItemsSource = stats;


        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<CustomProcess> stats = new List<CustomProcess>();

            stats = populateStats();
            stats.Sort((x, y) => y.Memory.CompareTo(x.Memory));
            statsSource.ItemsSource = stats;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    _timer = new DispatcherTimer();
        //    _timer.Interval = new TimeSpan(0, 0, 1);
        //    _timer.Tick += new EventHandler(dispatcherTimer_Tick);
        //    _timer.Start();
        //}

        List<CustomProcess> populateStats()
        {
            List<CustomProcess> result = new List<CustomProcess>();

            Process[] remoteAll = Process.GetProcesses();
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

                result.Add(new CustomProcess() { ID = id, Name = name, Note = note, CPU = cpu, Memory = memory, Started = startTime, Thread = thread });
            }


            return result;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);
            _timer.Start();
        }



        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
           this.Topmost = this.Topmost ? false : true;
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (browserViewWindow != null)
            {
                browserViewWindow.Close();
            }
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
