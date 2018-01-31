using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Reactive.Webapi
{
    public class AccessToken
    {
        private string _name;
        private string _id;
        private string _token;

        public string Token => _token ?? (_token = Generate());

        public AccessToken(string name, string id)
        {
            _name = name;
            _id = id;
        }

        private string Generate()
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("123456789123456789123456789");
            var securityKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var identity = new ClaimsIdentity(new GenericIdentity(_name, "TokenAuth"), new List<Claim>() { new Claim("id", _id)});
            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "Reactive",
                Audience = "Reactive",
                SigningCredentials = signingCredentials,
                Subject = identity,
                Expires = DateTime.MaxValue
            });
            var tokenString = handler.WriteToken(token);
            return tokenString;
        }
    }
}
