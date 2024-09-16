namespace FlightBookingSystem.DTO
{
    namespace FlightBookingSystem.DTO
    {
        public class FlightSearchDto
        {
            public string Departure { get; set; } // Kalkış yeri
            public string Arrival { get; set; } // Varış yeri
            public DateTime? StartDate { get; set; } // Başlangıç tarihi (Nullable)
            public DateTime? EndDate { get; set; } // Bitiş tarihi (Nullable)
            public decimal? MinPrice { get; set; } // Min bilet fiyatı
            public decimal? MaxPrice { get; set; } // Max bilet fiyatı
            
            public int? Capacity { get; set; }
        }
    }

}
