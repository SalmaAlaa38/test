using Domain.Utilties;
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
        public string Role { get; set; }="USER";
        public bool IsPhoneVerified { get; set; } = false;
        public List<RefreshToken?> RefreshTokens { get; set; } 
            = new List<RefreshToken?>();
        public List<VerificationToken>? verificationTokens { get; set; }

    }
}
