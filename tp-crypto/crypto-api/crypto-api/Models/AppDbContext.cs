using Microsoft.EntityFrameworkCore;
namespace crypto_api.Models
{
    // 2-) Descargar paquetes
    // microsoft.entityframeworkcore
    // microsoft.entityframeworkcore.Sqlserver
    // microsoft.visualstudio.web.codegeneration.design
    // microsoft.entityframeworkcore.tools

    public class AppDbContext : DbContext // Con el using podemos hacer la herencia
    {
        // Poner bien la cadena de conexión del appsetting
        // builder del program.cs
        
        // 3-) Hacer migracion
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
