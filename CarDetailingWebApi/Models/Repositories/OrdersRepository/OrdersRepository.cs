using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Repositories;
using CarDetailingWebApi.Models.Repositories.OrdersTemplateRepositoryF;
using System.Collections.Generic;
using System.Linq;

namespace CarDetailingWebApi.Models
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        IOrdersTemplateRepository OTempRepository;
        IUsersRepository usersRepo;
        public OrdersRepository(IOrdersTemplateRepository ordersTemplate,IUsersRepository usersRepo)
        {
            OTempRepository = ordersTemplate;
            this.usersRepo = usersRepo;
        }
        public new Result<List<Order>> Get()
        {
            var Rlist=base.Get();
            foreach(var l in Rlist.value)
            {
                var pom = OTempRepository.GetById(l.OrderTemplateId);
                //var user = usersRepo.GetById(l.UserId);
                //var userCreate
                if (pom.status)
                {
                    l.OrdersTemplate = pom.value;
                }
                
            }
            
            return Rlist;
        }
        public new Result<Order> GetById(int id)
        {
            var R = base.GetById(id);
            if (R.status)
            {
                var templ = OTempRepository.GetById(R.value.OrderTemplateId);
                var user = usersRepo.GetById(R.value.UserId);
                var userCreate = usersRepo.GetById(R.value.CreateOrderUserId);
                List<OrdersInformation> info;
                using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
                {
                    info = db.OrdersInformations.Where(i => i.OrderId == R.value.OrderId).ToList();
                }
                R.value.OrdersInformations = info;
                if (templ.status)
                {
                    R.value.OrdersTemplate = templ.value;
                }
                if (user.status)
                {
                    R.value.User = user.value;
                }
                //TODO: dodaj userCreate jako referencje.
                //if (userCreate.status)
                //{
                //    R.value.Crea
                //}
            }
            return R;
        }
    }
}