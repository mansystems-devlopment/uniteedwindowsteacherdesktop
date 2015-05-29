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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniteEDTeacher.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChatPage : UniteEDTeacher.Common.LayoutAwarePage
    {
        public ChatPage()
        {
            this.InitializeComponent();
        }

        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void CommandInvokedHandler(IUICommand command)
        {
            // Display message showing the label of the command that was invoked
            //Frame rootFrame = Window.Current.Content as Frame;
            //rootFrame.Navigate(typeof(AppStorePage));
        }

        private async void BtnWechat_Click(object sender, RoutedEventArgs e)
        {

            var options = new Windows.System.LauncherOptions();
            options.TreatAsUntrusted = true;
            var ur1 = "http://apps.microsoft.com/windows/en-us/app/8407d7da-b3a6-4ead-b617-351acf00ec1f?ocid=Apps_Search_WOL_en-us_search-main_search-results-from_search-weChat_image_guide-wechat-on-pc";
            Uri ur = new Uri(ur1);

            await Windows.System.Launcher.LaunchUriAsync(ur, options);

            //MessageDialog dlg = new MessageDialog("Application is not installed. To use this feature download The Application", "Information");
            //dlg.Commands.Add(new UICommand("Download App", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            //dlg.Commands.Add(new UICommand("Close"));

            //// Set the command that will be invoked by default
            //dlg.DefaultCommandIndex = 0;

            //// Set the command to be invoked when escape is pressed
            //dlg.CancelCommandIndex = 1;
            //await dlg.ShowAsync();
           
        }

        private async void BtnChat_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog dlg = new MessageDialog("Cooming Soon", "Information");
            await dlg.ShowAsync();
        }

    }
}
