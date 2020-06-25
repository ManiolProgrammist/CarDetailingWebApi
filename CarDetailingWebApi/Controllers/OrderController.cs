using CarDetailingWebApi.Models;
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace CarDetailingWebApi.Controllers
{
    public class OrderController : ApiController
    {

        IOrderServices _orderService;
        IUserService _userService;
        public OrderController(IOrderServices serv, IUserService userv)
        {
            _orderService = serv;
            _userService = userv;
            //var kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly());
            //var UserRepo = kernel.Get<IRepository<User>>("UsersRepo");
        }
        [HttpGet]
        [Authorize(Roles = "Employee, Admin")] //roles="*"?
        [Route(("api/Order"))]
        // GET: api/Order
        //get all orders
        public Result<List<Order>> Get()
        {
            return _orderService.Get();
        }
        [HttpGet]
        [Authorize] //roles="*"?
        [Route("api/UserOrders")]
        public Result<List<Order>> GetOrdersOfUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var us = _userService.GetByLogin(identity.Name).value.UserId;
            return _orderService.GetByUserId(us);
        }


        [HttpGet]
        [Route("api/Order/FiltrDone/{Done}")] //zlecenia skończone/nie skończone
        public Result<List<Order>> Get(bool Done)
        {
            return _orderService.Get(Done);
        }
        [HttpGet]
        [Route("api/Order/FiltrStarted/{Started}")] //zlecenia zaczęte/nie zaczęte
        public Result<List<Order>> GetStarted(bool Started)
        {
            return _orderService.GetStarted(Started);
        }
        [Authorize(Roles = "Employee,Admin")]
        [HttpPut]
        [Route("api/Order/Start/{start}")]
        public Result<Order> StartOrder([FromBody] Order order, bool start)
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

        [Authorize]
        [Route("api/Order/{id}")]
        // GET: api/Order/5
        public Result<Order> Get(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = (identity).Claims
                 .Where(c => c.Type == ClaimTypes.Role)
                 .Select(c => c.Value).FirstOrDefault();
            var res = _orderService.GetById(id);
            if (roles == "Employee" || roles == "Admin") //employee i admin mogą wyświetlać informacje użytkowników
            {

                res.value.User = _userService.GetById(res.value.UserId).value;
                return res;
            }
            else
            {
                var user = _userService.GetByLogin(identity.Name).value;
                res = _orderService.GetById(id);
                if (res.status && user != null) //użytkownik sprawdzający detale swoje też może wyświetlić informacje o użytkowniku
                {
                    if (user.UserId == res.value.UserId)
                    {
                        res.value.User = user;
                        return res;
                    }
                }
            }
            res.value = null;
            res.status = false;
            res.info = "Brak uprawnień do wyświetlania tego zlecenia";
            return res;
        }
        //[Authorize(Roles = "Employee,Admin,Normal User,Temporary User")]
        // POST: api/Order
        [Authorize]
        [HttpPost]
        [Route("api/Order")]
        public Result<Order> Post([FromBody] Order value)
        {
            //if (value.ExpectedStartOfOrder != null)
            //{
            //    value.ExpectedStartOfOrder=value.ExpectedStartOfOrder.Value.ToLocalTime();
            //}
            //value.ExpectedStartOfOrder = new DateTime(value.ExpectedStartOfOrder.Value.Year, 
            //    value.ExpectedStartOfOrder.Value.Month, 
            //    value.ExpectedStartOfOrder.Value.Day, 
            //    value.ExpectedStartOfOrder.Value.Hour, 
            //    value.ExpectedStartOfOrder.Value.Minute,0);

            return _orderService.Add(value);
        }
        [Route("api/OrderTU")]
        [HttpPost]
        public Result<Order> PostFromTemporary([FromBody]OrderEmail value)
        {
            //TODO: Sprawdź Order przed CreateTemporaryUser 
            var U = _userService.CreateTemporaryUser(value.email); //tutaj też wysyłamy emaila
            var R = new Result<Order>();
            if (U.status)
            {
                value.order.UserId = U.value.UserId;
                value.order.CreateOrderUserId = U.value.UserId;
                R= _orderService.Add(value.order);
                if (R.status)
                {//przesyłamy informacje o temporary userze spowrotem
                    R.value.User = U.value;
                }
                return R;
            }
            R.status = false;
            R.info = U.info;
            return R;
        }

        [Authorize(Roles = "Employee,Admin,Normal User,Temporary User")]
        // PUT: api/Order/5
        public Result<Order> Put(int id, [FromBody]string value)
        {

            return null;
        }

        [Authorize(Roles = "Employee,Admin,NormalUser,Temporary User")]
        // DELETE: api/Order/5
        public Result<Order> Delete(int id)
        {
            return null;
        }

        public class OrderEmail
        {
            public Order order;
            public string email;
        }
    }
}
