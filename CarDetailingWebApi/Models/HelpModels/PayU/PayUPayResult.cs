using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels.PayU
{
   public class PayUPayResult
   {
      public PayUPayResultOrder order { get; set; }
      public string localReceiptDateTime { get; set; }
      public List<Properties> properties { get; set; }
   }

   public class Properties
   {
      public string name { get; set; }
      public string value { get; set; }

   }
}