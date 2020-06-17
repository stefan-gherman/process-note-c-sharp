using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for BrowserView.xaml
    /// </summary>
    /// 
    
    public partial class BrowserView : Window
    {
        private readonly MainWindow mainWindow;
        private readonly DispatcherTimer _mainWindowTimer;
        public BrowserView(MainWindow mainWindow, DispatcherTimer timer)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this._mainWindowTimer = timer;
            _mainWindowTimer.Stop();
            browser.Navigate(new Uri("https://google.com"));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _mainWindowTimer.Start();
            
        }

       

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(this.Width < 800)
            {
                this.Width = 800;
            }

            if(this.Height < 450)
            {
                this.Height = 450;
            }
            Console.WriteLine($"Window state changed, width:{this.Width}, height:{this.Height}");
            browser.Width = this.Width - 100;
            browser.Height = this.Height - 100;
        }


    }
}
