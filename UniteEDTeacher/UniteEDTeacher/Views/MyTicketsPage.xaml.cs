using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;
using SQLite;
//using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
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
    public sealed partial class MyTicketsPage : UniteEDTeacher.Common.LayoutAwarePage
    {
        public MyTicketsPage()
        {
            this.InitializeComponent();
        }
        private async void BindMyTickets()
        {
            var path = ApplicationData.Current.LocalFolder.Path + @"\SapientWindows.sqlite";
            var db = new SQLiteAsyncConnection(path);
            List<Ticket> allTickets = await db.QueryAsync<Ticket>("Select * From Ticket");

            foreach (var item in allTickets)
            {
                DateTime dt = DateTime.UtcNow;
                item.DateIssued = dt.ToString();
            }
            lsMyTickets.ItemsSource = allTickets;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            BindMyTickets();
        }

        private async void lsMyTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsMyTickets.SelectedItem == null)
                return;

            var ko = (Ticket)lsMyTickets.SelectedItem;
            string filePath = "selectedTicket";
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile isolatedStorage = await localFolder.CreateFileAsync(filePath, CreationCollisionOption.ReplaceExisting);
            

                using (IRandomAccessStream fileStream = await isolatedStorage.OpenAsync(FileAccessMode.ReadWrite))
                {
                    string writeDate = string.Empty;

                    // Json serialization.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Ticket objSelectedTicket;

                        var ser = new DataContractJsonSerializer(typeof(Ticket));
                        ser.WriteObject(ms, ko);
                        ms.Seek(0, SeekOrigin.Begin);
                        var reader = new StreamReader(ms);
                        writeDate = reader.ReadToEnd();

                        // Save to StorageFile.
                        DataWriter writer = new DataWriter(fileStream);

                        writer.WriteString(writeDate);
                        await writer.StoreAsync();

                        //Read
                        byte[] byteArray = System.Text.Encoding.Unicode.GetBytes(writeDate);
                        MemoryStream stream = new MemoryStream(byteArray);
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Ticket));
                        objSelectedTicket = (Ticket)serializer.ReadObject(stream);

                        MessageDialog dlg = new MessageDialog("I have a : " + objSelectedTicket.IHave + "\r\n" + "With my : " + objSelectedTicket.WithMy + "\r\n"
                            + "Description :" + objSelectedTicket.Description + "\r\r" + "Date Issue : " + objSelectedTicket.DateIssued);
                        dlg.ShowAsync();

                        // Reset. 
                    }
                }
                isolatedStorage.DeleteAsync();
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
    }
}
