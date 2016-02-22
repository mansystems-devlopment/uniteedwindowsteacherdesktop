using UniteEDTeacher.Serialization;
//using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
/*using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;*/

namespace UniteEDTeacher.Code
{
    class UniteEDNetwork
    {
        public class State
        {
            public HttpWebRequest WebRequest { get; set; }
            public Action<HttpWebResponse> ResponseCallBack;

        }

        private void GetCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                State state = (State)asynchronousResult.AsyncState;
                HttpWebResponse httpresponse = (HttpWebResponse)state.WebRequest.EndGetResponse(asynchronousResult);
                //Dispatch request back to ui thread


                state.ResponseCallBack(httpresponse);

            }
            catch (WebException ex)
            {

                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public void PostData(Action<HttpWebResponse> responseCallBack, string action, string postData)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(Constant.baseURL + action));

                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";

                // webRequest.ContentType = "application/json; charset=utf-8";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(postData);
               // webRequest.ContentLength = data.Length;
                //webRequest.ContentLength = boundaryBytes.Length + contentDispo.Length + lineEndsHere.Length + image.Length + lineAgainEndsHere.Length + boundaryBytes.Length;
                webRequest.BeginGetRequestStream(ar =>
                {
                    try
                    {
                        using (Stream oStream = webRequest.EndGetRequestStream(ar))
                        {


                            oStream.Write(data, 0, data.Length);

                        }
                    }
                    catch (Exception ex)
                    {
                        //Deployment.Current.Dispatcher.BeginInvoke((Action)(() => MessageBox.Show(ex.Message + "\n" + ex.StackTrace)));

                        Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);

                    }
                    webRequest.BeginGetResponse(new AsyncCallback(GetCallback),
                   new State()
                   {
                       WebRequest = webRequest,
                       ResponseCallBack = responseCallBack

                   });

                }, null);

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
            //return received;
        }
        // Post ticket
        public void PostTicket(Action<HttpWebResponse> responseCallBack, string url, string postData)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(new Uri("http://154.65.52.77/rest/" + url));

                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";

                // webRequest.ContentType = "application/json; charset=utf-8";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(postData);
                // webRequest.ContentLength = data.Length;
                //webRequest.ContentLength = boundaryBytes.Length + contentDispo.Length + lineEndsHere.Length + image.Length + lineAgainEndsHere.Length + boundaryBytes.Length;
                webRequest.BeginGetRequestStream(ar =>
                {
                    try
                    {
                        using (Stream oStream = webRequest.EndGetRequestStream(ar))
                        {


                            oStream.Write(data, 0, data.Length);

                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);

                    }
                    webRequest.BeginGetResponse(new AsyncCallback(GetCallback),
                   new State()
                   {
                       WebRequest = webRequest,
                       ResponseCallBack = responseCallBack

                   });

                }, null);

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            //return received;
        }

        public void PostMoodleData(Action<HttpWebResponse> responseCallBack, string url, string postData)
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));

                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";

                webRequest.CookieContainer = new CookieContainer();

                Guid guid = Guid.NewGuid();
                

                Cookie myCookie = new Cookie("MOODLEID", guid.ToString());
               
                CookieContainer cookie = new CookieContainer();
                cookie.Add(new Uri(url), myCookie);

                webRequest.CookieContainer = cookie;


                // webRequest.ContentType = "application/json; charset=utf-8";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(postData);
                // webRequest.ContentLength = data.Length;
                //webRequest.ContentLength = boundaryBytes.Length + contentDispo.Length + lineEndsHere.Length + image.Length + lineAgainEndsHere.Length + boundaryBytes.Length;
                webRequest.BeginGetRequestStream(ar =>
                {
                    try
                    {
                        using (Stream oStream = webRequest.EndGetRequestStream(ar))
                        { 
                            oStream.Write(data, 0, data.Length); 
                        }
                    }
                    catch (Exception ex)
                    {
                        // Deployment.Current.Dispatcher.BeginInvoke((Action)(() => MessageBox.Show(ex.Message + "\n" + ex.StackTrace)));


                        Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);

                    }
                    webRequest.BeginGetResponse(new AsyncCallback(GetCallback),
                   new State()
                   {
                       WebRequest = webRequest,
                       ResponseCallBack = responseCallBack

                   });

                }, null);
                
                
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            //return received;
        }
    }
}
