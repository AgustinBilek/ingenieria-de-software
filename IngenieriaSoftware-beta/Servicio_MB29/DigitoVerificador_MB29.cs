using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Servicio_MB29
{
    public static class DigitoVerificador_MB29
    {
        public static string CalcularDVH_MB29<T>(List<T> registros)
        {
            var propiedades = typeof(T)
                .GetProperties()
                .OrderBy(p => p.Name)
                .ToList();

            var hashesFila = registros.Select(r =>
            {
                var valores = propiedades
                    .Select(p => p.GetValue(r)?.ToString() ?? "");

                return Hashear_MB29(string.Join("|", valores));
            });

            return Hashear_MB29(string.Join("|", hashesFila));
        }

        public static string CalcularDVV_MB29<T>(List<T> registros)
        {
            var propiedades = typeof(T)
                .GetProperties()
                .OrderBy(p => p.Name)
                .ToList();

            var hashesColumna = propiedades.Select(prop =>
            {
                var valores = registros
                    .Select(r => prop.GetValue(r)?.ToString() ?? "");

                return Hashear_MB29(string.Join("|", valores));
            });

            return Hashear_MB29(string.Join("|", hashesColumna));
        }

        private static string Hashear_MB29(string texto)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                return Convert.ToBase64String(sha.ComputeHash(bytes));
            }
        }
    }
}
