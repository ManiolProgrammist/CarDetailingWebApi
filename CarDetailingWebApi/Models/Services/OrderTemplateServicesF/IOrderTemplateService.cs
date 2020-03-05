using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models.Services.OrderTemplateServicesF
{
    public interface IOrdersTemplateService:IService<OrdersTemplate>
    {
         Result<OrdersTemplate> Add(decimal MaxCost, decimal MinCost, string Name, string AdditionalInformation);
         Result<OrdersTemplate> GetByName(string name);
    }
}
