using ApiDafi.BOL.Modelos;

namespace ApiDafi.BOL
{
    public interface IReporteRepositorio
    {
        Task<List<Reporte>> ObtenerReportesPorUsuario(Usuario usuario);
        Task<Reporte> ObtenerReportePorFolio(int Folio);
        Task InsertarReporte(Reporte reporte);
    }
}
