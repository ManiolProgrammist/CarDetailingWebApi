using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Repositories.OrdersTemplateRepositoryF;

namespace CarDetailingWebApi.Models.Services.OrderTemplateServicesF
{
    public class OrdersTemplateService : IOrdersTemplateService
    {
        IOrdersTemplateRepository _ordersTRepository;
        IUserService _userService;
        public OrdersTemplateService(IOrdersTemplateRepository otr, IUserService iuS)
        {
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
            return _ordersTRepository.Get();
        }

        public Result<OrdersTemplate> GetById(int id)
        {
            return _ordersTRepository.GetById(id);
        }
        public Result<OrdersTemplate> GetByName(string name)
        {
            throw new NotImplementedException();
        }


        public Result<OrdersTemplate> Remove(int id)
        {
            
            if (GetById(id).status)
            {
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

        public Result<OrdersTemplate> Update(OrdersTemplate item)
        {
           return _ordersTRepository.Update(item);
        }
    }
}