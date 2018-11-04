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
        const string url = "https://mobilfitapi.azurewebsites.net/";

        public HTTPResponse Get(string url)
        {

            WebRequest request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse)
            {
                return BuildResponse(httpWebResponse);
            }
        }
        //realiza las conversiones de llamadas json de mandera sincrona
        private HTTPResponse BuildResponse(HttpWebResponse httpWebResponse)
        {
            using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string content = reader.ReadToEnd();
                HTTPResponse response = new HTTPResponse();
                response.content = content;
                response.httpstatuscode = httpWebResponse.StatusCode;
                return response;
            }
        }
        //realiza las conversiones de llamadas json de mandera asincrona
        public async Task<HTTPResponse> GetAsync(string url)
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
