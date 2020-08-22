using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.Repositories.OrderTemplateImageRepository
{
    public interface IOrderTemplateImageRepository : IRepository<OrdersTemplateImage>
    {
        Result<List<OrdersTemplateImage>> GetImagesFromOrderTemplateId(int orderTemplateId);
    }
}