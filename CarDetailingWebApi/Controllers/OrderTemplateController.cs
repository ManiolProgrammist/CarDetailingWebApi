using CarDetailingWebApi.Models;

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
        [HttpGet]
        [Route(("api/OrderTemplate"))]
  
        public Result<List<OrdersTemplate>> Get()
        {
           return orderServices.Get();
        }

        // GET: api/OrderTemplate/5
        [HttpGet]
        [Route(("api/OrderTemplate/{id}"))]
        public Result<OrdersTemplate> Get(int id)
        {
            return orderServices.GetById(id);
        }
        [Authorize(Roles = "Employee, Admin")]
        // POST: api/OrderTemplate
        //post OrdersTemplate bez zdjęć
        [HttpPost]
        [Route(("api/OrderTemplate"))]
        public Result<OrdersTemplate> Post([FromBody]OrdersTemplate value)
        {
            return orderServices.Add(value);
        }

        // PUT: api/OrderTemplate/5
        [HttpPut]
        [Route(("api/OrderTemplate/{id}"))]
        [Authorize(Roles = "Employee, Admin")]
        public Result<OrdersTemplate> Put(int id, [FromBody]OrdersTemplate value)
        {
            
            return orderServices.Update(value);
        }

      // DELETE: api/OrderTemplate/5
      [HttpDelete]
      [Route(("api/OrderTemplate/{id}/{deleteOrders}"))]
      [Authorize(Roles = "Employee, Admin")]
      public Result<OrdersTemplate> Delete(int id, bool deleteOrders)
        {
            return orderServices.Remove(id, deleteOrders);
        }
        //dodaj zdjęcie do orders template, musi być jpg
        [Authorize(Roles ="Employee, Admin")]
        [HttpPost]
        [Route(("api/OrderTemplate/AddImage/{OrderTemplateId}"))]
        public Result<OrdersTemplateImage> AddImage(int OrderTemplateId)
        {
            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
       
            byte[] fileData = null;
            using (var binaryReader = new System.IO.BinaryReader(hfc[0].InputStream))
            {
                fileData = binaryReader.ReadBytes(hfc[0].ContentLength);
            }
            var ordI = new OrdersTemplateImage() { OrderTemplateId = OrderTemplateId, Image = fileData };
            return orderServices.AddImage(ordI);
        }
    }
}
