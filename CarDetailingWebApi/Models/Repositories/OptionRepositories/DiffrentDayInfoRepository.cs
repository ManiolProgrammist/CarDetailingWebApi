using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.Repositories.OptionRepositories
{
    public class DiffrentDayInfoRepository : Repository<DiffrentDayInfo>, IDiffrentDayInfoRepository
    {
        IDayInfoRepository _dayInfoRepository;
        //IUsersRepository usersRepo;
        public DiffrentDayInfoRepository(IDayInfoRepository dayInfRep)
        {//,IUsersRepository usersRepo
            _dayInfoRepository = dayInfRep;
            // this.usersRepo = usersRepo;
        }
        public Result<DiffrentDayInfo> getDayDiffInfo(DateTime day)
        {
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<DiffrentDayInfo>();
                r.info = "Zmiana informacji o dniu: "+ day.ToString();
               
                //linku nie porównuje Date dlatego musiałem osobno year,month,day
                r.value =
                    db.DiffrentDayInfoes.Where(e => e.ExactChangeDate.Year == day.Year&&e.ExactChangeDate.Month==day.Month&&e.ExactChangeDate.Day==day.Day).FirstOrDefault();
                if (r.value != null)
                {
                    r.value.DayInfo = _dayInfoRepository.GetById(r.value.DayId).value;
                }

                

                return r;
            }
        }

        public Result<List<DiffrentDayInfo>> getMonthDiffInfo(DateTime month)
        {
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<List<DiffrentDayInfo>>();
                r.info = "Informacja o zmianach terminów w miesiącu: "+month.ToString();
                r.value =
                    db.DiffrentDayInfoes.Where(e => e.ExactChangeDate.Month == month.Month && e.ExactChangeDate.Year == month.Year)
                    .ToList();
                if (r.value.Count > 0)
                {
                    foreach (var day in r.value)
                    {
                        day.DayInfo = _dayInfoRepository.GetById(day.DayId).value;
                    }
                    r.status = true;
                }
                else
                {
                    r.status = false;
                }

                return r;
            }
        }
    }
}