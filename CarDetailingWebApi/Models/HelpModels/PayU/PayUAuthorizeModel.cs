using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels
{
   public class PayUAuthorizeModel
   {
      public string access_token { get; set; }
      public string token_type { get; set; }
      public string refresh_token { get; set; }
      public string expires_in { get; set; }
      public string grant_type { get; set; }
   }
}