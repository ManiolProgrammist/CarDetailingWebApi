
using CarDetailingWebApi.Models.db;
using CarDetailingWebApi.Models.HelpModels;


namespace CarDetailingWebApi.Models
{

    public interface IUserService:IService<User>
    {
        Result<User> Login(string username, string password);
        AuthorizationEnum CheckAuthority(string username, string password);
        Result<User> Add(RegisterUserModel item);
        bool CheckIfUserRightsChanged(User us);
        Result<User> GetByLogin(string login);
    }
}
