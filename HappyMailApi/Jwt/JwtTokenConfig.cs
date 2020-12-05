using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyMailApi.Jwt
{
    public class JwtTokenConfig : IJwtTokenConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }

    public interface IJwtTokenConfig
    {
        string Secret { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        int AccessTokenExpiration { get; set; }
        int RefreshTokenExpiration { get; set; }
    }
}
