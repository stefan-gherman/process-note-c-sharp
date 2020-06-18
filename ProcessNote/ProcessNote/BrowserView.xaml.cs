    using System;
using System.Collections.Generic;
using System.Configuration;
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
        
        public string StringSearchEngineSearchQuery
        {
            get { return _stringSearchEngineSearchQuery; }
            set { _stringSearchEngineSearchQuery = value; }
        }
        public BrowserView(MainWindow mainWindow, DispatcherTimer _timer, string processName)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this._mainWindowTimer = _timer;
            if (processName != "")
            {
                StringSearchEngineSearchQuery = searchEngineSelection(processName);
                try 
                {
                    webView.Source = new Uri(StringSearchEngineSearchQuery);
                    mainWindow.ParseError = false;
                } catch(UriFormatException ex)
                {
                    BrowserView wasRemoved;
                    MessageBox.Show("Url Parse Error, check App.config to make sure the right values are stored.");
                    MainWindow.openBrowserWindows.TryRemove(this.GetHashCode(), out wasRemoved);
                    mainWindow.ParseError = true;
                }
                
            }
            else
            {
                webView.Source = new Uri(gitHubURI);
            }


        }



        private void Window_Closed(object sender, EventArgs e)
        {
            BrowserView wasRemoved;
            MainWindow.openBrowserWindows.TryRemove(this.GetHashCode(),out wasRemoved);
            if (MainWindow.openBrowserWindows.Count == 0)
            {

                _mainWindowTimer.Start();

            }
            webView = null;

        }

        private void webView_NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            ButtonsEnablerDisabler();
            _mainWindowTimer.Start();
        }

        private void webView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            ButtonsEnablerDisabler();
            _mainWindowTimer.Stop();
           
        }

        private void backwardButton_Click(object sender, RoutedEventArgs e)
        {


            webView.GoBack();


        }

        private void forwardButton_Click(object sender, RoutedEventArgs e)
        {

            webView.GoForward();
        }

        private void ButtonsEnablerDisabler()
        {
            if (!webView.CanGoBack)
            {
                backwardButton.IsEnabled = false;
            }
            else
            {
                backwardButton.IsEnabled = true;
            }
            if (!webView.CanGoForward)
            {
                forwardButton.IsEnabled = false;
            }
            else
            {
                forwardButton.IsEnabled = true;
            }
        }

        private string searchEngineSelection(string process)
        {
            string bingQuery = "https://www.bing.com/search?q=widnows+process+";
            string googleQuery = "https://www.google.com/search?q=windows+process+";
            string duckduckQuery = "https://duckduckgo.com/?q=windows+process+";
            string aolQuery = "https://search.aol.com/aol/search;_ylt=AwrJ7FjUnepeD9oAcTlpCWVH;_ylc=X1MDMTE5NzgwMzg4MQRfcgMyBGZyA25hBGdwcmlkAzFSWU8wWmdGUUJxQlhZZkxDeFBRMEEEbl9yc2x0AzAEbl9zdWdnAzEwBG9yaWdpbgNzZWFyY2guYW9sLmNvbQRwb3MDMARwcXN0cgMEcHFzdHJsAzAEcXN0cmwDMTIEcXVlcnkDeWFob28lMjBzZWFyY2gEdF9zdG1wAzE1OTI0MzQxNTQ-?q=windows+process+";
            string yahooQuery = "https://search.yahoo.com/search;_ylt=AwrJ7J8Fr.peYqEASyRXNyoA;_ylc=X1MDMjc2NjY3OQRfcgMyBGZyAwRmcjIDc2ItdG9wBGdwcmlkA2dlVFpwM2tZUnJ1UTVyQVpIdElnR0EEbl9yc2x0AzAEbl9zdWdnAzEwBG9yaWdpbgNzZWFyY2gueWFob28uY29tBHBvcwMwBHBxc3RyAwRwcXN0cmwDMARxc3RybAMxMwRxdWVyeQN5YW5kZXglMjBzZWFyY2gEdF9zdG1wAzE1OTI0Mzg1NDA-?p=windows+process+";
            switch (ConfigurationManager.AppSettings.Get("DefaultSearchEngine"))
            {
                case "bing":
                    return bingQuery + process;
                case "google":
                    return googleQuery + process;
                case "duckduckgo":
                    return duckduckQuery + process;
                case "aolsearch":
                    return aolQuery + process;
                case "yahoosearch":
                    return yahooQuery + process;
                case "custom":
                    return ConfigurationManager.AppSettings.Get("CustomSearchEngine") + process;
                default:
                    return bingQuery + process;

            }
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            webView.Stop();
            webView = null;
        }
    }
}

