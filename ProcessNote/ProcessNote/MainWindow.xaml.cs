using ProcessNote.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<int> browserWindows = new List<int>();
        private string sortMethod;
        private BrowserView browserViewWindow;

        public string SortMethod
        {
            get { return sortMethod; }
            set { sortMethod = value; }
        }

        public int RefreshInterval { get; set; }
        private DispatcherTimer _timer;

        public MainWindow()
        {
            string configKeyRefreshInterval = ConfigurationManager.AppSettings.Get("RefreshInterval");
            RefreshInterval = Convert.ToInt32(configKeyRefreshInterval);
            DataContext = this;
            InitializeComponent();
            CustomProcess.History.Clear();
            List<CustomProcess> stats = new List<CustomProcess>();
            sortMethod = "CPUDescending";
            Console.WriteLine("sortMethod set to: " + sortMethod);
            statsSource.ItemsSource = CustomProcess.Stats;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            generateGridView();
        }

        private void generateGridView()
        {
            if (ConfigurationManager.AppSettings.Get("IDVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(IDColumn));
            }
            if (ConfigurationManager.AppSettings.Get("NameVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(NameColumn));
            }
            if (ConfigurationManager.AppSettings.Get("NoteVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(NoteColumn));
            }
            if (ConfigurationManager.AppSettings.Get("CPUVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(CPUColumn));
            }
            if (ConfigurationManager.AppSettings.Get("MemoryVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(MemoryColumn));
            }
            if (ConfigurationManager.AppSettings.Get("StartedVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(StartedColumn));
            }
            if (ConfigurationManager.AppSettings.Get("ThreadVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(ThreadColumn));
            }

        }

        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<CustomProcess> stats = new List<CustomProcess>();
            //stats = CustomProcess.PopulateStats();
            await CustomProcess.PopulateStats();
            //statsSource.ItemsSource = Sorter.SortProcesses(stats, sortMethod);
            statsSource.ItemsSource = Sorter.SortProcesses(CustomProcess.Stats, sortMethod);
        }

        private void statsSource_Loaded(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, RefreshInterval);
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
                Console.WriteLine(exy.Message);
            }
        }

        private CustomProcess GetProcessOnMenuClick(object sender)
        {
            var menuItem = (MenuItem)sender;
            var contextMenu = (ContextMenu)menuItem.Parent;
            var item = (ListView)contextMenu.PlacementTarget;
            return (CustomProcess)item.SelectedItem;
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var processId = GetProcessOnMenuClick(sender).ID;
                Console.WriteLine("Process id: " + processId);
                CommentWindow window = new CommentWindow(processId);
                window.Show();
            }
            catch (Exception exy)
            {
                Console.WriteLine(exy.Message);
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = this.Topmost ? false : true;
        }

        private void webSearchButton_Click(object sender, RoutedEventArgs e)
        {
            browserViewWindow = new BrowserView(this, _timer, processNameQuery.Text);
            browserViewWindow.Show();
            browserWindows.Add(browserViewWindow.GetHashCode());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (browserViewWindow != null)
            {
                browserViewWindow.Close();
            }
            _timer.Stop();
        }

        private void statsSource_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                CustomProcess customProcess = (CustomProcess)statsSource.SelectedItems[0];
                processNameQuery.Text = customProcess.Name;
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Something went wrong");
            }
        }
    }

}