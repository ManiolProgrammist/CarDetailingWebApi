using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models
{
    public class OrderServices : IOrderServices
    {
        IOrdersRepository _orderRepository;
        IUserService _userService;
        public OrderServices(IOrdersRepository r, IUserService userService)
        {
            _userService = userService;
            _orderRepository = r;
        }
        public Result<Order> Add(Order item)
        {
            //TODO: Czy w tych godzinach nie ma problemu z zrobieniem
            //TODO: Czy user istnieje
            item.OrderDate = System.DateTime.Now;
            item.ExpectedStartOfOrder = item.ExpectedStartOfOrder.Value.ToLocalTime();
            item.User = null;
            item.OrdersTemplate = null;
            
            var r = _orderRepository.Add(item);
            if (r.status)
            {
                r.value.User = _userService.GetById(r.value.UserId).value;
            }
            return r;
        }


        public Result<List<Order>> Get()
        {
            var r = _orderRepository.Get();

            foreach (var rv in r.value)
            {
                rv.User = _userService.GetById(rv.UserId).value;
            }
            return r;
        }
        public Result<List<Order>> GetByUserId(int id)
        {
            var r = _orderRepository.GetByUserId(id);
            if (r.status)
            {
                foreach (var rv in r.value)
                {
                    rv.User = _userService.GetById(rv.UserId).value;
                }
            }
            return r;

        }

        public Result<List<Order>> Get(bool done)
        {
            var r = _orderRepository.Get(done);
            foreach (var rv in r.value)
            {
                rv.User = _userService.GetById(rv.UserId).value;
            }
            return r;
        }

        public Result<Order> GetById(int id)
        {
            var r = _orderRepository.GetById(id);
            if (r.status)
            {
                r.value.User = _userService.GetById(r.value.UserId).value;
            }
            return r;
        }

        public Result<List<Order>> GetStarted(bool started)
        {

            var r = _orderRepository.GetStarted(started);
            foreach (var rv in r.value)
            {
                rv.User = _userService.GetById(rv.UserId).value;
            }
            return r;
        }

        public Result<Order> Remove(int id)
        {
            var r = _orderRepository.Remove(id);
            if (r.status)
            {
                r.value.User = _userService.GetById(r.value.UserId).value;

            }
            return r;

        }

        public Result<Order> StartOrder(int orderId, bool start) //bool w razie pomyłki pracownika- może to cofnąć
        {
            var r = _orderRepository.GetById(orderId);
            if (r.status)
            {
                if (start)
                {
                    r.value.StartOfOrder = System.DateTime.Now;
                }
                else
                {
                    r.value.StartOfOrder = null;
                }
                r.value.IsOrderStarted = start;

                return this.Update(r.value);
            }
            else
            {
                return r;
            }

        }
        public Result<Order> EndOrder(int orderId, bool end)
        {
            var r = _orderRepository.GetById(orderId);
            if (r.status)
            {
                if (end)
                {
                    r.value.CompletedOrderDate = System.DateTime.Now;
                }
                else
                {
                    r.value.CompletedOrderDate = null;
                }
                r.value.IsOrderCompleted = end;

                return this.Update(r.value);
            }
            else
            {
                return r;
            }
        }
        public Result<Order> Update(Order item)
        {
            var r = _orderRepository.Update(item);
            if (r.status)
            {
                r.value.User = _userService.GetById(r.value.UserId).value;
            }
            return r;
        }

        public Result<Order> PaidOrder(int orderId, bool paid)
        {
            var r = _orderRepository.GetById(orderId);
            r.value.IsPaid = paid;
            return this.Update(r.value);
        }

    }
}