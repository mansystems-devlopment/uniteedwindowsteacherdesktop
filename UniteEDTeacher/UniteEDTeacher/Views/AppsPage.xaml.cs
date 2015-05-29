using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace UniteEDTeacher.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AppsPage : UniteEDTeacher.Common.LayoutAwarePage
    {
        public AppsPage()
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

        private void BtnGoogleApps_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(GoogleAppsPage));
        }

        private void BtnDocuments_Click(object sender, RoutedEventArgs e)
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
        //Get Calendar if installed
        async void GetDocStore()
        {
            string uriToLaunch = @"http://apps.microsoft.com/windows/en-us/app/c26f5008-7dd5-44c5-b64e-0e2d1afa92b9?ocid=Apps_Search_WOL_en-us_search-main_search-results-from_search-spread-sheet_text-link_cloud-office";
            var uri = new Uri(uriToLaunch);

            // Set app
            var options = new Windows.System.LauncherOptions();
            options.PreferredApplicationPackageFamilyName = "28684AlotaSolution.CloudOffice_dy56wgsyc94e6";
            options.PreferredApplicationDisplayName = "Cloud Office App";

            // Launch the URI and pass in app
            var success = await Windows.System.Launcher.LaunchUriAsync(uri, options);
            if (success)
            {
                // URI launched
            }
            else
            {
                // URI launch failed
            }  
        }
        private void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            GetDocStore(); 
        }

        private void BtnSheet_Click(object sender, RoutedEventArgs e)
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
