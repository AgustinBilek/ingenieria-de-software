using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_MB29
{
    public class ConexionDB_MB29
    {
        SqlConnection conn = new SqlConnection("Server=localhost;Database=IngenieriaSoftware;Trusted_Connection=True;");

        public SqlConnection Conectar_MB29()
        {
            conn.Open();
            return conn;
        }

        public SqlConnection Desconectar_MB29()
        {
            conn.Close();
            return conn;
        }
    }
}
