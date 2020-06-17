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
using Microsoft.Web.WebView2.Core;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for BrowserView.xaml
    /// </summary>
    /// 
   
    public partial class BrowserView : Window
    {
        private readonly string gitHubURI = "https://github.com/cdne/process-note-c-sharp";
        private MainWindow mainWindow;
        private DispatcherTimer _mainWindowTimer;
        public BrowserView(MainWindow mainWindow, DispatcherTimer _timer)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this._mainWindowTimer = _timer;
            webView.Source = new Uri(gitHubURI);
            //_mainWindowTimer.Stop();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //_mainWindowTimer.Start();
        }


    }
    }

      