using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
using Windows.UI.Xaml.Navigation; 


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace UniteEDTeacher.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MyBooksPage : UniteEDTeacher.Common.LayoutAwarePage
    {
         

        public MyBooksPage()
        {
            this.InitializeComponent(); 
            
        } 
        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        } 

        private async void BtnEbookReader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string exeFile = @"C:\Program Files (x86)\MyStudies\MyStudies\MyStudies.exe";

                var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(exeFile);

                if (file != null)
                {
                    // Set the option to show the picker
                    var options = new Windows.System.LauncherOptions();
                    options.DisplayApplicationPicker = true;

                    // Launch the retrieved file
                    bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
                    if (success)
                    {
                        // File launched
                    }
                    else
                    {
                        // File launch failed
                    }
                }
            }
            catch (Exception ex)
            {

            }
             
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            DefaultLaunch(); 
        }
          
        // Launch the URI
        async void DefaultLaunch()
        {
            // The URI to launch
            string uriToLaunch = @"https://www.mystudies.co.za/DownloadApp/windows";

            // Create a Uri object from a URI string 
            var uri = new Uri(uriToLaunch);

            // Launch the URI
            var success = await Windows.System.Launcher.LaunchUriAsync(uri);

            if (success)
            {
                // URI launched
            }
            else
            {
                // URI launch failed
            }           
        }

 
        private void BtnSchoolBooks_Click(object sender, RoutedEventArgs e)
        {

            MessageDialog dlg = new MessageDialog("Application is not installed. To use this feature download The Application", "Information");
            dlg.Commands.Add(new UICommand("Download App", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            dlg.Commands.Add(new UICommand("Close"));

            // Set the command that will be invoked by default
            dlg.DefaultCommandIndex = 0;

            // Set the command to be invoked when escape is pressed
            dlg.CancelCommandIndex = 1;
            dlg.ShowAsync();   
             
            
        }

         
         
    }
}
