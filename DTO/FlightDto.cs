namespace FlightBookingSystem.DTO
{
    public class FlightDto
    {
        public string Departure { get; set; } // Kalkış yeri
        public string Arrival { get; set; } // Varış yeri
                                                // Tarih ve saat ayrı ayrı
        public string Date { get; set; } // Uçuş tarihi (örneğin, "2024-05-02")
        public string Time { get; set; } // Uçuş saati (örneğin, "12:45")
        public int Capacity { get; set; } // Uçak kapasitesi
        public decimal Price { get; set; } // Bilet fiyatı
        public bool IsActive { get; set; } // Uçuş aktif mi
    }
}
