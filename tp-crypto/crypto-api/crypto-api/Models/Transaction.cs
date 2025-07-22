using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace crypto_api.Models
{
    public class Transaction
    {
        [Key]
       public int id { get; set; }
        [NotNull,Required]
       public string action { get; set; }
        [NotNull,Required]
       public string crypto_code { get; set; }
        [NotNull, Required]
       public int client_id { get; set; }
       public Client? client_data { get; set; }

        [NotNull]
       public float crypto_amount { get; set; }
        [NotNull]
       public float money { get; set; }
        [NotNull,Required]
       public DateTime datetime { get; set; }
    }
}
