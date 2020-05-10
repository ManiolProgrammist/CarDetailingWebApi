using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models
{
    public interface IOrderServices:IService<Order>
    {
        Result<List<Order>> Get(bool done);
        Result<List<Order>> GetStarted(bool started);
        Result<Order> StartOrder(int orderId, bool start);
        Result<Order> EndOrder(int orderId, bool end);
    }
}
