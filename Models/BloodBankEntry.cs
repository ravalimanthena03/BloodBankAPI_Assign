﻿namespace BloodBankAPI_Assign.Models
{
    public class BloodBankEntry
    {
        public int Id { get; set; } // Auto-generated primary key
        public string DonorName { get; set; }
        public int Age { get; set; }
        public string BloodType { get; set; }
        public string ContactInfo { get; set; }
        public int Quantity { get; set; } // in ml
        public DateTime CollectionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; } // Available, Requested, Expired
    }
}
