using CarDetailingWebApi.Models;
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Services.UserRightsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDetailingWebApi.Controllers
{
    public class UserTypeController : ApiController
    {
        IUserRightsService serv;
        public UserTypeController(IUserRightsService serv)
        {
            this.serv = serv;
            //var kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly());
            //var UserRepo = kernel.Get<IRepository<User>>("UsersRepo");
        }
        // GET: api/UserType
        public Result<List<UserType>> Get()
        {
            return serv.Get();
        }

        // GET: api/UserType/5
        public Result<UserType> Get(int id)
        {
            return serv.GetById(id);
        }

        //// POST: api/UserType
        //public Result<UserType> Post([FromBody]string value)
        //{
        //}

        //// PUT: api/UserType/5
        //public Result<UserType> Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/UserType/5
        //public Result<UserType> Delete(int id)
        //{
        //}
    }
}
