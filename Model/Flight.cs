using Microsoft.EntityFrameworkCore;

namespace FlightBookingSystem.Model
{
    public class Flight
    {
        public int Id { get; set; }
        public string Origin { get; set; } // Kalkış yeri
        public string Destination { get; set; } // Varış yeri
        public DateTime Date { get; set; } // Uçuş tarihi
        public int Capacity { get; set; } // Uçak kapasitesi
        public decimal Price { get; set; } // Bilet fiyatı
        public bool IsActive { get; set; } // Uçuş aktif mi

        // İlişkiler
        public List<Ticket> Tickets { get; set; } // Uçuşun alındığı biletler
    }

}
