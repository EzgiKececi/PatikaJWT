using Microsoft.SqlServer.Server;
using PatikaJWT.Entities;

namespace PatikaJWT.Dtos
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }

    }
}
