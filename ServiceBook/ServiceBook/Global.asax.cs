using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ServiceBook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.Timers.Timer myTimer = new System.Timers.Timer();
            // Set the Interval to 1 hour  (3 600 000 milliseconds).
            myTimer.Interval = 3600000;
            myTimer.AutoReset = true;
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
        }
        public void myTimer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            // use your mailer code
            clsScheduleMail objScheduleMail = new clsScheduleMail();
            objScheduleMail.SendScheduleMail();
        }
    }
}
