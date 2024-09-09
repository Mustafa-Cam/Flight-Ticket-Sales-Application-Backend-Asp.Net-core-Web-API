namespace FlightBookingSystem.Model
{
    public class Ticket
    {
        public int Id { get; set; }              // Bilet ID'si
        public int UserId { get; set; }          // Kullanıcı ID'si
        public int FlightId { get; set; }        // Uçuş ID'si
        public DateTime PurchaseDate { get; set; } // Satın alma tarihi

        // İlişkiler
        public User User { get; set; }
        public Flight Flight { get; set; }
    }

}
