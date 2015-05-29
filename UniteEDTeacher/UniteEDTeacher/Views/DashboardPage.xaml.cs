using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Popups;
using System.Runtime.InteropServices;
using Windows.System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniteEDTeacher.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : Page
    {
        public Dashboard()
        {
            this.InitializeComponent();

        }

        private void mybooks_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MyBooksPage));
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void Image_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(SettingsPage));
        }

        private void signin_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(LoginToClassPage));
        }

        private void mycourses_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MyCoursesPage));
        }

        private void myapps_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AppsPage));
        }

        private void myschool_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MyschoolsPage));
        }
        private void myshop_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MyShopPage));
        }

        private void chat_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ChatPage));
        
        }
        private void help_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(SelfCareMenuPage));
            
        }

        private void cloudbanc_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Cloudbanc));
        }
 
        private void mycalendar_Click(object sender, RoutedEventArgs e)
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
        async void GetCalendar()
        {
            string uriToLaunch = @"http://apps.microsoft.com/windows/en-us/app/gmail-calendar/434a71b4-9f02-4e73-a5c7-c0eeeac63e7a/";
            var uri = new Uri(uriToLaunch);

            // Set app
            var options = new Windows.System.LauncherOptions();
            options.PreferredApplicationPackageFamilyName = "BRICKMAKERS.gmailcalendar_wps5hyj3streg";
            options.PreferredApplicationDisplayName = "Calenda URI App";

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
            GetCalendar(); 
        }

        private async void myfiles_Click(object sender, RoutedEventArgs e)
        {
            var options = new Windows.System.LauncherOptions();
            options.TreatAsUntrusted = true;
            var ur1 = "http://apps.microsoft.com/windows/en-gb/app/onedrive-for-business/d78bf57e-27fe-403e-b49b-701dedfdbf9e?ocid=Apps";
            Uri ur = new Uri(ur1);

            await Windows.System.Launcher.LaunchUriAsync(ur, options);
        }

        private void Image_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Activate));
        }

        
    }
}
