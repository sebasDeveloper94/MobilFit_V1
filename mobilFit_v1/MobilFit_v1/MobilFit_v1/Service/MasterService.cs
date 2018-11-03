using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MobilFit_v1.Service
{
    class MasterService
    {
        const string url = "https://mobilfitapi.azurewebsites.net/";

        public HTTPResponse Get(string url) {

            WebRequest request = WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse)
            {
                return BuildResponse(httpWebResponse);
            }
        }

        private HTTPResponse BuildResponse(HttpWebResponse httpWebResponse)
        {
            using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string content = reader.ReadToEnd();
                var response = new HTTPResponse();
                response.content = content;
                response.httpstatuscode = httpWebResponse.StatusCode;
                return response;
            }
        }

    }
}
