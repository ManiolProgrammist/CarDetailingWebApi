using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.Repositories.UserRightsRepo
{
    public class UserRightsRepository:Repository<UserType>,IUserRightsRepository
    {
    }
}