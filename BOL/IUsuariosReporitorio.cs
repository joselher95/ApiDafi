using ApiDafi.BOL.Modelos;

namespace ApiDafi.BOL
{
    public interface IUsuariosReporitorio
    {
        Task<Usuario> ObtenerUsuarioPorUsername(Usuario usuario);
    }
}
