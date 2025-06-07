using crypto_api.Models;
using System.Text.RegularExpressions;

namespace crypto_api.Validations
{
    internal abstract class Validation
    {
        public static bool ValidEmail(Client client)
        {
            // Expresión regular para validar correos electrónicos
            string mustContain = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            // Crea un objeto de Regex
            Regex regex = new Regex(mustContain);

            // Verifica si el correo coincide con el patrón
            return regex.IsMatch(client.email);
        }
    }
}
