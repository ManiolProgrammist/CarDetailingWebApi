using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models.Services.OrderServicesF
{
   public interface IOrderInfoService:IService<OrdersInformation>
   {
      Result<List<OrdersInformation>> GetInfoByOrderId(int orderId);
   }
}
