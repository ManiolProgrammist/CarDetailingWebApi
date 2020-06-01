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
        [HttpGet]
        [Route(("api/Order/FiltrDone/{Done}"))] //zlecenia skończone/nie skończone
        public Result<List<Order>> Get(bool Done)
        {
            return _orderService.Get(Done);
        }
        [HttpGet]
        [Route(("api/Order/FiltrStarted/{Started}"))] //zlecenia zaczęte/nie zaczęte
        public Result<List<Order>> GetStarted(bool Started)
        {
            return _orderService.GetStarted(Started);
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpPut]
        [Route("api/Order/Start/{start}")]
        public Result<Order> StartOrder([FromBody] Order order,bool start)
        {
            return _orderService.StartOrder(order.OrderId, start);
        }
        [Authorize(Roles = "Employee, Admin")]
        [HttpPut]
        [Route("api/Order/End/{end}")]
        public Result<Order> EndOrder([FromBody] Order order, bool end)
        {
            return _orderService.EndOrder(order.OrderId, end);
        }

        [Authorize(Roles = "Employee, Admin")]
        // GET: api/Order/5
        public Result<Order> Get(int id)
        {
            return _orderService.GetById(id);
        }
        [Authorize(Roles = "Employee, Admin, Normal User, Temporary User")]
        // POST: api/Order
        public Result<Order> Post([FromBody]Order value)
        {
            return _orderService.Add(value);
        }
        [Authorize(Roles = "Employee, Admin, Normal User, Temporary User")]
        // PUT: api/Order/5
        public Result<Order> Put(int id, [FromBody]string value)
        {

            return null;
        }

        [Authorize(Roles = "Employee, Admin, Normal User, Temporary User")]
        // DELETE: api/Order/5
        public Result<Order> Delete(int id)
        {
            return null;
        }
    }
}
