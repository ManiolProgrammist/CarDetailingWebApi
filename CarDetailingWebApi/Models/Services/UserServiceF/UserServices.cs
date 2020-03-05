using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models
{
  
    public class UserServices:IUserService
    {
        IUsersRepository _userRepository;
        public UserServices(IUsersRepository r)
        {
            _userRepository = r;
        }
        public Result<User> Login(string username,string password)
        {
            return _userRepository.Login(username, password);
        }
        public AuthorizationEnum CheckAuthority(string username,string password)
        {
            AuthorizationEnum en = AuthorizationEnum.NotExistUser;
            var r = Login(username, password);
            if (r.status)
            {
                if (r.value.UserType.AccessRights>3)
                {
                    en = AuthorizationEnum.AdminUser;
                }
                else
                {
                    if (r.value.UserType.AccessRights == 3)
                    {
                        en = AuthorizationEnum.EmployeeUser;
                    }
                    else
                    {
                        if (r.value.UserType.AccessRights==1)
                        {
                            en = AuthorizationEnum.TemporaryUser;
                        }
                        else
                        {
                            if(r.value.UserType.AccessRights==2)
                            en = AuthorizationEnum.NormalUser;
                        }
                    }
                }
              
            }
            return en;
        }
        public Result<User> Add(User item)
        {
           return _userRepository.Add(item);
        }
        //zrób validację
        public Result<User> Add(RegisterUserModel item)
        {
            var r = new Result<User>();
            if (!_userRepository.UserExist(item.login))
            {
                User u = new User() {UserTypeId=2, Password = item.password, Login = item.login, Email = item.email, FirstName = item.firstName, Surname = item.surname,PhoneNumber=item.phoneNumber };

                r = _userRepository.Add(u);
                return r;
            }
            r.info = "taki użytkownik istnieje";
            r.status = false;
            return r;
        }

        public Result<List<User>> Get()
        {
        
            return _userRepository.Get();
        }

        public Result<User> GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public Result<User> Remove(int id)
        {

            return _userRepository.Remove(id);
        }

        public Result<User> Update(User item)
        {
            return _userRepository.Update(item);
        }

        public bool CheckIfUserRightsChanged(User us)
        {
            var us2 = GetById(us.UserId);
            return us2.value.UserTypeId != us.UserTypeId;
        }
    }


}
