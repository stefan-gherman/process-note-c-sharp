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
            _mainWindowTimer.Start();
            mainWindow.performanceViewer.IsEnabled = true;
        }
    }
}
