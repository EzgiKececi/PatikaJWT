using PatikaJWT.Context;
using PatikaJWT.Dtos;
using PatikaJWT.Entities;
using PatikaJWT.Services;
using PatikaJWT.Types;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PatikaJWT.Managers
{
    public class UserManager : IUserService
    {
        private readonly JWTDbContext _db;
        public UserManager(JWTDbContext db)
        {
            _db = db;
        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var newUser = new UserEntity
            {
                Email = user.Email,
                Password = user.Password,
                CreatedDate = DateTime.UtcNow
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return new ServiceMessage { IsSucceed = true, Message = "Kayıt başarıyla oluşturuldu." };
        }

        public async Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user)
        { 
            var userEntity = _db.Users.Where(x=>x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı hatalı."
                };
            }

            if (userEntity.Password == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        Id = userEntity.Id,
                        UserType = userEntity.UserType,

                    }
                };


            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Parola hatalı."
                };
            }
        
        
        }



           
    }
}
