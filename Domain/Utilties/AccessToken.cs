using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utilties
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; } = DateTime.Now.AddMinutes(30);
        public string Role  { get; set; }
    }
}
