using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobilFit_v1.Service
{
    class MasterService
    {
        public HttpResponse Get(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse)
            {
                return BuildResponse(httpWebResponse);
            }
        }

        public HttpResponse Post(string url, string json)
        {
            var data = Encoding.ASCII.GetBytes(json);
            WebRequest request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.ContentLength = data.Length;

            using (HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse)
            {
                return BuildRequest(httpWebResponse);
            }
        }

        //realiza las conversiones de llamadas json de mandera sincrona
        private HttpResponse BuildResponse(HttpWebResponse httpWebResponse)
        {
            using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string content = reader.ReadToEnd();
                HttpResponse response = new HttpResponse();
                response.content = content;
                response.httpstatuscode = httpWebResponse.StatusCode;
                return response;
            }
        }
        private HttpResponse BuildRequest(HttpWebResponse httpWebResponse)
        {
            using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string content = reader.ReadToEnd();
                HttpResponse response = new HttpResponse();
                response.content = content;
                response.httpstatuscode = httpWebResponse.StatusCode;
                return response;
            }
        }
        //realiza las conversiones de llamadas json de mandera asincrona
        public async Task<HttpResponse> GetAsync(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (HttpWebResponse httpWebResponse = await request.GetResponseAsync() as HttpWebResponse)
            {
                return BuildResponse(httpWebResponse);
            }
        }

        //public async Task<Response> CheckConnection()
        //{
        //    if (!CrossConnectivity.Current.IsConnected)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = "Please turn on your internet settings.",
        //        };
        //    }

        //    var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
        //        "google.com");
        //    if (!isReachable)
        //    {
        //        return new Response
        //        {
        //            IsSuccess = false,
        //            Message = "Check you internet connection.",
        //        };
        //    }

        //    return new Response
        //    {
        //        IsSuccess = true,
        //        Message = "Ok",
        //    };
        //}
    }
}
