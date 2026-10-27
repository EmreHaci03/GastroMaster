using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.DataAccessLayer.Abstract
{
    public interface IBookingDal : IGenericDal<Booking>
    {
        Task<bool> IsAlreadyBooking(DateTime date);
        Task<int> GetTodayReservationCount();
        Task<int> GetTotalReservationCount();
        Task<bool> IsDuplicateBookingAsync(Booking booking);
    }
}
