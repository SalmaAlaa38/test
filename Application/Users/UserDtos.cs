using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserDtos
    {
        public record  LoginUserDto
            (
            string Email ,
            string Password
            );
        public record RegisterUserDto
            (
            string Username, 
            string Email,
            string Password,
            string Phone ,
            string Address
            );
    }
}
