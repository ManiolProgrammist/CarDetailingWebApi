using CarDetailingWebApi.Models;
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Services.OrderServicesF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace CarDetailingWebApi.Controllers
{
   public class OrderInfoController : ApiController
   {
      IOrderInfoService _orderInfoService;
      IUserService _userService;
      public OrderInfoController(IOrderInfoService serv, IUserService userService)
      {
         _orderInfoService = serv;
         _userService = userService;

      }
      [Authorize] //roles="*"?
      [Route("api/OrderInfo/GetByOrderId/{orderId}")]
      [HttpGet]
      public Result<List<OrdersInformation>> GetByOrderId(int orderId)
      {
         return _orderInfoService.GetInfoByOrderId(orderId);
      }
      [Authorize] //roles="*"?
      [Route("api/OrderInfo/PostOrderInfo/{orderId}")]
      [HttpPost]
      // POST api/<controller>
      public Result<OrdersInformation> Post([FromBody] OrderInfoCreate info)
      {
         OrdersInformation or = new OrdersInformation();
         or.Information = info.value;
         or.OrderId = info.orderId;
         or.TypeOfInformation = "notatka";
         return _orderInfoService.Add(or);
      }

      [Authorize] //roles="*"?
      [Route("api/OrderInfo/RemoveOrderInfo/{orderInfoId}")]
      public Result<OrdersInformation> Delete(int orderInfoId)
      {
         return _orderInfoService.Remove(orderInfoId);
      }
      public class OrderInfoCreate
      {
         public string value;
         public int orderId;
         public string title;
      }
   }
}