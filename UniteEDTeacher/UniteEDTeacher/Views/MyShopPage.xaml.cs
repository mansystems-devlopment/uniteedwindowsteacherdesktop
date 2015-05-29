using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class MyShopPage : UniteEDTeacher.Common.LayoutAwarePage
    {
        ApplicationDataContainer settings;

        public MyShopPage()
        {
            this.InitializeComponent();
            settings = Windows.Storage.ApplicationData.Current.LocalSettings;

            string url = "https://sapientmedia.redtouchmedia.com";
            WebviewMedia.Navigate(new Uri(url));
        }
         
        
    }
}
