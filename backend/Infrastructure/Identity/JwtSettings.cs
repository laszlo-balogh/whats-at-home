using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Identity
{
    public class JwtSettings
    {
        public required string Issuer { get; set; } 
        public required string Audience { get; set; }
        public required string Key { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
    }
}
