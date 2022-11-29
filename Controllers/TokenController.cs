using ApiDafi.BOL;
using ApiDafi.BOL.Modelos;
using ApiDafi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ApiDafi.Controllers;

[AllowAnonymous]
public class TokenController : BaseApiController
{
    private readonly ITokensRepositorio _tokens;
    private Retorno retorno;

    public TokenController(ITokensRepositorio tokens)
    {
        _tokens = tokens;
    }

    [HttpPost]
    public async Task<IActionResult> Login(Usuario user)
    {
        retorno = new Retorno();
        try
        {
            var login = await _tokens.Login(user);
            retorno.Datos = login;
            return Ok(retorno);
        }
        catch(Exception ex)
        {
            retorno.Error = true;
            retorno.Mensaje = ex.Message;
            return BadRequest(retorno);
        }
    }
}