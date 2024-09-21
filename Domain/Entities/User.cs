using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public string Id  { get; set; } = Guid.NewGuid().ToString();
        public string Username  { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public string  Phone { get; set; }
        public string Role { get; set; }
        public bool IsPhoneVerified { get; set; } = false;

    }
}
