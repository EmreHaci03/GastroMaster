using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IBookingDal : IGenericDal<Booking>
    {
        Task BookingStatusApproved(int id);
        Task CancelledStatusApproved(int id);
        Task BookingStatusPending(int id);

        Task<bool> IsAlreadyBooking(DateTime date);
        Task<int> GetTodayReservationCount();
        Task<int> GetTotalReservationCount();
        Task<bool> IsDuplicateBookingAsync(Booking booking);
    }
}
