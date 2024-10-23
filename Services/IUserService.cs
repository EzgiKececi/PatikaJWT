using PatikaJWT.Dtos;
using PatikaJWT.Types;

namespace PatikaJWT.Services
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);

        Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user);

    }
}
