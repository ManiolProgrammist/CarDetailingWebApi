using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels.PayU
{
   public class PayUNotification
   {
      public NotificationPayuOrder order { get; set; }
      public List<NotificationProperties> properties { get; set; }
   }

   public class NotificationPayuOrder : PayUOrder
   {
      public string status { get; set; }
      public string orderId { get; set; }
   }
   public class NotificationProperties
   {
      public string name { get; set; }
      public string value { get; set; }
   }
}