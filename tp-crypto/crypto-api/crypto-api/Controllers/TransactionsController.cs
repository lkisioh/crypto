using crypto_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;

namespace crypto_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        public TransactionsController(AppDbContext context, IHttpClientFactory httpClientFactory) { 
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Get()
        {
            return await _context.Transactions
                .ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Transaction>> Post(Transaction newTransaction)
        {
            // Se puede vender
            if (newTransaction.action == "sell")
            {
                var totalBuy = await _context.Transactions
                    .Where(t => t.client_id == newTransaction.client_id && t.crypto_code == newTransaction.crypto_code && t.action == "purchase")
                    .SumAsync(t => t.crypto_amount);

                var totalSold = await _context.Transactions
                    .Where(t => t.client_id == newTransaction.client_id && t.crypto_code == newTransaction.crypto_code && t.action == "sell")
                    .SumAsync(t => t.crypto_amount);

                var total = totalBuy - totalSold;

                if (newTransaction.crypto_amount > total)
                    return BadRequest("No tiene suficientes cryptomonedas para vender");

            }

            // Llamar a la API externa
            string url = $"https://criptoya.com/api/satoshitango/{newTransaction.crypto_code}/ars";
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                decimal cost = root.GetProperty("totalAsk").GetDecimal();

                decimal totalMoney = (decimal)newTransaction.crypto_amount * cost;
                newTransaction.money = (float)totalMoney;

                DateTime time = DateTime.Now;
                newTransaction.datetime = time;

                _context.Transactions.Add(newTransaction);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = newTransaction.id }, newTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al llamar API externa: {ex.Message}");
            }

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
        public async Task<ActionResult> Patch(int id, Transaction editTransaction)
        {
            if (id != editTransaction.id)
            {
                return BadRequest();
            }
            if (editTransaction.action == "sell")
            {
                var totalBuy = await _context.Transactions
                    .Where(t => t.client_id == editTransaction.client_id && t.crypto_code == editTransaction.crypto_code && t.action == "purchase")
                    .SumAsync(t => t.crypto_amount);

                var totalSold = await _context.Transactions
                    .Where(t => t.client_id == editTransaction.client_id && t.crypto_code == editTransaction.crypto_code && t.action == "sell")
                    .SumAsync(t => t.crypto_amount);

                var total = totalBuy - totalSold;

                if (editTransaction.crypto_amount > total)
                    return BadRequest("No tiene suficientes cryptomonedas para vender");

            }
            string url = $"https://criptoya.com/api/satoshitango/{editTransaction.crypto_code}/ars";
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                decimal cost = root.GetProperty("totalAsk").GetDecimal();

                decimal totalMoney = (decimal)editTransaction.crypto_amount * cost;
                editTransaction.money = (float)totalMoney;

                DateTime time = DateTime.Now;
                editTransaction.datetime = time;

                _context.Entry(editTransaction).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al llamar API externa: {ex.Message}");
            }

            
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
