using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scrypt;
namespace CarDetailingWebApi.Models
{
   public class UsersRepository : Repository<User>, IUsersRepository
   {
      public Result<User> Login(string login, string password)
      {
         using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
         {
            db.Configuration.LazyLoadingEnabled = false;
            var r = new Result<User>();
            r.value = db.Users.FirstOrDefault(u => u.Login.Equals(login));
            ScryptEncoder encoder = new ScryptEncoder();

            if (r.value != null)
            {
               //porównaj czy hash w bazie zgadza się z zwykłym hasłem przesłanym przez użytkownika

               if (encoder.Compare(password, r.value.Password))
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
         var r = new Result<User>();
         if (!UserExist(item.Login))
         {
            item.AccoutCreateDate = DateTime.Now;

            r = base.Add(item);

         }
         else
         {

            r.info = "uzytkownik istnieje";
            r.status = false;
         }

         return r;
      }

      public new Result<List<User>> Get()
      {
         var r = new Result<List<User>>();
         r = base.Get();
         using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
         {
            db.Configuration.LazyLoadingEnabled = false;

            foreach (var u in r.value)
            {

               u.UserType = (UserType)(db.UserTypes.FirstOrDefault(v => v.UserTypeId == u.UserTypeId));
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
      public bool IsFirstUser()
      {
         using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
         {
            return db.Users.Count() < 1;
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