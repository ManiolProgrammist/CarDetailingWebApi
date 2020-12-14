using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels.PayU
{
   public class PayUOrder
   {
      public string notifyUrl { get; set; }
      public string continueUrl { get; set; }
      //public string orderId { get; set; }
      public string customerIp { get; set; }
      public string merchantPosId { get; set; }
      public string description { get; set; }
      public string currencyCode { get; set; }
      public string totalAmount { get; set; }

      public List<PayuProduct> products { get; set; }
      public BuyerPayU buyer { get; set; }

   }
}