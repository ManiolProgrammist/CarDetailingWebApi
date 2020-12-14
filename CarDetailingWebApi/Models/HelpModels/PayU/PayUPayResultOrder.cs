using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels.PayU
{
   public class PayUPayResultOrder:PayUOrder
   {
      public string orderId { get; set; }
      public string status { get; set; }
   }
}