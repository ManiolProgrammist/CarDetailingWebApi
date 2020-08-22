using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Repositories.OrdersTemplateRepositoryF;
using CarDetailingWebApi.Models.Repositories.OrderTemplateImageRepository;

namespace CarDetailingWebApi.Models.Services.OrderTemplateServicesF
{
   public class OrdersTemplateService : IOrdersTemplateService
   {
      IOrdersTemplateRepository _ordersTRepository;
      IOrderTemplateImageRepository _orderTImageRepository;
      IOrdersRepository _ordersRepository;
      IUserService _userService;
      public OrdersTemplateService(IOrdersTemplateRepository otr, IUserService iuS, IOrderTemplateImageRepository otir, IOrdersRepository _ordersR)
      {
         _ordersRepository = _ordersR;
         _orderTImageRepository = otir;
         _ordersTRepository = otr;
         _userService = iuS;
      }
      public Result<OrdersTemplate> Add(OrdersTemplate item)
      {

         var r = _ordersTRepository.GetByName(item.Name);
         //sprawdź czy nie ma tego samego NAME co jakiś inny element
         if (!r.status)
         {
            if (item.MaxCost < item.MinCost)
            {
               r.status = false;
               r.info = "Error: maxymalny koszt mniejszy od minimalnego";
               return r;
            }
            item.OrdersTemplateSets = null;
            return _ordersTRepository.Add(item);
         }
         else
         {
            r.status = false;
            r.info = "Error: nie można dodać order template - istnieje już order template o nazwie:" + item.Name;
            return r;
         }
      }
      public Result<OrdersTemplate> Add(decimal MaxCost, decimal MinCost, string Name, string AdditionalInformation)
      {

         var item = new OrdersTemplate() { MaxCost = MaxCost, MinCost = MinCost, Name = Name, AdditionalInformation = AdditionalInformation };


         return Add(item);
      }


      public Result<List<OrdersTemplate>> Get()
      {
         var r = _ordersTRepository.Get();

         foreach (var orT in r.value)
         {
            var im = _orderTImageRepository.GetImagesFromOrderTemplateId(orT.OrderTemplateId);
            if (im.status)
            {
               orT.OrdersTemplateImages = im.value;
            }
         }
         return r;
      }

      public Result<OrdersTemplate> GetById(int id)
      {
         var r = _ordersTRepository.GetById(id);
         if (r.status)
         {
            var im = _orderTImageRepository.GetImagesFromOrderTemplateId(id);
            if (im.status)
            {
               r.value.OrdersTemplateImages = im.value;
            }

         }
         return r;
      }
      public Result<OrdersTemplate> GetByName(string name)
      {
         throw new NotImplementedException();
      }

      public Result<OrdersTemplate> Remove(int id,bool usunOrder)
      {
         if (usunOrder)
         {
            var OrderList = _ordersRepository.GetByTemplateId(id);
            foreach (var order in OrderList.value)
            {
               _ordersRepository.Remove(order.OrderId);
            }

         }
         return this.Remove(id);
      }

      public Result<OrdersTemplate> Remove(int id)
      {
         //TODO: zadecyduj co zrobić z zleceniami na tą usługę
  
         var pom = GetById(id);
         if (pom.status)
         {
            foreach (var im in pom.value.OrdersTemplateImages)
            {
               _orderTImageRepository.Remove(im.ImageId);
            }
            return _ordersTRepository.Remove(id);
         }
         else
         {
            var r = new Result<OrdersTemplate>();
            r.status = false;
            r.info = "nie znaleziono order template o id" + id;
            return r;
         }
      }

      public Result<OrdersTemplateImage> RemoveImage(int orderTemplateId, int imageId)
      {
         throw new NotImplementedException();
      }
      public Result<OrdersTemplateImage> GetImagesFromOrderTemplate(int orderTemplateId)
      {
         throw new NotImplementedException();
      }
      public Result<OrdersTemplateImage> AddImage(OrdersTemplateImage orderTemplateImage)
      {
         var r = new Result<OrdersTemplateImage>();
         if (orderTemplateImage.OrderTemplateId != 0)
         {
            var ordT = this._ordersTRepository.GetById(orderTemplateImage.OrderTemplateId);
            if (ordT.status)
            {
               r = this._orderTImageRepository.Add(orderTemplateImage);
               return r;
            }
         }

         r.status = false;
         r.info = "nieprawidłowe order Template Id";


         return r;
      }
      public Result<OrdersTemplate> Update(OrdersTemplate item)
      {
         return _ordersTRepository.Update(item);
      }
   }
}