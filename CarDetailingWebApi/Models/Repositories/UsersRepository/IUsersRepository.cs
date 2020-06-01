using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDetailingWebApi.Models
{
    public interface IUsersRepository:IRepository<User>
    {
        Result<User> Login(string login, string password);
        Boolean UserExist(string login);
        Result<User> GetByLogin(string login);
        Result<int> GetIdByLogin(string login);
    }
}
