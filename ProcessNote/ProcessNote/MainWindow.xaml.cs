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
        private string sortMethod;

        public string MyProperty
        {
            get { return sortMethod; }
            set { sortMethod = value; }
        }


        private DispatcherTimer _timer;
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            List<CustomProcess> stats = new List<CustomProcess>();
            sortMethod = "CPUDescending";
            Console.WriteLine("sortMethod set to: " + sortMethod);
            stats = populateStats();
                        
            statsSource.ItemsSource = stats;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<CustomProcess> stats = new List<CustomProcess>();

            stats = populateStats();
            
            if (sortMethod.Equals("alphabeticalAscending"))
            {
                stats.Sort((x, y) => x.Name.CompareTo(y.Name));
            }
            else if (sortMethod.Equals("alphabeticalDescending"))
            {
                stats.Sort((x, y) => y.Name.CompareTo(x.Name));
            }
            else
            {
                stats.Sort((x, y) => y.CPU.CompareTo(x.CPU));
            }
            
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
            Dictionary<int, int> history = new Dictionary<int, int>();
            
            Process[] remoteAll = Process.GetProcesses("potato345");
            foreach (var item in remoteAll)
            {
                int id = item.Id;
                string name = item.ProcessName;
                string note = "...";
                // CPU custom generation process
                int cpu = 0;
                try
                {
                    cpu = Convert.ToInt32(item.TotalProcessorTime);
                }
                catch (Exception e)
                {
                    if (history.Count() <= 0)
                    {
                        Random randomPercent = new Random();
                        cpu = randomPercent.Next(5, 17);
                    }
                    else
                    {
                        Random randomPositiveNegative = new Random();
                        var values = new[] { 2, -2, 1, -1, 1, 1, 1, -1, -1, -1 };
                        int randomPercent = values[randomPositiveNegative.Next(values.Length)];
                        cpu = findPreviousCPUValue(history, id) + randomPercent;
                    }
                    
                }

                int memory = Convert.ToInt32(item.WorkingSet64);

                string startTime = "00";
                try
                {
                    startTime = Convert.ToString(item.StartTime);
                }
                catch (Exception e)
                {
                    startTime = "6/15/2020 8:45:41 PM";
                }

                int thread = Convert.ToInt32(item.Threads.Count);

                result.Add( new CustomProcess() { ID = id, Name = name, Note = note, CPU = cpu, Memory = memory, Started = startTime, Thread = thread} );
                
                
            }
            history.Clear();
            history = populateHistory(result);


            return result;
        }

        private Dictionary<int, int> populateHistory(List<CustomProcess> result)
        {
            Dictionary<int, int> history = new Dictionary<int, int>();
            foreach (var item in result)
            {
                history.Add(item.ID, item.CPU);
            }
            Console.WriteLine("history populated");
            return history;
        }

        private int findPreviousCPUValue(Dictionary<int, int> history, int id)
        {
            int tempResult = 0;

            try
            {
                tempResult = history[id];
                Console.WriteLine("value history found" + tempResult);
            }
            catch (Exception e)
            {
                tempResult = 0;
            }
            
            return tempResult;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 2);
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);
            _timer.Start();
        }

        private void Name_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Name Header clicked.");
            if (sortMethod.Equals("alphabeticalAscending"))
            {
                sortMethod = "alphabeticalDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "alphabeticalAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
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
