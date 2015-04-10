using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniHubble.Models.Home
{
    public class HubbleConfigModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int Alert { get; set; }
        public string Url { get; set; }
        public DateTime Lastcontact { get; set; }
    }
}