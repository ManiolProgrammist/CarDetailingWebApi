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
           return retWithoutPassword(_userRepository.Add(item));
        }
        //zrób validację
        public Result<User> Add(RegisterUserModel item)
        {
        
            if (!_userRepository.UserExist(item.Login))
            {
                User u = new User() {UserTypeId=2, Password = item.Password, Login = item.Login, Email = item.Email, FirstName = item.FirstName, Surname = item.Surname, PhoneNumber=item.PhoneNumber };

                return retWithoutPassword(_userRepository.Add(u));
            }
            var r = new Result<User>();
            r.info = "taki użytkownik istnieje";
            r.status = false;
            return r;
        }

        public Result<List<User>> Get()
        {
            var r = _userRepository.Get();
            foreach (var u in r.value)
            {
                u.Password = "";
            }
            return r;
        }

        public Result<User> GetById(int id)
        {
  
            return retWithoutPassword(_userRepository.GetById(id));
        }

        public Result<User> Remove(int id)
        {
        
           return retWithoutPassword(_userRepository.Remove(id));
        }

        //update info -> not password
        public Result<User> Update(User item)
        {
            
            if (item.Password == "")
            {
                item.Password=_userRepository.GetById(item.UserId).value.Password;
            }
            return retWithoutPassword(_userRepository.Update(item));
        }
        public Result<User> UpdatePassword(User item,string password)
        {
            item.Password = password;
            return retWithoutPassword(_userRepository.Update(item));
        }
        public Result<User> GetByLogin(string login)
        {
            return retWithoutPassword(_userRepository.GetByLogin(login));
        }

        public bool CheckIfUserRightsChanged(User us)
        {
            var us2 = GetById(us.UserId);
            return us2.value.UserTypeId != us.UserTypeId;
        }
        private Result<User> retWithoutPassword(Result<User> us)
        {
            us.value.Password = "";
            return us;
        }

    }


}
