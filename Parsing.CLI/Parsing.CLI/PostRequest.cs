using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace Parsing.CLI
{
    public class PostRequest
    {

        HttpWebRequest _request;
        string _address;

        public Dictionary<string, string> Headers { get; set; }

        public string Response { get; set; }
        public string Accept { get; set; }
        public string Host { get; set; }
        public string Data { get; set; }
        public string ContentType { get; set; }
        public WebProxy Proxy { get; set; }
        public string Referer { get; set; }
        public string Useragent { get; set; }

        public PostRequest(string address)
        {
            _address = address;
            Headers = new Dictionary<string, string>();
        }

        public string Run(CookieContainer cookieContainer)
        {
            _request = (HttpWebRequest)WebRequest.Create(_address);
            _request.Method = "Post";
            _request.CookieContainer = cookieContainer;
            _request.Proxy = Proxy;
            _request.Accept = Accept;
            _request.Host = Host;
            _request.ContentType = ContentType;
            _request.Referer = Referer;
            _request.UserAgent = Useragent;
            _request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;



            byte[] sentData = Encoding.UTF8.GetBytes(Data);
            Stream sendStream = _request.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();



            foreach (var pair in Headers)
            {
                _request.Headers.Add(pair.Key, pair.Value);
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)_request.GetResponse();
                Stream newStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(newStream);
                var result = streamReader.ReadToEnd();

                return result;

            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
