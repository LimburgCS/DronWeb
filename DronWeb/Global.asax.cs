using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DronWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e) // metoda wywo³ywana przed kazdym zadaniem
        {
            CultureInfo newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone(); //ogolnie to formatowanie po stronie serwera
            newCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            newCulture.DateTimeFormat.DateSeparator = "-"; // ustawienia dla daty-s¹ culture info pobieranie z ukladow swiatowych
            newCulture.NumberFormat.NumberDecimalDigits = 2;
            newCulture.NumberFormat.NumberDecimalSeparator = ",";
            newCulture.NumberFormat.NumberGroupSeparator = "";
            Thread.CurrentThread.CurrentCulture = newCulture;
        }
    }
}
