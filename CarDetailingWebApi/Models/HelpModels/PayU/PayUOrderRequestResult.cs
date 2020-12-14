using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels
{
   public class PayUOrderRequestResult
   {
      public PayUStatus status { get; set; }
      public string redirectUri { get; set; }
      public string orderId { get; set; }
      public string extOrderId { get; set; }
   }
   public class PayUStatus
   {
      public string statusCode { get; set; }

   }


}
