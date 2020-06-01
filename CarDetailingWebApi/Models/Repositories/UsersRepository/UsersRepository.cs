using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models
{
    public class UsersRepository :Repository<User>, IUsersRepository
    {
        public Result<User> Login(string login, string password)
        {
            using(CarCosmeticSalonEntities db= new CarCosmeticSalonEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<User>();
                r.value = db.Users.FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
                if(r.value!=null)
                {
                    r.value.UserType = db.UserTypes.FirstOrDefault(u => u.UserTypeId == r.value.UserTypeId);
                    r.info = "zalogowany:";
                    r.status = true;
                }
                else
                {
                    r.info = "Nie znaleziono użytkownika o podanym loginie i haśle";
                    r.status = false;
                }
                
                return r;
            }
        }
        public new Result<User> Add(User item)
        {
            item.AccoutCreateDate = DateTime.Now;
            var r= base.Add(item);
    
            return r;
        }

        public new Result<List<User>> Get()
        {
            var r = new Result<List<User>>();
            r = base.Get();
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
               
                foreach( var u in r.value)
                {

                    u.UserType = (UserType)( db.UserTypes.FirstOrDefault(v => v.UserTypeId == u.UserTypeId));
                    u.UserType.Users = null;
           
                }
                return r;
            }
        }

        public new Result<User> GetById(int id)
        {
            var r = base.GetById(id);
            
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                r.value.UserType = db.UserTypes.FirstOrDefault(u => u.UserTypeId == r.value.UserTypeId);
            }
            return r;
       
        }
        public bool UserExist(string login)
        {
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                return db.Users.Any(u => u.Login == login);
            }
        }

        public Result<User> GetByLogin(string login)
        {
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<User>();
                r.value = db.Users.FirstOrDefault(u => u.Login == login);
                if (r.value != null)
                {
                    r.info = "znaleziono użytkownika";
                    r.status = true;
                    r.value.UserType = db.UserTypes.FirstOrDefault(u => u.UserTypeId == r.value.UserTypeId);
                }
                else
                {
                    r.info = "nie znaleziono użytkownika";
                    r.status = false;
                }
                return r;
            }
        }

        public Result<int> GetIdByLogin(string login)
        {
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                var r = new Result<int>();
                var i = db.Users.FirstOrDefault(u => u.Login == login).UserId;
                if (i == 0)
                {
                    r.info = "brak użytkownika o takim loginie";
                    r.status = false;
                }
                else
                {
                    r.info = "znaleziono id użytkownika";
                    r.status = true;
                    r.value = i;
                }

                return r;
            }
        }

    }
}