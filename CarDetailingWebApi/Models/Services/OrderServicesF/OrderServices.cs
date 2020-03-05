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
           return _orderRepository.Add(item);
        }

        public Result<List<Order>> Get()
        {
            return _orderRepository.Get();
        }

        public Result<Order> GetById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public Result<Order> Remove(int id)
        {
            return _orderRepository.Remove(id);
        }

        public Result<Order> Update(Order item)
        {
            return _orderRepository.Update(item);
        }
    }
}