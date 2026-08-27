using DAL_MB29;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DigitoVerificadorDAL_MB29
    {
        public void Guardar_MB29(string nombreTabla, string dvh, string dvv)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            string query = @"
                IF EXISTS (SELECT 1 FROM DigitoVerificador WHERE NombreTabla = @N)
                    UPDATE DigitoVerificador 
                    SET DVH = @DVH, DVV = @DVV 
                    WHERE NombreTabla = @N
                ELSE
                    INSERT INTO DigitoVerificador (NombreTabla, DVH, DVV) 
                    VALUES (@N, @DVH, @DVV)";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@N", nombreTabla);
                cmd.Parameters.AddWithValue("@DVH", dvh);
                cmd.Parameters.AddWithValue("@DVV", dvv);
                cmd.ExecuteNonQuery();
            }

            conectar.Desconectar_MB29();
        }

        public (string DVH, string DVV)? Obtener_MB29(string nombreTabla)
        {
            var conectar = new ConexionDB_MB29();
            var conexion = conectar.Conectar_MB29();

            using (SqlCommand cmd = new SqlCommand(
                "SELECT DVH, DVV FROM DigitoVerificador WHERE NombreTabla = @N",
                conexion))
            {
                cmd.Parameters.AddWithValue("@N", nombreTabla);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var result = (reader["DVH"].ToString(), reader["DVV"].ToString());
                        conectar.Desconectar_MB29();
                        return result;
                    }
                }
            }

            conectar.Desconectar_MB29();
            return null;
        }
    }
}
