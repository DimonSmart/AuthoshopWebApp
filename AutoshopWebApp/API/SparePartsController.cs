using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;

namespace AutoshopWebApp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SparePartsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SparePartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SpareParts
        [HttpGet]
        public async Task<IEnumerable<SparePart>> GetSpareParts([FromQuery] string search)
        {
            var query = _context.SpareParts.Select(x => x);

            if (!string.IsNullOrEmpty(search))
            {
                var substrings = search.Split(' ');

                foreach (var item in substrings)
                {
                    foreach (var str in substrings)
                    {
                        query =
                            from partData in query
                            where partData.MarkAndModel.CarMark.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                            partData.MarkAndModel.CarModel.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                            partData.PartName.Contains(str, StringComparison.OrdinalIgnoreCase)
                            select partData;
                    }
                }
            }

            return await query
                .AsNoTracking()
                .Include(x => x.MarkAndModel)
                .ToListAsync();
        }

        // GET: api/SpareParts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSparePart([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sparePart = await _context.SpareParts.FindAsync(id);

            if (sparePart == null)
            {
                return NotFound();
            }

            return Ok(sparePart);
        }

        // PUT: api/SpareParts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSparePart([FromRoute] int id, [FromBody] SparePart sparePart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sparePart.SparePartId)
            {
                return BadRequest();
            }

            _context.Entry(sparePart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SparePartExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SpareParts
        [HttpPost]
        public async Task<IActionResult> PostSparePart([FromBody] SparePart sparePart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SpareParts.Add(sparePart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSparePart", new { id = sparePart.SparePartId }, sparePart);
        }

        // DELETE: api/SpareParts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSparePart([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sparePart = await _context.SpareParts.FindAsync(id);
            if (sparePart == null)
            {
                return NotFound();
            }

            _context.SpareParts.Remove(sparePart);
            await _context.SaveChangesAsync();

            return Ok(sparePart);
        }

        private bool SparePartExists(int id)
        {
            return _context.SpareParts.Any(e => e.SparePartId == id);
        }
    }
}