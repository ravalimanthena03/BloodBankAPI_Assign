using BloodBankAPI_Assign.Models;
using BloodBankAPI_Assign.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI_Assign.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodbankController : ControllerBase
    {   
        private readonly BloodBankServices _services;
        public BloodbankController(BloodBankServices bloodBankServices) {
            _services = bloodBankServices;
        }

        [HttpGet("all-donors")]
        public async Task<IActionResult> GetAllDonors()
        {
            var entries = await _services.GetAllDonors();
            return Ok(entries);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( int page = 1, int size = 10)
        {
            var entries = await _services.GetAllEntries(page, size);
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _services.GetEntryById(id);
            if (entry == null) return NotFound();
            return Ok(entry);
        }

        [HttpPost]
        public async Task<IActionResult> Create( BloodBankEntry entry)
        {
            var createdEntry = await _services.CreateEntry(entry);
            return CreatedAtAction(nameof(GetById), new { id = createdEntry.Id }, createdEntry);
        }

        [HttpPost("bulk-add")]
        public async Task<IActionResult> AddBulkDonors([FromBody] List<BloodBankEntry> donorEntries)
        {
            if (donorEntries == null || !donorEntries.Any())
            {
                return BadRequest("The donor list cannot be empty.");
            }

            var addedEntries = await _services.AddBulkEntries(donorEntries);
            return Ok(new { Message = $"{addedEntries.Count} entries added successfully.", Entries = addedEntries });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BloodBankEntry updatedEntry)
        {
            var updated = await _services.UpdateEntry(id, updatedEntry);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _services.DeleteEntry(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string bloodType = null, string status = null,string donorName = null)
        {
            var results = await _services.SearchEntries(bloodType, status, donorName);
            return Ok(results);
        }

    }
}
