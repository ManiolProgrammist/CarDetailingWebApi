using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models.Services.OptionS
{
    public interface IDayInfoService:IService<DayInfo>
    {
        Result<List<DayInfo>> GetMonth(System.DateTime month);
        Result<DayInfo> getDayInfo(System.DateTime day);
        int getHours(string hourMin);

        int getMinutes(string hourMin);
    }
}
