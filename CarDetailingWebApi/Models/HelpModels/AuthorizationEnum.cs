using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels
{
    public enum AuthorizationEnum
    {
        NotExistUser = 0,
        TemporaryUser = 1,
        NormalUser =2,
        EmployeeUser = 3,
        AdminUser=4
    }
}