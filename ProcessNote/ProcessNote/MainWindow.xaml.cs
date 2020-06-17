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
using ProcessNote.Views;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string sortMethod;

        public string SortMethod
        {
            get { return sortMethod; }
            set { sortMethod = value; }
        }
        private DispatcherTimer _timer;
        private BrowserView browserViewWindow;
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            CustomProcess.History.Clear();
            this.Topmost = false;
            List<CustomProcess> stats = new List<CustomProcess>();
            sortMethod = "CPUDescending";
            Console.WriteLine("sortMethod set to: " + sortMethod);
            //stats = CustomProcess.PopulateStats();
            //await CustomProcess.PopulateStats();
            //statsSource.ItemsSource = stats;
            statsSource.ItemsSource = CustomProcess.Stats;
        }

        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<CustomProcess> stats = new List<CustomProcess>();
            //stats = CustomProcess.PopulateStats();
            await CustomProcess.PopulateStats();
            //statsSource.ItemsSource = Sorter.SortProcesses(stats, sortMethod);
            statsSource.ItemsSource = Sorter.SortProcesses(CustomProcess.Stats, sortMethod);
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
            if (sortMethod.Equals("NameAscending"))
            {
                sortMethod = "NameDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "NameAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void ID_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ID Header clicked.");
            if (sortMethod.Equals("IDAscending"))
            {
                sortMethod = "IDDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "IDAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void Note_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Note Header clicked.");
            if (sortMethod.Equals("NoteAscending"))
            {
                sortMethod = "NoteDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "NoteAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void CPU_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("CPU Header clicked.");
            if (sortMethod.Equals("CPUAscending"))
            {
                sortMethod = "CPUDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "CPUAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void Memory_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Memory Header clicked.");
            if (sortMethod.Equals("MemoryAscending"))
            {
                sortMethod = "MemoryDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "MemoryAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void Started_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Started Header clicked.");
            if (sortMethod.Equals("StartedAscending"))
            {
                sortMethod = "StartedDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "StartedAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void Thread_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Thread Header clicked.");
            if (sortMethod.Equals("ThreadAscending"))
            {
                sortMethod = "ThreadDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "ThreadAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }
        
        private void ShowThreads_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                var processId = GetProcessOnMenuClick(sender).ID;
                ThreadsWindow window = new ThreadsWindow(processId);
                window.Show();
            }
            catch (Exception exy)
            {

            }
            
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = this.Topmost ? false : true;
        }

        private void webSearchButton_Click(object sender, RoutedEventArgs e)
        {
           
            browserViewWindow = new BrowserView(this, _timer);
            browserViewWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (browserViewWindow != null)
            {
                browserViewWindow.Close();
            }
        }
    }

        private CustomProcess GetProcessOnMenuClick(object sender)
        {
            var menuItem = (MenuItem)sender;
            var contextMenu = (ContextMenu)menuItem.Parent;
            var item = (ListView)contextMenu.PlacementTarget;
            return (CustomProcess)item.SelectedItem;
        }
    }  
}