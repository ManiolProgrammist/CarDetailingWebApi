using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Repositories.OptionRepositories;

namespace CarDetailingWebApi.Models.Services.OptionS
{
    public class DayInfoService : IDayInfoService
    {
        public IDayInfoRepository _dayInfoRepo;
        public IDiffrentDayInfoRepository _diffR;
        public DayInfoService(IDayInfoRepository r, IDiffrentDayInfoRepository diffR)
        {
            _dayInfoRepo = r;
            _diffR = diffR;
        }
        public Result<DayInfo> Add(DayInfo item)
        {
            if (this.Get().value.Count < 7)
            {
                return _dayInfoRepo.Add(item);
            }
            else
            {
                var r = new Result<DayInfo>();
                r.status = false;
                r.value = null;
                r.info = "can't add more than 7 days to a week";
                return r;
            }
        }
        public Result<DiffrentDayInfo> AddIrregularDay(DayInfo item, System.DateTime dateTime)
        {
            return null;
        }

        public Result<List<DayInfo>> Get()
        {
            return _dayInfoRepo.Get();
        }

        public Result<DayInfo> GetById(int id)
        {
            return _dayInfoRepo.GetById(id);
        }

        public Result<DayInfo> Remove(int id)
        {

            return _dayInfoRepo.Remove(id);
        }

        public Result<DayInfo> Update(DayInfo item)
        {
            return _dayInfoRepo.Update(item);
        }
        //1-12 month
        //TODO:
        public Result<List<DayInfo>> GetMonth(System.DateTime month)
        {
            var result = new Result<List<DayInfo>>();
            result.value = new List<DayInfo>();
            System.DateTime firstDayOfMonth = new System.DateTime(month.Year, month.Month, 1);

            var r1 = _diffR.getMonthDiffInfo(month);
            var normalWeek = _dayInfoRepo.Get();

            for (; firstDayOfMonth.Month == month.Month; firstDayOfMonth=firstDayOfMonth.AddDays(1))
            {
                if (r1.status)
                {
                    var r2 = r1.value.Find(e => e.ExactChangeDate.Date == firstDayOfMonth.Date);

                    if (r2 != null)
                    {
                        result.value.Add(FromDiffrentDayToDayInfo(r2));
                    }
                    else
                    {
                        result.value.Add(_dayInfoRepo.GetByDate(firstDayOfMonth.Date));
                    }
                }
                else
                {
                    result.value.Add(_dayInfoRepo.GetByDate(firstDayOfMonth.Date));
                }
                if (DateTime.Today > firstDayOfMonth.Date)//jeśli data jest przed dniem dzisiejszym: pokazuje jak by lokal byłzamknijęty
                {
                    result.value.Last().IsOpen = false;
                }
            }

            if (result.value.Count > 20)
            {
                result.status = true;
                result.info = "informacje o dacie";
            }
            else
            {
                result.status = false;
                result.info = "brak informacji o dacie w bazie danych";
            }

            return result;
        }

        public Result<DayInfo> getDayInfo(System.DateTime day)
        {

            var res = new Result<DayInfo>();
            var r1 = this._diffR.getDayDiffInfo(day);
            res.status = true;
            if (r1.status)
            {
                res.value = FromDiffrentDayToDayInfo(r1.value);
                res.info = r1.info;
            }
            else
            {
                res.value = this._dayInfoRepo.GetByDate(day);
                res.info = "nie zmieniony dzień";
            }

            return res;
        }



        public Boolean CheckIfDayIsOpen(System.DateTime day)
        {
            DateTime today = DateTime.Today;
            //jeśli data jest wczesniej niż dzień dzisiejszy -> zamknięty lokal
            if (day < today)
            {
                return false;
            }
            var r1 = this._diffR.getDayDiffInfo(day);
            if (r1.status)
            {
                return r1.value.IsOpen;
            }
            var r2 = this._dayInfoRepo.GetByDate(day);
            return r2.IsOpen;

        }



        private DayInfo FromDiffrentDayToDayInfo(DiffrentDayInfo d)
        {
            var r = new DayInfo();
            r.DayId = d.DayId;
            r.Name = d.DayInfo.Name;
            r.IsOpen = d.IsOpen;
            r.OpenHour = d.OpenHour;
            d.CloseHour = d.CloseHour;
            return r;

        }
        public int getHours(string hourMin)
        {

            var h = hourMin.Split(':');

            return Int16.Parse(h[0]);
        }

        public int getMinutes(string hourMin)
        {
            var h = hourMin.Split(':');

            return Int16.Parse(h[1]);
        }
    }
}