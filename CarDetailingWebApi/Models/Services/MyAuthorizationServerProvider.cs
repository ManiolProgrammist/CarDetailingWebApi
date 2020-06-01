using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CarDetailingWebApi.Models.Services
{
    //Within that class, we need to write the logic for validating the user credentials and generating the access token.
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        //We need to inherit the MyAuthorizationServerProvider class from OAuthAuthorizationServerProvider 
        //class and then need to override the ValidateClientAuthentication and GrantResourceOwnerCredentials method.




        //The ValidateClientAuthentication method is used for validating the client application.
        //zakładamy ze mamy tylko jednego klienta.
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UsersRepository _repo = new UsersRepository();

            db.User user = _repo.Login(context.UserName, context.Password).value;
            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.UserType.Name));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Login));
            identity.AddClaim(new Claim("Email", user.Email));
            //identity.AddClaim(new Claim("FirstName", user.FirstName));
            //identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            //identity.AddClaim(new Claim("Surname", user.Surname));
            identity.AddClaim(new Claim("AccountCreateDate", user.AccoutCreateDate.ToString()));
            context.Validated(identity);

        }
    }



}