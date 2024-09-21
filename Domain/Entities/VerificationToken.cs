using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class VerificationToken
    {
        public string Id  { get; set; }= Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public User  user  { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Expire_At { get; set; } =DateTime.Now.AddMinutes(3);
    }
}
