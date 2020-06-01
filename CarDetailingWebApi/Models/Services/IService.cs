
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models
{

    public interface IService<T>
    {
        Result<T> GetById(int id);
        Result<List<T>> Get();
        Result<T> Add(T item);
        Result<T> Remove(int id);
        Result<T> Update(T item);
    }
}
