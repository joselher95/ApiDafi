using ApiDafi.BOL.Modelos;
using ApiDafi.DAL;
using Dapper;

namespace ApiDafi.BOL
{
    public class UsuariosRepositorio : IUsuariosReporitorio
    {
        private readonly DataConext _context;

        public UsuariosRepositorio(DataConext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObtenerUsuarioPorUsername(Usuario usuario)
        {
            string query = "SELECT USERNAME, PASSWORD FROM COM_USUARIOS WHERE USERNAME = @USERNAME";

            using (var connection = _context.CrearConexion())
            {
                var usuarios = await connection.QueryAsync<Usuario>(query, usuario);
                return usuarios.FirstOrDefault();
            }
        }
    }
}
