using FlightBookingSystem.DTO;
using FlightBookingSystem.Interfaces.IRepositories;
using FlightBookingSystem.Interfaces.IServices;
using FlightBookingSystem.Model;
using FlightBookingSystem.UOW;

namespace FlightBookingSystem.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(ITicketRepository ticketRepository, IFlightRepository flightRepository, IUnitOfWork unitOfWork)
        {
            _ticketRepository = ticketRepository;
            _flightRepository = flightRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> PurchaseTicketAsync(TicketDto ticketDto)
        {
            // Flight'ı bul ve kapasiteyi kontrol et
            var flight = await _flightRepository.GetFlightByIdAsync(ticketDto.FlightId);
            if (flight == null || flight.Capacity <= 0)
            {
                return false; // Uçuş yok ya da kapasite dolu
            }

            // Bilet oluştur
            var ticket = new Ticket
            {
                UserId = ticketDto.UserId,
                FlightId = ticketDto.FlightId,
                PurchaseDate = DateTime.UtcNow
            };

            // Bileti kaydet ve kapasiteyi azalt
            await _ticketRepository.AddTicketAsync(ticket);
            flight.Capacity--; // Uçuş kapasitesini düşür
            await _unitOfWork.CompleteAsync(); // Tüm değişiklikleri kaydet

            return true;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId)
        {
            return await _ticketRepository.GetTicketsByUserIdAsync(userId);
        }
    }

}
