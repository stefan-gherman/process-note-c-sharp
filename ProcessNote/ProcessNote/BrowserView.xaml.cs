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
        private string _stringSearchEngineSearchQuery;
        private readonly string bingQuery = "https://www.bing.com/search?q=widnows+process+";
        public string StringSearchEngineSearchQuery
        {
            get { return _stringSearchEngineSearchQuery; }
            set { _stringSearchEngineSearchQuery = value;  }
        }
        public BrowserView(MainWindow mainWindow, DispatcherTimer _timer, string processName)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this._mainWindowTimer = _timer;
            if(processName != "")
            {
                StringSearchEngineSearchQuery = bingQuery + processName;
                webView.Source = new Uri(StringSearchEngineSearchQuery);
            } else
            {
                webView.Source = new Uri(gitHubURI);
            }

           
        }

      

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow.browserWindows.Remove(this.GetHashCode());
            if(MainWindow.browserWindows.Count == 0 )
            {
                
                _mainWindowTimer.Start();
               
            }
               
        }

        private void webView_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            _mainWindowTimer.Start();
        }

        private void webView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            _mainWindowTimer.Stop();
        }
    }
    }

      