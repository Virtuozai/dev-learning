using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dev_learning.Authentication
{
    interface IJwtTokenGenerator
    { 
        string GenerateAccessToken(string email, IEnumerable userClaims);
    }
}
