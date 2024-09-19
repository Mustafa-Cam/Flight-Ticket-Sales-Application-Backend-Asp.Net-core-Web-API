namespace FlightBookingSystem.Model
{
    public class Ticket
    {
        public int Id { get; set; }              // Bilet ID'si
        public int UserId { get; set; }          // Kullanıcı ID'si foreign key olarak belirtmek için kullanıyoruz. ve eğer User bilgisi taamamen gelmesin dersen bu id üzerinden lazy loading yaparsın. yani eğer istersen user bilgileri de gelir. 
        public int FlightId { get; set; }        // Uçuş ID'si
        public DateTime PurchaseDate { get; set; } // Satın alma tarihi

        // İlişkiler
        public User User { get; set; }
        public Flight Flight { get; set; }
    }

}
