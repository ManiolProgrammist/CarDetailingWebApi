using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models.Repositories.OptionRepositories
{
    public interface IDayInfoRepository:IRepository<DayInfo>
    {
        DayInfo GetByDate(System.DateTime date);
    }
}
