﻿using CarDetailingWebApi.Models;
using CarDetailingWebApi.Models.AuthenticationAtributes;
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Services.OrderTemplateServicesF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDetailingWebApi.Controllers
{
    public class OrderTemplateController : ApiController
    {
        IOrdersTemplateService orderServices;
        public OrderTemplateController(IOrdersTemplateService orderServices)
        {
            this.orderServices = orderServices;
        }
        // GET: api/OrderTemplate
        public Result<List<OrdersTemplate>> Get()
        {
           return orderServices.Get();
        }

        // GET: api/OrderTemplate/5
        public string Get(int id)
        {
            return "value";
        }
        [EmployeeAuthentication]
        // POST: api/OrderTemplate
        public Result<OrdersTemplate> Post([FromBody]OrdersTemplate value)
        {
            return orderServices.Add(value);
        }

        // PUT: api/OrderTemplate/5
        [EmployeeAuthentication]
        public Result<OrdersTemplate> Put(int id, [FromBody]OrdersTemplate value)
        {
            
            return orderServices.Update(value);
        }

        // DELETE: api/OrderTemplate/5
        [EmployeeAuthentication]
        public Result<OrdersTemplate> Delete(int id)
        {
            return orderServices.Remove(id);
        }
    }
}
