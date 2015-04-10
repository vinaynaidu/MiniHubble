using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniHubble.Models.Home
{
    public class ServiceStatusModel
    {
        public List<PayloadObj> payload { get; set; }

        public ServiceStatusModel()
        {
            payload = new List<PayloadObj>();
        }

        public ServiceStatusModel(List<Models.Home.HubbleConfigModel> services)
            : this()
        {
            foreach (var s in services)
            {
                payload.Add(new PayloadObj
                    {
                        Name = s.Name,
                        Status = Utils.GetStatus(s.Lastcontact, s.Alert),
                        LastSeen = (int)(DateTime.Now - s.Lastcontact).TotalSeconds
                    });
            }
        }

        public class PayloadObj
        {
            public string Name { get; set; }
            public string Status { get; set; }
            public int LastSeen { get; set; }

        }
    }
}