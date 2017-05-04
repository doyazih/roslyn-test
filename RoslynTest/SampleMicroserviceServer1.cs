using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTest
{
    public class SampleMicroserviceServer1
    {
        string uri = "http://www.auction.co.kr";

        public void Get()
        {
            string result = CallApi(uri, "GET");

            Console.WriteLine(result);
        }

        public string CallApi(string uri, string httpType)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.Method = WebRequestMethods.Http.Get;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string result = reader.ReadToEnd();
            return result;
        }
    }
}
