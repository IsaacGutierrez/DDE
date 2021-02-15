using dde.api.Helpers;
using dde.api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using dde.dataaccess;
using dde.dataaccess.Models;

namespace dde.api.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User getUserById(int id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private DDEContext _entities;

        public UserService(IOptions<AppSettings> appSettings, DDEContext entities)
        {
            _appSettings = appSettings.Value;
            _entities = entities;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest user)
        {
            var userFinded = _entities.Usuarios
                .Where(userModel => userModel.NombreUsuario.ToUpper() == user.Username.ToUpper())
                .FirstOrDefault();

            if (userFinded != null)
            {
                if (validatePassword(userFinded.Password, user.Password, userFinded.Salt))
                {
                    var userInfo = new User(userFinded.UsuarioId, userFinded.NombreCompleto, userFinded.NombreUsuario);
                    // authentication successful so generate jwt token
                    var token = GenerateJwtToken(userInfo);

                    return new AuthenticateResponse(userInfo, token);
                }
            }
            return null;
        }
      

        // helper methods

        private bool validatePassword(string seudoPassword, string password, string salt)
        {
            
            string hashPassword = GenerateHashPassword(password, salt);
            return hashPassword == seudoPassword;
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateHashPassword(string password, string salt)
        {
            byte[] passwordWithSaltBytes = Encoding.ASCII.GetBytes((password+salt));
            using (HashAlgorithm hasGenerator = SHA256.Create())
            {
                byte[] hashComputed = hasGenerator.ComputeHash(passwordWithSaltBytes);
                string hashString = Convert.ToBase64String(hashComputed);
                return hashString;
            }
        }

        public User getUserById(int id)
        {
            var userInfo = _entities.Usuarios.Find(id);
            if (userInfo != null) {
                var user = new User(userInfo.UsuarioId, userInfo.NombreCompleto, userInfo.NombreUsuario);
            }
            return null;
        }
    }
}
