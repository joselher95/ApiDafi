using ApiDafi.BOL.Modelos;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiDafi.BOL;

public interface ITokensRepositorio
{
    Task<Modelos.Login> Login(Usuario user);
}