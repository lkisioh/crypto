using crypto_api.Models;
using crypto_api.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace crypto_api.Controllers
{
    // 4-) Hacer controladores
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ClientController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            return await _context.Clients
                .ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client newClient)
        {
            if (!Validation.ValidEmail(newClient))
            {
                return BadRequest("Email inválido");
            }
            _context.Clients.Add(newClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = newClient.id }, newClient);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            //_context.Amigos.FindAsync(id); cuando busco con find no puedo poner el includes
            var client = await _context.Clients
                .FindAsync(id);
                //.Include(x => x.Localidad)
                 // .FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return client;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, Client client)
        {
            if (id != client.id)
            {
                return BadRequest();
            }
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

