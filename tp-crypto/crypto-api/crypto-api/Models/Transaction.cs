namespace crypto_api.Models
{
    public class Transaction
    {
       public int id { get; set; }
       public string action { get; set; }
       public string crypto_code { get; set; }
       public int client_id { get; set; }
       public Client? client_data { get; set; }
       public float crypto_amount { get; set; }
       public float money { get; set; }
       public DateTime datetime { get; set; }
    }
}
