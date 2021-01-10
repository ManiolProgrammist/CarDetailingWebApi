using CarDetailingWebApi.Models;
using CarDetailingWebApi.Models.db;
using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
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
      //[EmployeeAuthentication]
      [Authorize(Roles = "Employee, Admin")]
      // GET: api/User
      [Route("api/User")]
      public Result<List<User>> Get()
      {
         return _userServices.Get();
      }

      //[EmployeeAuthentication]
      [Authorize(Roles = "Employee, Admin")]
      // GET: api/User/5
      public Result<User> Get(int id)
      {
         var identity = (ClaimsIdentity)User.Identity;

         return _userServices.GetById(id);
      }
      //[HttpGet]
      //[Route(("api/LoginUser/{login}/{haslo}"))]
      //public Result<User> LoginUser(string login, string haslo)
      //{

      //    return _userServices.Login(login, haslo);
      //}

      [Authorize(Roles = "Employee, Admin, Normal User, Temporary User")]
      [HttpGet]
      [Route(("api/LogInfo"))]
      public Result<User> GetLogInfo()
      {
         var identity = (ClaimsIdentity)User.Identity;
         return _userServices.GetByLogin(identity.Name);
      }
      [HttpPost]
      [Route("api/User")]

      // POST: api/User
      public Result<User> Post([FromBody] RegisterUserModel value)
      {
         return _userServices.Add(value);
      }

      //[HttpPost]
      //[Route("api/TempUser")]
      //public Result<User> PostTempUs([FromBody]string email)
      //{
      //    return _userServices.CreateTemporaryUser(email);
      //}





      // PUT: api/User/5
      [HttpPut]
      [Authorize(Roles = "Employee, Admin, Normal User, Temporary User")]
      [Route("api/UserEditByEmp/{id}")]
      public Result<User> EditUserByEmp(int id, [FromBody] User value)
      {
         var r = new Result<User>();
         if (!_userServices.CheckIfUserRightsChanged(value))
         {
            var identity = (ClaimsIdentity)User.Identity;
            var role = identity.Claims
                 .Where(c => c.Type == ClaimTypes.Role)
                 .Select(c => c.Value).FirstOrDefault();
            if ( (identity.Name == value.Login )|| (role == "Admin") || (role =="Employee"))
            {
               value.UserId = id;
               r= _userServices.Update(value);
            }
            else
            {
               r.value = value;
               r.status = false;
               r.info = "próba nieautoryzowanej edycji użytkownika";
            }
         }
         else
         {

            r.value = value;
            r.status = false;
            r.info = "Bez praw administratora nie masz prawa do zmieniania uprawnień";

         }
         return r;
      }

      [HttpPut]
      [Route("api/UserEditByAdm/{id}")]
      public Result<User> EditUserByAdm(int id, [FromBody] User value)
      {
         value.UserId = id;
         return _userServices.Update(value);
      }

      // DELETE: api/User/5
      public Result<User> Delete(int id)
      {
         return _userServices.Remove(id);
      }
   }
}
