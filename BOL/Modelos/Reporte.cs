namespace ApiDafi.BOL.Modelos
{
    public class Reporte
    {
        public int Folio { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        public int EstatusID { get; set; }
        public Estatus Estatus { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get;}
    }
}
