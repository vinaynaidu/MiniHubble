using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MiniHubble
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static CacheItemRemovedCallback OnCacheRemove = null;
        private const string TIMER_CACHE_KEY = "TimerCacheKey";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterCountdownTimer();
        }

        private bool RegisterCountdownTimer()
        {
            if(HttpRuntime.Cache[TIMER_CACHE_KEY] != null)
            {
                return false;
            }

            HttpRuntime.Cache.Add(
                TIMER_CACHE_KEY, 
                "timer", 
                null, 
                DateTime.MaxValue, 
                TimeSpan.FromMinutes(1), 
                System.Web.Caching.CacheItemPriority.Normal,
                new CacheItemRemovedCallback(CacheItemRemoved));

            return true;
        }

        public void CacheItemRemoved(string k, object v, CacheItemRemovedReason r)
        {
            Utils.PingWebservices();
            RegisterCountdownTimer();
        }

        void Session_Start(object sender, EventArgs e)
        {
            Utils.LoadConfiguration();
        }
    }
}