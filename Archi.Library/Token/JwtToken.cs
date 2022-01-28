using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Archi.Library.Token
{
    public static class JwtToken
    {
        public static readonly string SECRET_KEY = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
 
        public static string GenerateJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("https://localhost:44306",
                                             "https://localhost:44306",
                                             expires: DateTime.Now.AddDays(1),
                                             signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
