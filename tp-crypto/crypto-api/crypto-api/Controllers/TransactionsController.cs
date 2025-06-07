using crypto_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crypto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TransactionsController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get()
        {
            return await _context.Transactions
                .ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Transaction>> Post(Transaction newTransaction)
        {
            _context.Transactions.Add(newTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = newTransaction.id }, newTransaction);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(int id)
        {
            //_context.Amigos.FindAsync(id); cuando busco con find no puedo poner el includes
            var transaction = await _context.Transactions
              .Include(x => x.client_data)
              .FirstOrDefaultAsync(x => x.id == id);
            if (transaction == null)
            {
                return NotFound();
            }
            return transaction;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, Transaction transaction)
        {
            if (id != transaction.id)
            {
                return BadRequest();
            }
            _context.Entry(transaction).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
