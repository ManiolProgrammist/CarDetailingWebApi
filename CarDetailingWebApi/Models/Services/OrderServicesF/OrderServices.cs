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
        public OrderServices(IOrdersRepository r)
        {
            _orderRepository = r;
        }
        public Result<Order> Add(Order item)
        {
            //TODO: Czy w tych godzinach nie ma problemu z zrobieniem
            //TODO: Czy user istnieje
            item.OrderDate = System.DateTime.Now;
            return _orderRepository.Add(item);
        }

   

        public Result<List<Order>> Get()
        {
            return _orderRepository.Get();
        }

        public Result<List<Order>> Get(bool done)
        {
            return _orderRepository.Get(done);
        }

        public Result<Order> GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public Result<List<Order>> GetStarted(bool started)
        {
            return _orderRepository.GetStarted(started);
        }

        public Result<Order> Remove(int id)
        {
            return _orderRepository.Remove(id);
        }

        public Result<Order> StartOrder(int orderId, bool start) //bool w razie pomyłki pracownika- może to cofnąć
        {
            var r = this.GetById(orderId);
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
        public Result<Order> EndOrder(int orderId, bool end)
        {
            var r = this.GetById(orderId);
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
        public Result<Order> Update(Order item)
        {
            return _orderRepository.Update(item);
        }

        public Result<Order> PaidOrder(int orderId, bool paid)
        {
            var r = this.GetById(orderId);
            r.value.IsPaid = paid;
            return this.Update(r.value);
        }
    }
}