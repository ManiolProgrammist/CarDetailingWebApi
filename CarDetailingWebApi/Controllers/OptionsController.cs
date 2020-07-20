using CarDetailingWebApi.Models;
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Services.OptionS;
using CarDetailingWebApi.Models.Services.OrderTemplateServicesF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDetailingWebApi.Controllers
{
    public class OptionsController : ApiController
    {
        //[EmployeeAuthentication]
        //[Authorize(Roles = "Employee, Admin")]
        // GET: api/User
        IDayInfoService _dayInfo;
        IOrderServices _orderServices;
        IOrdersTemplateService _orderTemplService;
        public OptionsController(IDayInfoService serv, IOrderServices orderServices, IOrdersTemplateService ordersTemplateService)
        {
            _dayInfo = serv;
            _orderServices = orderServices;
            _orderTemplService = ordersTemplateService;
            //var kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly());
            //var UserRepo = kernel.Get<IRepository<User>>("UsersRepo");
        }
        //informacje o tym czy otwarte w danym miesiacu etc
        [Route("api/Options/DaysInfo/{Year}/{Month}")]
        [HttpGet]
        public Result<List<DayInfo>> Get(int Year, int Month)
        {

            var date = new DateTime(Year, Month, 1);
            return _dayInfo.GetMonth(date);
        }

        [Route("api/Options/FreeDaysInfo/{Year}/{Month}/{OrderTemplateId}")]
        [HttpGet]
        public Result<List<DayInfo>> GetInfoFreeTimeInMonthOrderTemplate(int Year, int Month, int OrderTemplateId)
        {
            var date = new DateTime(Year, Month, 1);
            var list = _dayInfo.GetMonth(date);
            var ordertempl = _orderTemplService.GetById(OrderTemplateId);
            if (list.status)
            {
                if (ordertempl.status)
                {
                    return _orderServices.CheckMonthIfFree(list.value, date, ordertempl.value.ExpectedTime.Value);
                }
                else
                {
                    list.info = "nieprawidłowy Order Template ID";
                }
            }
            //return with error because status=false;
            return list;

        }
        [Route("api/Options/FreeDaysInfo/{Year}/{Month}/{day}/{OrderTemplateId}")]
        [HttpGet]
        public Result<List<System.DateTime[]>> GetInfoFreeTimeInDayForOrdT(int Year, int Month,int day, int OrderTemplateId)
        {
            var date = new DateTime(Year, Month, day);
            var ordertempl = _orderTemplService.GetById(OrderTemplateId);
            var res = new Result<List<System.DateTime[]>>();
            if (ordertempl.status)
            {
              
               res= _orderServices.CheckFreeHours(date, ordertempl.value.ExpectedTime.Value);
            }
            else
            {
                res.status = false;
                res.info = "nieprawidlowe orderTemplateId";
            }
            return res;
        }

        //[Route("api/Options/DayInfo/{Year}/{Month}/{Day}")]
        //[HttpGet]
        //public Result<List<>>

        //// GET: api/Options/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Options
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Options/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Options/5
        public void Delete(int id)
        {
        }
    }
}
