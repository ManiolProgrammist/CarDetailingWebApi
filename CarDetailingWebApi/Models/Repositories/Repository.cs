﻿using CarDetailingWebApi.Models.db;
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
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<T>();
                r.info = "Poprawnie dodane";
                r.status = true;
                r.value = item;
                db.Entry(item).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return r;
            }
        }

        public Result<List<T>> Get()
        {
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
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
            using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var r = new Result<T>();
                r.info = "test mess";
                r.status = true;
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
                using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
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

               
                using (CarCosmeticSalonEntities db = new CarCosmeticSalonEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    ////i = db.Ts.Attach(item);
                    //i = db.Ts.Where(e => e.TId == item.TId).First();
                    //i.Email = item.Email;
                    //i.FirstName = item.FirstName;
                    //i.TTypeId = item.TTypeId;
                    //i.Login = item.Login;
                    //i.Password = item.Password;
                    //i.PhoneNumber = item.PhoneNumber;
                    //i.Surname = item.Surname;
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