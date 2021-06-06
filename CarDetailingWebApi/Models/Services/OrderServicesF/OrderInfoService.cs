using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Repositories.OrdersRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.Services.OrderServicesF
{
   public class OrderInfoService : IOrderInfoService
   {
      IOrdersInfoRepository _ordersInfoRepository;
      IOrdersRepository _ordersRep;

      public OrderInfoService(IOrdersInfoRepository r,IOrdersRepository orR)
      {
         _ordersInfoRepository = r;
         _ordersRep = orR;
      }
      public Result<OrdersInformation> Add(OrdersInformation item)
      {
         var r =new Result<OrdersInformation>();
         if(_ordersRep.GetById(item.OrderId).status == true)
         {
            r= _ordersInfoRepository.Add(item);
         }
         else
         {
            r.info = "wrong order Id";
            r.status = false;
         }
         return r;
      }

      public Result<List<OrdersInformation>> Get()
      {
         return _ordersInfoRepository.Get();
      }

      public Result<OrdersInformation> GetById(int id)
      {
         return _ordersInfoRepository.GetById(id);
      }

      public Result<List<OrdersInformation>> GetInfoByOrderId(int orderId)
      {
         var r = this.Get();
         if(r.status)
         {
            r.value = r.value.Where(e => e.OrderId == orderId).ToList();
            r.info = "dodatkowe informacje z zamówienia";
         }
         return r;
      }

      public Result<OrdersInformation> Remove(int id)
      {
         return _ordersInfoRepository.Remove(id);
      }

      public Result<OrdersInformation> Update(OrdersInformation item)
      {
         return _ordersInfoRepository.Update(item);
      }
   }
}