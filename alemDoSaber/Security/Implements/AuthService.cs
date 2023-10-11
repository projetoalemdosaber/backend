using Microsoft.IdentityModel.Tokens;
using RedeSocial.Model;
using RedeSocial.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RedeSocial.Security.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserLogin?> Autenticar(UserLogin userLogin)
        {
            string FotoPadrao = "https://imgur.com/a/N38LGJU";

            if (userLogin is null || string.IsNullOrEmpty(userLogin.Usuario) || string.IsNullOrEmpty(userLogin.Usuario))
                return null;

            var BuscarUsuario = await _userService.GetByUsuario(userLogin.Usuario);

            if (BuscarUsuario is null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(userLogin.Senha, BuscarUsuario.Senha))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(Settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userLogin.Usuario)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            userLogin.Id = BuscarUsuario.Id;
            userLogin.Nome = BuscarUsuario.Nome;
            userLogin.Foto = BuscarUsuario.Foto is null ? FotoPadrao : BuscarUsuario.Foto;
            userLogin.Token = "Bearer " + tokenHandler.WriteToken(token).ToString();
            userLogin.DataNascimento = BuscarUsuario.DataNascimento;
            userLogin.Senha = "";

            return userLogin;
        }
    }
}
