﻿using System.IO;
using System.Net;
using System.Text;

namespace Radish.Helpers
{
    public class Http
    {
        public static ResponseResult Get(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            return ResponseResult.Text(response);
        }

        // TODO: add unit tests for put and post methods
        //        public static object Put(string url, string data)
        //        {
        //            HttpWebResponse response;
        //            var request = (HttpWebRequest)WebRequest.Create(url);
        //            string result = null;
        //            try
        //            {
        //
        //                byte[] arr = Encoding.UTF8.GetBytes(data);
        //
        //                request.Timeout = 10000;
        //                request.Method = "PUT";
        //                request.Headers.Add("X-API-KEY", "6GjfU7ru");
        //                request.ContentType = "application/x-www-form-urlencoded";
        //                request.ContentLength = arr.Length;
        //
        //                request.Accept = "text/json";
        //                request.ProtocolVersion = HttpVersion.Version11;
        //                Stream dataStream = request.GetRequestStream();
        //                dataStream.Write(arr, 0, arr.Length);
        //                dataStream.Close();
        //                response = (HttpWebResponse)request.GetResponse();
        //
        //            }
        //            catch (WebException exc)
        //            {
        //                response = (HttpWebResponse)exc.Response;
        //            }
        //            if (response != null)
        //            {
        //                // we will read data via the response stream
        //                var encoding = string.IsNullOrEmpty(response.ContentEncoding) ? Encoding.UTF8 : Encoding.GetEncoding(response.ContentEncoding);
        //                var streamReader = new StreamReader(response.GetResponseStream(), encoding);
        //                result = streamReader.ReadToEnd();
        //            }
        //
        //
        //            return result;
        //        }
        //
        //        public static object Post(string url, string data)
        //        {
        //            HttpWebResponse response;
        //            var request = (HttpWebRequest)WebRequest.Create(url);
        //            string result = null;
        //            try
        //            {
        //
        //                byte[] arr = Encoding.UTF8.GetBytes(data);
        //
        //                request.Timeout = 10000;
        //                request.Method = "POST";
        //                request.Headers.Add("X-API-KEY", "6GjfU7ru");
        //                request.ContentType = "application/x-www-form-urlencoded";
        //                request.ContentLength = arr.Length;
        //                request.KeepAlive = true;
        //                request.Accept = "text/html";
        //                request.ProtocolVersion = HttpVersion.Version11;
        //                Stream dataStream = request.GetRequestStream();
        //                dataStream.Write(arr, 0, arr.Length);
        //                dataStream.Close();
        //                response = (HttpWebResponse)request.GetResponse();
        //
        //            }
        //            catch (WebException exc)
        //            {
        //                response = (HttpWebResponse)exc.Response;
        //            }
        //            if (response != null)
        //            {
        //                // we will read data via the response stream
        //                var encoding = string.IsNullOrEmpty(response.ContentEncoding) ? Encoding.UTF8 : Encoding.GetEncoding(response.ContentEncoding);
        //                var streamReader = new StreamReader(response.GetResponseStream(), encoding);
        //                result = streamReader.ReadToEnd();
        //            }
        //
        //
        //            return result;
        //        }
    }

    public abstract class ResponseResult
    {
        public abstract string GetContent();

        public static ResponseResult Text(HttpWebResponse httpWebResponse)
        {
            return new TextResult(httpWebResponse);
        }
    }

    public class TextResult : ResponseResult
    {
        private readonly HttpWebResponse _response;
        private string _content;
        public TextResult(HttpWebResponse response)
        {
            _response = response;
        }

        public override string GetContent()
        {
            if (_content == null)
            {
                ReadContent();
            }
            return _content;
        }

        private void ReadContent()
        {
            var encoding = string.IsNullOrEmpty(_response.ContentEncoding) ? Encoding.UTF8 : Encoding.GetEncoding(_response.ContentEncoding);

            using (var reader = new StreamReader(_response.GetResponseStream(), encoding))
            {
                _content = reader.ReadToEnd();
            }
        }
    }
}
