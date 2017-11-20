using EBMTodo.Models.Line;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;

namespace EBMTodo.Lib
{
    public class Reply
    {
        public const string API_URL = "https://api.line.me/v2/bot/message/reply";
        private WebRequest req;

        public Reply(ReplyBody body)
        {
            //--- set header and body required infos ---
            req = WebRequest.Create(API_URL);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers["Authorization"] = "Bearer " + WebConfigurationManager.AppSettings["AccessToken"];

            // --- format to json and add to request body ---
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                string data = JsonConvert.SerializeObject(body);
                streamWriter.Write(data);
                streamWriter.Flush();
            }
        }

        /*
            --- send message to LINE ---
            return response data
        */
        public string send()
        {
            string result = null;
            try
            {
                WebResponse response = req.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return result;
        }
    }
}