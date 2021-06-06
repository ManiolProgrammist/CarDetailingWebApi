using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Services.OptionS;
using CarDetailingWebApi.Models.Services.OrderServicesF;
using CarDetailingWebApi.Models.Services.OrderTemplateServicesF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models
{
   public class OrderServices : IOrderServices
   {
      IOrdersRepository _orderRepository;
      IUserService _userService;
      IDayInfoService _dayInfoService;
      IOrdersTemplateService orderTemplateService;
      IOrderInfoService _orderInfoService;
      public OrderServices(IOrdersRepository r, IUserService userService, IDayInfoService dayinfo, IOrdersTemplateService oTPs, IOrderInfoService oIS)
      {
         _userService = userService;
         _orderRepository = r;
         _dayInfoService = dayinfo;
         orderTemplateService = oTPs;
         _orderInfoService = oIS;
      }
      public Result<Order> Add(Order item)
      {
         //TODO: Czy w tych godzinach nie ma problemu z zrobieniem

         item.OrderDate = System.DateTime.Now;
         item.ExpectedStartOfOrder = item.ExpectedStartOfOrder.Value.ToLocalTime();
         item.ExpectedStartOfOrder = new DateTime(item.ExpectedStartOfOrder.Value.Ticks);
         item.User = null;
         item.OrdersTemplate = null;

         var r = _orderRepository.Add(item);
         if(r.status)
         {
            r.value.User = _userService.GetById(r.value.UserId).value;
            r.value.OrdersTemplate = orderTemplateService.GetById(r.value.OrderTemplateId).value;
         }
         return r;

      }


      public Result<List<Order>> Get()
      {
         var r = _orderRepository.Get();

         foreach(var rv in r.value)
         {
            rv.User = _userService.GetById(rv.UserId).value;
         }
         AddOrderInfoTo(r.value);


         return r;
      }
      public Result<List<Order>> GetByUserId(int id)
      {
         var r = _orderRepository.GetByUserId(id);
         if(r.status)
         {
            foreach(var rv in r.value)
            {
               rv.User = _userService.GetById(rv.UserId).value;
            }
            AddOrderInfoTo(r.value);
         }
         return r;

      }

      public Result<List<Order>> Get(bool done)
      {
         var r = _orderRepository.Get(done);
         foreach(var rv in r.value)
         {
            rv.User = _userService.GetById(rv.UserId).value;
         }
         AddOrderInfoTo(r.value);

         return r;
      }

      public Result<Order> GetById(int id)
      {
         var r = _orderRepository.GetById(id);
         if(r.status)
         {
            r.value.User = _userService.GetById(r.value.UserId).value;
            AddOrderInfoTo(r.value);
         }
         return r;
      }

      public Result<List<Order>> GetStarted(bool started)
      {

         var r = _orderRepository.GetStarted(started);
         foreach(var rv in r.value)
         {
            rv.User = _userService.GetById(rv.UserId).value;

         }

         AddOrderInfoTo(r.value);
         return r;
      }

      public Result<Order> Remove(int id)
      {
         var r = _orderRepository.Remove(id);
         if(r.status)
         {
            r.value.User = _userService.GetById(r.value.UserId).value;

         }
         return r;

      }

      public Result<Order> StartOrder(int orderId, bool start) //bool w razie pomyłki pracownika- może to cofnąć
      {
         var r = _orderRepository.GetById(orderId);
         if(r.status)
         {
            if(start)
            {
               r.value.StartOfOrder = System.DateTime.Now;
            }
            else
            {
               r.value.StartOfOrder = null;
            }
            r.value.IsOrderStarted = start;

            return this.Update(r.value);
         }
         else
         {
            return r;
         }

      }
      public Result<Order> EndOrder(int orderId, bool end)
      {
         var r = _orderRepository.GetById(orderId);
         if(r.status)
         {
            if(end)
            {
               r.value.CompletedOrderDate = System.DateTime.Now;
            }
            else
            {
               r.value.CompletedOrderDate = null;
            }
            r.value.IsOrderCompleted = end;

            return this.Update(r.value);
         }
         else
         {
            return r;
         }
      }
      public Result<Order> Update(Order item)
      {
         //item.OrdersInformations.Clear();
         //clear information
         item.OrdersInformations = null;
         item.OrdersTemplate = null;
         item.User = null;
         
         
         var r = _orderRepository.Update(item);

         if(r.status)
         {
            r.value.User = _userService.GetById(r.value.UserId).value;
            AddOrderInfoTo(r.value);
         }
         return r;
      }

      public Result<Order> PaidOrder(int orderId, bool paid)
      {
         var r = _orderRepository.GetById(orderId);
         r.value.IsPaid = paid;
         return this.Update(r.value);
      }
      //every delay is +15 min
      public Result<Order> AddDelay(int orderId, int delayAmount)
      {
         var r = _orderRepository.GetById(orderId);
         if(r.status)
         {
            if(r.value.delays != null)
            {
               r.value.delays += delayAmount;
            }
            else
            {
               r.value.delays = delayAmount;
            }
            r = _orderRepository.Update(r.value);
            if(r.status)
            {
               r.info = "poprawnie dodane opóźnienie";
            }
         }
         if(r.status)
         {
            var infoR = _orderInfoService.GetInfoByOrderId(r.value.OrderId);
            if(infoR.status)
            {
               r.value.OrdersInformations = infoR.value;
            }
         }
         return r;
      }
      public Result<List<DayInfo>> CheckMonthIfFree(List<DayInfo> dayInfos, System.DateTime month, System.TimeSpan NeededFreeTime)
      {
         var R = new Result<List<DayInfo>>();
         R.value = dayInfos;
         var nextMonth = month.AddMonths(1);
         int i = 0;
         for(System.DateTime DayInMonth = new DateTime(month.Year, month.Month, month.Day); DayInMonth.Month < nextMonth.Month; DayInMonth = DayInMonth.AddDays(1))
         {

            //jeśli jest otwarty, sprawdź czy starczy czasu na zlecenie
            if(R.value[i].IsOpen)
            {
               R.value[i].IsOpen = CheckIfDayIsFree(DayInMonth, R.value[i], NeededFreeTime).value;
            }
            i = i + 1;
         }
         R.status = true;
         R.info = "Sprawdzono czy w miesiącu są wolne terminy";
         return R;
      }

      //zwraca listę wolnych godzin
      public Result<List<System.DateTime[]>> CheckFreeHours(System.DateTime day, System.TimeSpan NeededFreeTim)
      {
         var result = new Result<List<System.DateTime[]>>();
         result.value = new List<System.DateTime[]>();
         //result.value.Add(new System.DateTime[2]);

         var dayFree = _dayInfoService.getDayInfo(day);
         var FR = this._orderRepository.GetOrdersFromDay(day);
         FR.value = SortOrderByStartDate(FR.value);
         if(dayFree.value.IsOpen)
         {
            if(CheckIfDayIsFree(day, dayFree.value, NeededFreeTim).value)
            {
               result.status = true;
               result.info = "w tym dniu jest wolny czas na to zlecenie";
               var OpenTimeSpan = new TimeSpan(_dayInfoService.getHours(dayFree.value.OpenHour), _dayInfoService.getMinutes(dayFree.value.OpenHour), 0);
               var CloseTimeSpan = new TimeSpan(_dayInfoService.getHours(dayFree.value.CloseHour), _dayInfoService.getHours(dayFree.value.OpenHour), 0);

               var startD = new System.DateTime(day.Year, day.Month, day.Day,
                   this._dayInfoService.getHours(dayFree.value.OpenHour), this._dayInfoService.getMinutes(dayFree.value.OpenHour), 0);
               var endD = new System.DateTime(day.Year, day.Month, day.Day,
                   this._dayInfoService.getHours(dayFree.value.CloseHour), this._dayInfoService.getMinutes(dayFree.value.CloseHour), 0);

               if(FR.value.Count > 0)
               {

                  //dodanie jeśli jest czas przed pierwszym zleceniem
                  if((FR.value[0].ExpectedStartOfOrder.Value.TimeOfDay - OpenTimeSpan) >= NeededFreeTim)
                  {
                     result.value.Add(new System.DateTime[2] { startD, FR.value[0].ExpectedStartOfOrder.Value });
                  }




                  if(FR.value.Count > 1)
                  {



                     //todo, check where he have time for that
                     for(int i = 0; i < FR.value.Count - 1; i++)
                     {
                        //jeśli jakakolwiek godzina będzie pasować to przechodzi poprawnie

                        if(FR.value[i + 1].ExpectedStartOfOrder.Value - (FR.value[i].ExpectedStartOfOrder.Value + FR.value[i].OrdersTemplate.ExpectedTime.Value) >= NeededFreeTim)
                        {
                           var dateTFirst = new System.DateTime[2] { FR.value[i].ExpectedStartOfOrder.Value + FR.value[i].OrdersTemplate.ExpectedTime.Value, FR.value[i + 1].ExpectedStartOfOrder.Value };
                           result.value.Add(dateTFirst);
                        }
                     }
                  }
                  //dodanie jeśli jest czas po ostatnim zleceniu
                  if((CloseTimeSpan - (FR.value[FR.value.Count - 1].ExpectedStartOfOrder.Value.TimeOfDay + FR.value[FR.value.Count - 1].OrdersTemplate.ExpectedTime.Value)) >= NeededFreeTim)
                  {
                     result.value.Add(new System.DateTime[2] { FR.value[FR.value.Count - 1].ExpectedStartOfOrder.Value + FR.value[FR.value.Count - 1].OrdersTemplate.ExpectedTime.Value, endD });
                  }

               }
               else
               {


                  result.value.Add(new System.DateTime[2] { startD, endD });
               }
               if(result.value.Count > 0)
               {
                  return result;
               }
            }

         }
         result.status = false;
         result.info = "w tym dniu zamknięte lub brak czasu";
         return result;

      }

      public Result<Boolean> CheckIfDayIsFree(System.DateTime Day, DayInfo dayInfo, System.TimeSpan NeededFreeTime)
      {
         var FirstCheck = dayInfo;


         var R = new Result<Boolean>();

         if(FirstCheck.IsOpen)
         {


            var FR = _orderRepository.GetOrdersFromDay(Day);
            FR.value = SortOrderByStartDate(FR.value);
            //zakladam ze jest czas
            R.value = true;
            R.status = true;
            //przypadek w którym nie ma żadnego zlecenia w danym dniu
            if(FR.value.Count < 1)
            {

               R.info = "Brak zleceń w danym dniu, jest czas na zlecenie";
               return R;
            }

            //przypadek w którym jest czas przed lub po aktualnych zleceinach
            var OpenTimeSpan = new TimeSpan(_dayInfoService.getHours(FirstCheck.OpenHour), _dayInfoService.getMinutes(FirstCheck.OpenHour), 0);
            var CloseTimeSpan = new TimeSpan(_dayInfoService.getHours(FirstCheck.CloseHour), _dayInfoService.getHours(FirstCheck.CloseHour), 0);

            R.info = "jest czas na zlecenie";

            if((FR.value[0].ExpectedStartOfOrder.Value.TimeOfDay - OpenTimeSpan) >= NeededFreeTime || (CloseTimeSpan - (FR.value[FR.value.Count - 1].ExpectedStartOfOrder.Value.TimeOfDay + FR.value[FR.value.Count - 1].OrdersTemplate.ExpectedTime.Value)) >= NeededFreeTime)
            {
               return R;
            }


            for(int i = 0; i < FR.value.Count - 1; i++)
            {
               //jeśli jakakolwiek godzina będzie pasować to przechodzi poprawnie
               //
               if(FR.value[i + 1].ExpectedStartOfOrder - (FR.value[i].ExpectedStartOfOrder + FR.value[i].OrdersTemplate.ExpectedTime) >= NeededFreeTime)
               {
                  return R;
               }
            }
            R.info = "brak czasu na zlecenie";
            R.status = false;
            R.value = false;



         }
         R.info = "W tym dniu zamknięte";
         R.status = false;
         R.value = false;


         return R;
      }
      //Sortuje przesłaną listę przez datę
      //up =  true-> sortuj od "najstarszej" do "najmłodszej" daty
      //up =false -> sortuj od "najmłodszedj" do "najstarszej
      public List<Order> SortOrderByStartDate(List<Order> orderList, Boolean up = true)
      {
         var r = new List<Order>();
         if(orderList.Count > 0)
         {
            if(up)
            {
               r = (List<Order>)orderList.OrderBy(p => p.StartOfOrder).ToList();
            }
            else
            {
               r = (List<Order>)orderList.OrderByDescending(p => p.StartOfOrder).ToList();
            }
         }

         return r;
      }

      public Result<Order> GetByPayUOrderId(string payuIdOrd)
      {
         var r = _orderRepository.GetByPayUOrderId(payuIdOrd);
         //if (r.status)
         //{
         //   r.value.User = _userService.GetById(r.value.UserId).value;
         //}
         if(r.status)
         {
            var infoR = _orderInfoService.GetInfoByOrderId(r.value.OrderId);
            if(infoR.status)
            {
               r.value.OrdersInformations = infoR.value;
            }
         }
         return r;
      }
      private List<Order> AddOrderInfoTo(List<Order> orders)
      {
         foreach(var or in orders)
         {
            var infoR = _orderInfoService.GetInfoByOrderId(or.OrderId);
            if(infoR.status)
            {
               or.OrdersInformations = new List<OrdersInformation>();
               foreach(var i in infoR.value)
               {
                  or.OrdersInformations.Add(i);
               }
            }

         }
         return orders;
      }
      private Order AddOrderInfoTo(Order orders)
      {
         if(orders != null)
         {
            var infoR = _orderInfoService.GetInfoByOrderId(orders.OrderId);
            if(infoR.status)
            {
               orders.OrdersInformations = infoR.value;
            }
         }
         return orders;
      }
   }
}