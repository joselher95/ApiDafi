using System.Data;
using Microsoft.Data.SqlClient;

namespace ApiDafi.DAL;

public class DataConext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DataConext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("SqlConnection");
    }

    public IDbConnection CrearConexion() => new SqlConnection(_connectionString);
}