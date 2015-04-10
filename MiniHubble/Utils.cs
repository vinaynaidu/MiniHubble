using MiniHubble.Models.Home;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MiniHubble
{
    public static class Utils
    {
        public static void LoadConfiguration()
        {
            var initialCfg = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("/Content/config.json"));
            var configuration = JsonConvert.DeserializeObject<List<HubbleConfigModel>>(initialCfg);
            
            //start off by resetting everything 
            configuration.ForEach(r => r.Lastcontact = DateTime.Now);

            SaveMonitoringServices(configuration);
        }

        public static void PingWebservices()
        {
            var webServices = GetMonitoringServices()
                .Where(r => r.Type == "web");

            foreach(var s in webServices)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(s.Url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        UpdateServiceHeartbeat(s.Id);
                    }                    
                }catch(Exception)
                {
                    //do nothing
                }
            }
        }

        public static void UpdateServiceHeartbeat(string id)
        {
            var services = GetMonitoringServices();
            services.First(r => r.Id == id).Lastcontact = DateTime.Now;
            SaveMonitoringServices(services);
        }

        public static List<HubbleConfigModel> FetchCurrentStatus()
        {
            return GetMonitoringServices();
        }

        public static string GetStatus(DateTime lastContact, int alert)
        {
            DateTime now = DateTime.Now;
            var elapsedTime = (now - lastContact).TotalSeconds;
            
            if(elapsedTime > alert)
            {
                return "Alarm";
            }
            else
            {
                return "OK";
            }
        }

        private static List<HubbleConfigModel> GetMonitoringServices()
        { 
            return (List<HubbleConfigModel>)HttpRuntime.Cache["State"];
        }

        private static void SaveMonitoringServices(List<HubbleConfigModel> monitoringServices)
        {
            //Saving in cache
            HttpRuntime.Cache["State"] = monitoringServices;
        }
    }
}