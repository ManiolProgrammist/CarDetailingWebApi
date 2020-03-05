using CarDetailingWebApi.Models;
using CarDetailingWebApi.Models.Authentication;
using CarDetailingWebApi.Models.AuthenticationAtributes;
using CarDetailingWebApi.Models.db;

using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace CarDetailingWebApi.Controllers
{
    public class UserController : ApiController
    {
        IUserService _userServices;
        public UserController(IUserService serv)
        {
            _userServices = serv;
            //var kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly());
            //var UserRepo = kernel.Get<IRepository<User>>("UsersRepo");
        }
        [EmployeeAuthentication]
        // GET: api/User
        public Result<List<User>> Get()
        {
            return _userServices.Get();
        }

        [EmployeeAuthentication]
        // GET: api/User/5
        public Result<User> Get(int id)
        {
            return _userServices.GetById(id);
        }
        [HttpGet]
        [Route(("api/LoginUser/{login}/{haslo}"))]
        public Result<User> LoginUser(string login,string haslo)
        {
            
            return _userServices.Login(login, haslo);
        }

        // POST: api/User
        public Result<User> Post([FromBody]RegisterUserModel value)
        {
            return _userServices.Add(value);
        }

        [EmployeeAuthentication]
        // PUT: api/User/5
        [HttpPut]
        [Route("api/UserEditByEmp/{id}")]
        public Result<User> EditUserByEmp(int id, [FromBody]User value)
        {
            if (_userServices.CheckIfUserRightsChanged(value))
            {
                value.UserId = id;
                return _userServices.Update(value);
            }
            else
            {
                var r = new Result<User>();
                r.value = value;
                r.status = false;
                r.info = "Bez praw administratora nie masz prawa do zmieniania uprawnień";
                return r;
            }
        }

        [AdminAuthentication]
        [HttpPut]
        [Route("api/UserEditByAdm/{id}")]
        public Result<User> EditUserByAdm(int id, [FromBody]User value)
        {
            value.UserId = id;
            return _userServices.Update(value);
        }
        [EmployeeAuthentication]
        // DELETE: api/User/5
        public Result<User> Delete(int id)
        {
            return _userServices.Remove(id);
        }
    }
}
