
using System.Data;

namespace Domain.Utilties
{
    public class RefreshToken
    {
        public string token {  get; set; }
        public DateTime Expiration  { get; set; } =DateTime.Now.AddDays(30);
        public bool IsExpired => DateTime.Now >= Expiration;
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;

    }
}
