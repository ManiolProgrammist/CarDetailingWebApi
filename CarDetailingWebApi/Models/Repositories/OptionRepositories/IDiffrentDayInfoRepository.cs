using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models.Repositories.OptionRepositories
{
    public interface IDifDayInfoRepository:IRepository<DifDayInfo>
    {
        Result<List<DifDayInfo>> getMonthDiffInfo(System.DateTime month);
        Result<DifDayInfo> getDayDiffInfo(System.DateTime day);
    }
}
