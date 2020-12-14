using System;
using Microsoft.Owin;
using Owin;

using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using CarDetailingWebApi.Models.Services;
using System.Web.Cors;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(CarDetailingWebApi.StartupAuthenticationServ))]

namespace CarDetailingWebApi
{
    // In this class we will Configure the OAuth Authorization Server.
    public class StartupAuthenticationServ
    {
        public void Configuration(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                //The Path For generating the Toekn
                //Here, we set the path for generating the tokens as “http://localhost:portnumber/api/login”.?

                TokenEndpointPath = new PathString("/api/login"),
                //Setting the Token Expired Time (24 hours)
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //MyAuthorizationServerProvider class will validate the user credentials
                Provider = new MyAuthorizationServerProvider()
                
            };

      
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
    
            //Token Generations
            app.UseOAuthAuthorizationServer(options);
           
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            //var cors = new EnableCorsAttribute("http://localhost:4200", "*", "*");

            //

            HttpConfiguration config = new HttpConfiguration();
            //config.EnableCors(cors);
            WebApiConfig.Register(config);

        }
    }
}
