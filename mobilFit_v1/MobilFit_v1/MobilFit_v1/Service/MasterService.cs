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

        //public HttpRequest Post(string url, object obj)
        //{
        //    WebRequest request = WebRequest.Create(url);
        //    request.ContentType = "application/json";
        //    request.Method = "POST";
        //    request.coint
        //    //using (HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse)
        //    //{
        //    //    return BuildRequest(httpWebResponse);
        //    //}
        //}
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
        private HttpRequest BuildRequest(HttpWebRequest httpWebRequest)
        {
            using (StreamReader reader = new StreamReader(httpWebRequest.GetRequestStream()))
            {
                string content = reader.ReadToEnd();
                HttpRequest request = new HttpRequest();
                request.content = content;
                //request.httpstatuscode = httpWebRequest.StatusCode;
                return request;
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
    }
}
