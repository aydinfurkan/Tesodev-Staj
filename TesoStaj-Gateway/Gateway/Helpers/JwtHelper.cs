using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Gateway.Helpers
{
    public static class JwtHelper
    {
        private const string _privateKey = "CBA^6-LMgSV6tje2eJtW*7E+3#k?cRWrCe7wv*v++u@v_+!4XmxL3#Wp=eBP-P-+@jytumXvr-5Re5DuLjDygSa%P5snp-9GJX?--LPLN$3kcPkS#w$?%MhJp?KE%QQQmpV?__Nv@De$H8VxGn*pR7c?H?8syh5zW7Ha3A3x6FeeMuHXYy!W#x%XRH5$S=C$x@xu=U2aG*WDcSNzhRWKAvuzD?ea223nqpyLJzZpAH4xEXV^Yv^kfRMzrCM2y-_P";
        public static string CreateJwt(IEnumerable<Claim> claims, int validMinutes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimIdentities = CreateClaimsIdentities(claims);

            var token = tokenHandler.CreateJwtSecurityToken(
                subject: claimIdentities,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(validMinutes),
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.Default.GetBytes(_privateKey)),
                    SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(token);
        }

        public static IEnumerable<Claim> GetClaims(string jwt)
        {
            var key = Encoding.ASCII.GetBytes(_privateKey);
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            
            var handler = new JwtSecurityTokenHandler();
            var claims = handler.ValidateToken(jwt, validations,out var tokenSecure);

            return claims.Claims;
        }

        public static bool IsTimeValid(string jwt)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jwt);
            return token.ValidTo.CompareTo(DateTime.UtcNow) <= 0;
        }

        private static ClaimsIdentity CreateClaimsIdentities(IEnumerable<Claim> claims)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            foreach (var claim in claims)
            {
                claimsIdentity.AddClaim(claim);
            }
            return claimsIdentity;
        }
    }
}