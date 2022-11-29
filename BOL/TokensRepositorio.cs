using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiDafi.BOL.Modelos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace ApiDafi.BOL;

public class TokensRepositorio : ITokensRepositorio
{
    private readonly IConfiguration _configuration;
    private readonly IUsuariosReporitorio _usuarios;

    public TokensRepositorio(IConfiguration configuration, IUsuariosReporitorio usuarios)
    {
        _configuration = configuration;
        _usuarios = usuarios;
    }

    public async Task<Modelos.Login> Login(Usuario user)
    {
        var usuario = await _usuarios.ObtenerUsuarioPorUsername(user);

        if (usuario == null)
            throw new Exception("No se encontró el usuario");

        bool passwordMatch = false;

        byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(user.USERNAME.ToUpper() + user.PASSWORD.ToUpper());
        using (System.Security.Cryptography.SHA512Managed mhash = new System.Security.Cryptography.SHA512Managed())
        {
            byte[] bytHash = mhash.ComputeHash(bytValue);
            mhash.Clear();
            string strPasswordResultante = Convert.ToBase64String(bytHash);
            passwordMatch = strPasswordResultante == usuario.PASSWORD;
        }

        if (!passwordMatch) 
            throw new Exception("La contraseña especificada no es válida");

        //Validacion de usuario que exista
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);


        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", "1"),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.USERNAME),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.Now.AddDays(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);
        var jwtToken = tokenHandler.WriteToken(token);

        Login login = new Login()
        {
            Username = user.USERNAME,
            Token = jwtToken
        };

        return login;

    }
}