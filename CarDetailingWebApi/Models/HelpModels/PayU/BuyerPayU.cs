using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels.PayU
{
   public class BuyerPayU
   {
      public string email { get; set; }
      public string phone { get; set; }
      public string firstName { get; set; }
      public string lastName { get; set; }
      public string language { get; set; }
               
   }
}