using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models
{
    public interface IOrdersRepository : IRepository<Order>
    {
        Result<List<Order>> Get(bool Done);
        Result<List<Order>> GetStarted(bool started);
        Result<List<Order>> GetByUserId(int id);
    }
}
