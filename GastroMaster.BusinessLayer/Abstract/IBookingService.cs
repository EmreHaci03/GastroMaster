using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IBookingService:IGenericService<Booking>
    {
        Task TBookingStatusApproved(int id);
        Task TBookingStatusPending(int id);
        Task TCancelledStatusApproved(int id);
        Task<bool> TIsAlreadyBooking(DateTime date);
        Task<int> TGetTodayReservationCount();
        Task<int> TGetTotalReservationCount();
        Task<bool> TIsDuplicateBookingAsync(Booking booking);
    }
}
