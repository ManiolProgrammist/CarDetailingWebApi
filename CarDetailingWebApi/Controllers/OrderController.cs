using CarDetailingWebApi.Models;
using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDetailingWebApi.Controllers
{
    public class OrderController : ApiController
    {

        IOrderServices _orderService;
        public OrderController(IOrderServices serv)
        {
            _orderService = serv;
            //var kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly());
            //var UserRepo = kernel.Get<IRepository<User>>("UsersRepo");
        }
        // GET: api/Order
        public Result<List<Order>> Get()
        {
            return _orderService.Get();
        }

        // GET: api/Order/5
        public Result<Order> Get(int id)
        {
            return _orderService.GetById(id);
        }

        // POST: api/Order
        public Result<Order> Post([FromBody]Order value)
        {
            return null;
        }

        // PUT: api/Order/5
        public Result<Order> Put(int id, [FromBody]string value)
        {

            return null;
        }

        // DELETE: api/Order/5
        public Result<Order> Delete(int id)
        {
            return null;
        }
    }
}
