using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace CarDetailingWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        //TODO: SPRAWDŹ KIEDY TO SIĘ WYKONUJE
        //CZY WYKONUJE SIĘ TEŻ STARTUP AUTHENTICATION SERVETC
        protected void Application_Start()
        { //1
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
