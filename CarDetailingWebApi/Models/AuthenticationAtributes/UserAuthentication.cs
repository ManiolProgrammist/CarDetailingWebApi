using CarDetailingWebApi.App_Start;
using CarDetailingWebApi.Models.HelpModels;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CarDetailingWebApi.Models.Authentication
{
    public class UserAuthentication: AuthorizationFilterAttribute
    {
        IUserService _userServices = (IUserService)NinjectWebCommon.GetKernel().Get<IUserService>();


        //IUserService userService;
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string token = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                //username:password
                string username = decodedToken.Split(':')[0];
                string password = decodedToken.Split(':')[1];
                if (!(_userServices.CheckAuthority(username, password) >= AuthorizationEnum.TemporaryUser))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            base.OnAuthorization(actionContext);
        }
    }
}
