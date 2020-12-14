﻿using CarDetailingWebApi.Models;
using CarDetailingWebApi.Models.HelpModels;
using CarDetailingWebApi.Models.HelpModels.PayU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CarDetailingWebApi.Controllers
{
   public class PayUController : ApiController
   {

      IOrderServices _orderService;
      IUserService _userService;
      public PayUController(IOrderServices serv, IUserService userv)
      {
         _orderService = serv;
         _userService = userv;
         //var kernel = new StandardKernel();
         //kernel.Load(Assembly.GetExecutingAssembly());
         //var UserRepo = kernel.Get<IRepository<User>>("UsersRepo");
      }


      [HttpPost]
      [Route("api/PayStatus")]
      public HttpResponseMessage PostPayuPayResult([FromBody] string result)
      {
         //HttpRequestMessage request,
         return new HttpRequestMessage().CreateResponse(HttpStatusCode.OK);
      }

      [HttpGet]
      [Route("api/PayU/{id}")]
      // POST: api/PayU/id
      public async Task<Result<PayUOrderRequestResult>> Post(int id)
      {
         var apiUrl = Url.Content("~/");
         //apiUrl = apiUrl.Replace("http","https");
         var orderResult = _orderService.GetById(id);
         var rV = new Result<PayUOrderRequestResult>();
         rV.status = false;
         rV.info = orderResult.info;
         if (true == orderResult.status)
         {
            rV = await PayUHelper.OrderPayU(orderResult.value, apiUrl + "api/PayStatus");
            if (true == rV.status)
            {
               if (null != rV.value)
               {
                  if ("" != rV.value.orderId)
                  {
                     var orderUpd = orderResult.value;
                     orderUpd.PayUOrderId = rV.value.orderId;
                     var resUpd = _orderService.Update(orderUpd);
                     if (false == resUpd.status)
                     {
                        rV.status = false;
                        rV.info = resUpd.info;
                        rV.value = null;
                     }
                  }
               }
            }
         }
         return rV;
      }

      //// PUT: api/PayU/5
      //public void Put(int id, [FromBody] string value)
      //{
      //}

      //// DELETE: api/PayU/5
      //public void Delete(int id)
      //{
      //}
   }
}