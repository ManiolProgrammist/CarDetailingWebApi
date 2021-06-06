using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDetailingWebApi.Models.db;

namespace CarDetailingWebApi.Models.Repositories.OrdersTemplateRepositoryF
{
    public class OrdersTemplateRepository: Repository<OrdersTemplate>, IOrdersTemplateRepository
    {

        public Result<OrdersTemplate> GetByName(string name)
        {
            using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<OrdersTemplate>();
                r.info = "test mess";
                
                r.value = db.OrdersTemplates.FirstOrDefault(p => p.Name == name);
                if (r.value != null)
                {
                    r.status = true;
                    r.info = "Istnieje order template o tej nazwie";
                }
                else
                {
                    r.status = false;
                    r.info = "order template o tej nazwie nie istnieje";
                }
                return r;
            }
        }
    }
}