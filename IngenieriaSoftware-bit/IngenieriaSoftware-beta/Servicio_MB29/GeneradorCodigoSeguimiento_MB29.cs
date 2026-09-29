using System;

namespace Servicio_MB29
{
    // Genera el código de seguimiento del envío (comprobante, paso 6 de PN1)
    // y su dígito verificador, calculado con algoritmo módulo 11.
    public static class GeneradorCodigoSeguimiento_MB29
    {
        // Código base: prefijo + IdEnvio con padding + parte de la fecha (yyMMdd)
        public static string GenerarCodigo_MB29(int idEnvio)
        {
            string baseNumerica = idEnvio.ToString("D8") + DateTime.Now.ToString("yyMMdd");
            char dv = CalcularDigitoVerificador_MB29(baseNumerica);
            return baseNumerica + dv;
        }

        // Módulo 11: se pondera cada dígito (de derecha a izquierda) con
        // pesos cíclicos 2,3,4,5,6,7. Si el resto da 10 -> DV = 0 (o 'K' según
        // convención; acá se usa 0 para simplificar validaciones numéricas).
        public static char CalcularDigitoVerificador_MB29(string codigoBase)
        {
            if (string.IsNullOrWhiteSpace(codigoBase))
                throw new ArgumentException("El código base no puede estar vacío.");

            int suma = 0;
            int peso = 2;

            for (int i = codigoBase.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(codigoBase[i]))
                    continue;

                int digito = codigoBase[i] - '0';
                suma += digito * peso;
                peso = (peso == 7) ? 2 : peso + 1;
            }

            int resto = 11 - (suma % 11);
            int dv = (resto == 11) ? 0 : (resto == 10) ? 0 : resto;

            return dv.ToString()[0];
        }

        // Valida que el código de seguimiento completo (base + DV) sea consistente.
        public static bool ValidarCodigo_MB29(string codigoCompleto)
        {
            if (string.IsNullOrWhiteSpace(codigoCompleto) || codigoCompleto.Length < 2)
                return false;

            string baseNumerica = codigoCompleto.Substring(0, codigoCompleto.Length - 1);
            char dvInformado = codigoCompleto[codigoCompleto.Length - 1];
            char dvCalculado = CalcularDigitoVerificador_MB29(baseNumerica);

            return dvInformado == dvCalculado;
        }
    }
}
