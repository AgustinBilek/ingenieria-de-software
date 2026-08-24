using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public class Encriptador_MB29
    {
        public static string EncriptarPassword_MB29(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                //Convertir contraseña string a arreglo de bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                //Calcula hash de esos bytes
                byte[] hash = sha.ComputeHash(bytes);
                //Convierte hash binario a string
                return Convert.ToBase64String(hash);
            }
        }
    }
}
