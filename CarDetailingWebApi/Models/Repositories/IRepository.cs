using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models
{
    public interface IRepository<T> where T:class
    {
        Result<T> GetById(int id);
        Result<List<T>> Get();
        Result<T> Add(T item);
        Result<T> Remove(int id);

        Result<T> Update(T item);
    }
}