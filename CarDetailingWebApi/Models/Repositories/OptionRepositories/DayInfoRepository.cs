using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.Repositories.OptionRepositories
{
    public class DayInfoRepository:Repository<DayInfo>,IDayInfoRepository
    {
        public DayInfo GetByDate(System.DateTime date)
        {
            int day =(int) date.DayOfWeek;
            if (day == 0)
            {
                day = 7;//niedziela;
            }
            return this.GetById(day).value;
        }

    }
}