using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models.Repositories.OptionRepositories
{
    public interface IDiffrentDayInfoRepository:IRepository<DiffrentDayInfo>
    {
        Result<List<DiffrentDayInfo>> getMonthDiffInfo(System.DateTime month);
        Result<DiffrentDayInfo> getDayDiffInfo(System.DateTime day);
    }
}
