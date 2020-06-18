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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for StatsWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        private static StatsWindow instance;
        private DispatcherTimer _mainWindowTimer;
        private MainWindow mainWindow;
        public  static StatsWindow GetInstance(MainWindow mainWindow, DispatcherTimer timer)
        {
            if (instance == null)
            {
                instance = new StatsWindow(mainWindow, timer);                    
            }
            
            return instance;
        }
        private StatsWindow(MainWindow mainWindow, DispatcherTimer timer)
        {
            this.mainWindow = mainWindow;
            _mainWindowTimer = timer;
            mainWindow.performanceViewer.IsEnabled = false;
            InitializeComponent();
            _mainWindowTimer.Stop();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _currentTimer.Stop();
            _mainWindowTimer.Start();
            mainWindow.performanceViewer.IsEnabled = true;
        }

        private void currentTimer_Tick(object sender, EventArgs e)
        {
            cpuBar.Value = (int)cpuVals.NextValue();
            cpuBarText.Text = $"{cpuBar.Value}%";
            ramBar.Value = (int)ramVals.NextValue();
            ramBarText.Text = $"{ramBar.Value}%";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _mainWindowTimer.Stop();
            _currentTimer = new DispatcherTimer();
            _currentTimer.Interval = new TimeSpan(0, 0, 1);
            _currentTimer.Tick += new EventHandler(currentTimer_Tick);
            _currentTimer.Start();
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            _currentTimer.Stop();
        }

        private void Window_DragLeave(object sender, DragEventArgs e)
        {
            _currentTimer.Start();
        }

        private void Window_DragOver(object sender, DragEventArgs e)
        {
            _currentTimer.Start();
        }
    }
}
