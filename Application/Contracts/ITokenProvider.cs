using Domain.Entities;
using Domain.Utilties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface  ITokenProvider
    {
        public AccessToken GenerateAccessToken(User user);
        public RefreshToken GenerateRefreshToken();
    }
}
