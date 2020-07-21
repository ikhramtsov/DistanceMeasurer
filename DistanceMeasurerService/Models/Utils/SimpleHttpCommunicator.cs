using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DistanceMeasurerService.Models.Utils
{
    public class SimpleHttpCommunicator
    {
        public async Task<string> SendGetRequest(string url)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;   //  set the TLS Protocol

            // Create the web request
            HttpWebRequest request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            //  request.ServicePoint.Expect100Continue = false;
            // Set type to POST
            request.Method = "GET";
            request.KeepAlive = false;
            request.ServicePoint.Expect100Continue = false;
            request.ServicePoint.UseNagleAlgorithm = false;
            // Сообщаем серверу, что умеем работать с zip
            request.Headers.Add("Accept-Encoding", "gzip");

            try
            {
                string result = null;
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    Stream buffer = null;
                    if (response.ContentEncoding != "gzip")
                        buffer = new BufferedStream(response.GetResponseStream());
                    else
                        buffer = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);

                    using (StreamReader reader = new StreamReader(buffer))
                        result = await reader.ReadToEndAsync();

                    buffer.Dispose();
                }
                
                return result;
            }
            catch (WebException wex)
            {
                string errorMessage = String.Format("Status Code: {0}; Message: {1}", wex.Status.ToString(), wex.Message);
                if (wex.Response != null)
                    using (var sr = new StreamReader(wex.Response.GetResponseStream()))
                    {
                        Stream buffer = null;
                        if (wex.Response is HttpWebResponse && ((HttpWebResponse)wex.Response).ContentEncoding != "gzip")
                            buffer = new BufferedStream(wex.Response.GetResponseStream());
                        else
                            buffer = new GZipStream(wex.Response.GetResponseStream(), CompressionMode.Decompress);

                        using (StreamReader reader = new StreamReader(buffer))
                            errorMessage = await reader.ReadToEndAsync();

                        buffer.Dispose();
                    }
                throw new Exception("Provider failed to process request: " + errorMessage);
            }
        }

    }
}
