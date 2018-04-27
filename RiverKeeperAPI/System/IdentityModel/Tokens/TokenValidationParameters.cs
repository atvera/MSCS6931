using Microsoft.IdentityModel.Tokens;

namespace System.IdentityModel.Tokens
{
    internal class TokenValidationParameters : Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        public bool ValidateIssuer { get; set; }
    }
}