namespace FlightBookingSystem.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } // Bu kullanım aslında properties dediğimiz bir kavram ve hani encapsulation da private kullanıyorduk diyorsunuz haklı olarak ama encapsulation field'ler için geçerli CS de (sharp çalışmıyor :D) ise bu kullanıma automatic properties deniliyor ve arka planda encapsulation yapılıyor. Aslında biz properties'i public yapıyoruz.
        public string Email { get; set; } // Public olan burda Email properties'idir field değildir. Field arka planda private. 
        public string Password { get; set; }
        public string Role { get; set; } // "Admin" veya "Client" olacak

        // İlişkiler
        public List<Ticket> Tickets { get; set; } // Kullanıcının aldığı biletler
    }
}
