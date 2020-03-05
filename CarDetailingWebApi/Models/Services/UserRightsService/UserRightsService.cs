using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.Repositories.UserRightsRepo;

namespace CarDetailingWebApi.Models.Services.UserRightsService
{
    public class UserRightsService : IUserRightsService
    {
        IUserRightsRepository repo;
        public UserRightsService(IUserRightsRepository rep)
        {
            repo = rep;
        }
        public Result<UserType> Add(UserType item)
        {
            return repo.Add(item);
        }

        public Result<List<UserType>> Get()
        {
           return repo.Get();
        }

        public Result<UserType> GetById(int id)
        {
            return repo.GetById(id);
        }

        public Result<UserType> Remove(int id)
        {
            return repo.Remove(id);
        }

        public Result<UserType> Update(UserType item)
        {
            return repo.Update(item);
        }
    }
}