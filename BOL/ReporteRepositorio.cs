using ApiDafi.BOL.Modelos;
using ApiDafi.DAL;
using Dapper;

namespace ApiDafi.BOL
{
    public class ReporteRepositorio : IReporteRepositorio
    {
        private readonly DataConext _context;

        public ReporteRepositorio(DataConext context)
        {
            _context = context;
        }

        public async Task<Reporte> ObtenerReportePorFolio(int Folio)
        {
            string query = 
                "SELECT R.Folio, R.Titulo, R.Descripcion, R.Imagen, R.EstatusID, R.FechaRegistro, R.UsuarioRegistro,\n" +
                "   R.FechaModificacion, R.UsuarioModificacion, E.ID, E.Descripcion, E.Color\n" +
                "FROM Reportes R\n" +
                "INNER JOIN Estatus E ON E.ID = R.EstatusID \n" +
                "WHERE R.Folio = @Folio";

            BOL.Modelos.Reporte objReporte = new Reporte()
            {
                Folio = Folio
            };

            using (var connection = _context.CrearConexion())
            {
                var reporte = await connection.QueryAsync<Reporte, Estatus, Reporte>(query, (Reporte, Estatus) => {
                    Reporte.Estatus = Estatus;
                    return Reporte;
                }, param: objReporte);
                return reporte.FirstOrDefault();
            }
        }

        public async Task<List<Reporte>> ObtenerReportesPorUsuario(Usuario usuario)
        {
            string query =
                "SELECT R.Folio, R.Titulo, R.Descripcion, R.Imagen, R.EstatusID, R.FechaRegistro, R.UsuarioRegistro,\n" +
                "   R.FechaModificacion, R.UsuarioModificacion, E.ID, E.Descripcion, E.Color\n" +
                "FROM Reportes R \n" +
                "INNER JOIN Estatus E ON E.ID = R.EstatusID \n" +
                "WHERE R.UsuarioRegistro = @USERNAME";

            using (var connection = _context.CrearConexion())
            {
                var reporte = await connection.QueryAsync<Reporte, Estatus, Reporte>(query, (Reporte, Estatus) => {
                    Reporte.Estatus = Estatus;
                    return Reporte;
                }, param: usuario);
                return reporte.ToList();
            }
        }

        public async Task InsertarReporte(Reporte reporte)
        {
            var query = 
                "INSERT INTO Reportes\n" +
                "VALUES (@Titulo, @Descripcion, @Imagen, @EstatusID, @FechaRegistro, @UsuarioRegistro, @FechaModificacion, @UsuarioModificacion)";

            using (var connection = _context.CrearConexion())
            {
                var result = await connection.ExecuteAsync(query, reporte);
            }
        }
    }
}
