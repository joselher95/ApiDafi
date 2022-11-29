using ApiDafi.BOL;
using ApiDafi.BOL.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDafi.Controllers
{
    [Authorize]
    public class ReportesController : BaseApiController
    {
        private readonly IReporteRepositorio _reportesRepositorio;

        public ReportesController(IReporteRepositorio reportesRepositorio)
        {
            _reportesRepositorio = reportesRepositorio;
        }

        [HttpGet]
        [Route("/api/[controller]/ObtenerReportes")]
        public async Task<IActionResult> ObtenerReportes(string Username)
        {
            try
            {
                BOL.Modelos.Usuario usuario = new Usuario()
                {
                    USERNAME = Username
                };

                var reportes = await _reportesRepositorio.ObtenerReportesPorUsuario(usuario);
                return Ok(reportes);
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo obtener los reportes. Error: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/[controller]/ObtenerReporte")]
        public async Task<IActionResult> ObtenerReporte(int Folio)
        {
            try
            {
                
                var reporte = await _reportesRepositorio.ObtenerReportePorFolio(Folio);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo obtener el reporte. Error: " + ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/[controller]/GuardarReporte")]
        public async Task<IActionResult> GuardarReporte([FromBody] Reporte reporte)
        {
            try
            {
                await _reportesRepositorio.InsertarReporte(reporte);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo obtener el reporte. Error: " + ex.Message);
            }
        }
    }
}
