using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        public Result<T> Add(T item) //    item.AccoutCreateDate = DateTime.Now; dla usera
        {
            var r = new Result<T>();
            try
            {
                using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
                {
                    db.Configuration.LazyLoadingEnabled = false;

                    r.info = "Poprawnie dodane";
                    r.status = true;
                    r.value = item;
                    db.Entry(item).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();
                    return r;
                }
            }catch(Exception e)
            {
                r.status = false;
                r.value = null;
                r.info = "Nie udało się dodać, error:"+e.ToString();
                return r;
            }
        }

        public Result<List<T>> Get()
        {
            using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<List<T>>();
                r.info = "get all";
                r.status = true;
                r.value = db.Set<T>().ToList();
                return r;
            }
        }

        public Result<T> GetById(int id)
        {
            using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<T>();
                r.value = db.Set<T>().Find(id);
                if (r.value != null)
                {

                    r.info = "znaleziono encję o tym id";
                    r.status = true;
                }
                else
                {
                    r.info = "nie znaleziono encji o id:" + id;
                    r.status = false;
                }
                return r;

            }
        }



        public Result<T> Remove(int id)
        {

            try
            {
                T i;
                using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
                {
                    T p = db.Set<T>().Find(id);
                    db.Configuration.LazyLoadingEnabled = false;
                    i = db.Set<T>().Remove(p);

                    db.SaveChanges();
                }
                Result<T> r = new Result<T>();
                r.value = i;
                r.info = "usunięto";
                r.status = true;
                return r;
            }
            catch (Exception e)
            {

                Result<T> r = new Result<T>();
                r.value = null;
                r.info = "error " + e.Message;
                r.status = false;
                return r;
            }
        }

        public Result<T> Update(T item)
        {
            //var i = new T();
            Result<T> r = new Result<T>();
            try
            {

               
                using (CarCosmeticSalonEntities2 db = new CarCosmeticSalonEntities2())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                r.value = item;
                r.info = "poprawnie dodane";
                r.status = true;
                return r;
            }
            catch(Exception e)
            {
                r.value = null;
                r.info = "Error:" + e.Message;
                r.status = false;
                return r;
            }
    }
    }
}