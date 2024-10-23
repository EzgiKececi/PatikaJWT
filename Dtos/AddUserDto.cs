

using PatikaJWT.Entities;
using System.Security.Principal;

namespace PatikaJWT.Dtos
{
    public class AddUserDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
