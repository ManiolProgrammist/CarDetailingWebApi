using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDetailingWebApi.Models.db;

namespace CarDetailingWebApi.Models.Repositories.OrderTemplateImageRepository
{
    public class OrderTemplateImageRepository : Repository<OrdersTemplateImage>, IOrderTemplateImageRepository
    {
        public Result<List<OrdersTemplateImage>> GetImagesFromOrderTemplateId(int orderTemplateId)
        {
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<List<OrdersTemplateImage>>();
        
                r.value = db.OrdersTemplateImages.Where(e=>e.OrderTemplateId==orderTemplateId).ToList();
                if (r.value != null)
                {

                    r.info = "znaleziono zdjęcia dołączone do tego Template: "+orderTemplateId;
                    r.status = true;
                }
                else
                {
                    r.info = "nie znaleziono zdjęć dołączonych do tego Template: " + orderTemplateId;
                    r.status = false;
                }
                return r;

            }
        }
    }
}