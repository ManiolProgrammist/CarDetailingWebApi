﻿using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Repositories;
using CarDetailingWebApi.Models.Repositories.OrdersRepository;
using CarDetailingWebApi.Models.Repositories.OrdersTemplateRepositoryF;
using System.Collections.Generic;
using System.Linq;

namespace CarDetailingWebApi.Models
{
   public class OrdersRepository : Repository<Order>, IOrdersRepository
   {
      IOrdersTemplateRepository OTempRepository;
      IOrdersInfoRepository OrdersInfoRepository;
      //IUsersRepository usersRepo;
      public OrdersRepository(IOrdersTemplateRepository ordersTemplate, IOrdersInfoRepository orInfR)
      {//,IUsersRepository usersRepo
         OTempRepository = ordersTemplate;
         OrdersInfoRepository = orInfR;
         // this.usersRepo = usersRepo;
      }
      public new Result<List<Order>> Get()
      {
         var r = base.Get();
         r.value = AddTemplateOrderToList(r.value);

         return r;
      }
      public Result<List<Order>> GetByTemplateId(int id)
      {
         using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
         {
            db.Configuration.LazyLoadingEnabled = false;
            var r = new Result<List<Order>>();
            r.info = "get by user id " + id;
            r.status = true;

            r.value = db.Orders.Where(e => e.OrderTemplateId == id).ToList();
            r.value = AddTemplateOrderToList(r.value);
            return r;
         }
      }
      public Result<List<Order>> GetByUserId(int id)
      {
         using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
         {
            db.Configuration.LazyLoadingEnabled = false;
            var r = new Result<List<Order>>();
            r.info = "get by user id " + id;
            r.status = true;

            r.value = db.Orders.Where(e => e.UserId == id).ToList();
            r.value = AddTemplateOrderToList(r.value);
            return r;
         }
      }
      public new Result<Order> GetById(int id)
      {
         var R = base.GetById(id);
         if (R.status)
         {
            var templ = OTempRepository.GetById(R.value.OrderTemplateId);

            List<OrdersInformation> info;
            using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
            {
               info = db.OrdersInformations.Where(i => i.OrderId == R.value.OrderId).ToList();
            }
            R.value.OrdersInformations = info;
            if (templ.status)
            {
               R.value.OrdersTemplate = templ.value;
            }

         }
         return R;
      }

      public Result<List<Order>> Get(bool Done)
      {
         using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
         {
            db.Configuration.LazyLoadingEnabled = false;
            var r = new Result<List<Order>>();
            r.info = "get Done " + Done;
            r.status = true;
            r.value = db.Orders.Where(e => e.IsOrderCompleted == Done).ToList();
            r.value = AddTemplateOrderToList(r.value);
            return r;
         }
      }

      public Result<List<Order>> GetStarted(bool started)
      {
         using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
         {
            db.Configuration.LazyLoadingEnabled = false;
            var r = new Result<List<Order>>();
            r.info = "get Started " + started;
            r.status = true;
            r.value = db.Orders.Where(e => e.IsOrderStarted == started).ToList();
            r.value = AddTemplateOrderToList(r.value);
            return r;
         }
      }

      public Result<List<Order>> GetOrdersFromDay(System.DateTime date)
      {
         using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
         {
            db.Configuration.LazyLoadingEnabled = false;
            var r = new Result<List<Order>>();
            r.info = "get orders from Day " + date.Day + "/" + date.Month + "/" + date.Year + "/";
            r.status = true;
            r.value = db.Orders.Where(e => e.ExpectedStartOfOrder.Value.Day == date.Day && e.ExpectedStartOfOrder.Value.Month == date.Month && e.ExpectedStartOfOrder.Value.Year == date.Year).ToList();
            r.value = AddTemplateOrderToList(r.value);
            return r;
         }
      }
      public Result<List<Order>> GetOrdersFromMonth(System.DateTime date)
      {
         using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
         {
            db.Configuration.LazyLoadingEnabled = false;
            var r = new Result<List<Order>>();
            r.info = "get orders from month " + date.Month + "/" + date.Year + "/";
            r.status = true;
            r.value = db.Orders.Where(e => e.ExpectedStartOfOrder.Value.Date.Year == date.Date.Year && e.ExpectedStartOfOrder.Value.Date.Month == date.Date.Month).ToList();
            r.value = AddTemplateOrderToList(r.value);
            return r;
         }
      }
      public Result<Order> GetByPayUOrderId(string payuIdOrd)
      {
         var r = new Result<Order>();
         using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
         {
            db.Configuration.LazyLoadingEnabled = false;

            r.info = "get by payu id:" + payuIdOrd;

            r.value = db.Orders.Where(e => e.PayUOrderId == payuIdOrd).First();
            if (r.value != null)
            {
               r.status = true;
            }
            else
            {
               r.status = false;
            }
         }
         return r;
      }

      private List<Order> AddTemplateOrderToList(List<Order> ord)
      {
         foreach (var l in ord)
         {
            var pom = OTempRepository.GetById(l.OrderTemplateId);
            //var user = usersRepo.GetById(l.UserId);
            //var userCreate
            if (pom.status)
            {
               l.OrdersTemplate = pom.value;
            }

         }
         return ord;
      }
   }
}