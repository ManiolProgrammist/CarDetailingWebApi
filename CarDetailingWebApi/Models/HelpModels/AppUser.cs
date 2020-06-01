using CarDetailingWebApi.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models.HelpModels
{
    public class AppUser:User
    {
        public AppUser()
        {
        }
        public AppUser(User u,UserType usertype)
        {
            UserId = u.UserId;
            Login = u.Login;
            Email = u.Email;
            FirstName = u.FirstName;
            Surname = u.Surname;
            PhoneNumber = u.PhoneNumber;
            AccoutCreateDate = u.AccoutCreateDate;
            
        }

    }
}