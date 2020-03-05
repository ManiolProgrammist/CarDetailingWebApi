using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models.Repositories.OrdersTemplateRepositoryF
{
    public interface IOrdersTemplateRepository:IRepository<OrdersTemplate>
    {
        Result<OrdersTemplate> GetByName(string name);
    }
}
