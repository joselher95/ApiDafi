using System.Data;

namespace ApiDafi.DAL;

public interface IDataContext
{
    IDbConnection CrearConexion();
}