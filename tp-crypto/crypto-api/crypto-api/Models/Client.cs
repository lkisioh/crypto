using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace crypto_api.Models
{
    // 1-) Hacer los modelos.
    public class Client
    {
        [Key]
        public int id { get; set; }
        [NotNull]
        public string name { get; set; }

        [NotNull, MinLength(7),]
        public string email { get; set; }
       
    }
}
