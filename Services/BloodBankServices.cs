
using BloodBankAPI_Assign.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankAPI_Assign.Services
{
    public class BloodBankServices
    {
        private readonly List<BloodBankEntry> _bloodBankEntries;
        public BloodBankServices()
        {
            // Initializing with some dummy data
            _bloodBankEntries = new List<BloodBankEntry>
        {
            new BloodBankEntry
            {
                Id = 1,
                DonorName = "Ravali",
                Age = 20,
                BloodType = "A+",
                ContactInfo = "ravali@example.com",
                Quantity = 500,
                CollectionDate = System.DateTime.Now.AddDays(-2),
                ExpirationDate = System.DateTime.Now.AddMonths(1),
                Status = "Available"
            }
        };
        }
        public Task<List<BloodBankEntry>> GetAllDonors()
        {
            return Task.FromResult(_bloodBankEntries.ToList());
        }
        public Task<List<BloodBankEntry>> GetAllEntries(int pageNumber, int pageSize)
        {
            var entries = _bloodBankEntries.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return Task.FromResult(entries);
        }
        public Task<BloodBankEntry> GetEntryById(int id)
        {
            var entry = _bloodBankEntries.FirstOrDefault(e => e.Id == id);
            return Task.FromResult(entry);
        }

        public Task<BloodBankEntry> CreateEntry(BloodBankEntry entry)
        {
            entry.Id = _bloodBankEntries.Any() ? _bloodBankEntries.Max(e => e.Id) + 1 : 1;
            _bloodBankEntries.Add(entry);
            return Task.FromResult(entry);
        }
        public Task<List<BloodBankEntry>> AddBulkEntries(List<BloodBankEntry> donorEntries)
        {
            foreach (var entry in donorEntries)
            {
                entry.Id = _bloodBankEntries.Any() ? _bloodBankEntries.Max(e => e.Id) + 1 : 1;
                _bloodBankEntries.Add(entry);
            }

            return Task.FromResult(donorEntries);
        }


        public Task<BloodBankEntry> UpdateEntry(int id, BloodBankEntry updatedEntry)
        {
            var existingEntry = _bloodBankEntries.FirstOrDefault(e => e.Id == id);
            if (existingEntry == null) return Task.FromResult<BloodBankEntry>(null);

            existingEntry.DonorName = updatedEntry.DonorName;
            existingEntry.Age = updatedEntry.Age;
            existingEntry.BloodType = updatedEntry.BloodType;
            existingEntry.ContactInfo = updatedEntry.ContactInfo;
            existingEntry.Quantity = updatedEntry.Quantity;
            existingEntry.CollectionDate = updatedEntry.CollectionDate;
            existingEntry.ExpirationDate = updatedEntry.ExpirationDate;
            existingEntry.Status = updatedEntry.Status;

            return Task.FromResult(existingEntry);
        }

        public Task<bool> DeleteEntry(int id)
        {
            var entry = _bloodBankEntries.FirstOrDefault(e => e.Id == id);
            if (entry == null) return Task.FromResult(false);

            _bloodBankEntries.Remove(entry);
            return Task.FromResult(true);
        }

        public Task<List<BloodBankEntry>> SearchEntries(string bloodType, string status, string donorName)
        {
            var query = _bloodBankEntries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(bloodType))
                query = query.Where(e => e.BloodType == bloodType);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(e => e.Status == status);

            if (!string.IsNullOrWhiteSpace(donorName))
                query = query.Where(e => e.DonorName.Contains(donorName));

            return Task.FromResult(query.ToList());
        }

    }
}
