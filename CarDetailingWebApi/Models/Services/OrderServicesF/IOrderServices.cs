using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models
{
   public interface IOrderServices : IService<Order>
   {
      Result<List<Order>> Get(bool done);
      Result<List<Order>> GetStarted(bool started);
      Result<Order> StartOrder(int orderId, bool start);
      Result<Order> EndOrder(int orderId, bool end);
      Result<Order> PaidOrder(int orderId, bool paid);
      Result<List<Order>> GetByUserId(int id);
      Result<List<DayInfo>> CheckMonthIfFree(List<DayInfo> dayInfos, System.DateTime month, System.TimeSpan NeededFreeTime);
      Result<List<System.DateTime[]>> CheckFreeHours(System.DateTime day, System.TimeSpan NeededFreeTim);
      Result<Order> GetByPayUOrderId(string payuIdOrd);
      Result<Order> AddDelay(int orderId, int delayAmount);
   }
}
