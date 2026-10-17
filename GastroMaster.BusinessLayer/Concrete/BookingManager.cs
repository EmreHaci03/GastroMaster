using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class BookingManager:IBookingService
    {
        private readonly IBookingDal _bookingDal;

        public BookingManager(IBookingDal bookingDal)
        {
            _bookingDal = bookingDal;
        }

        public async Task TDeleteAsync(Booking t)
        {
            await _bookingDal.DeleteAsync(t);
        }

        public async Task<List<Booking>> TGetAllAsync()
        {
            return await _bookingDal.GetAllAsync();
        }

        public async Task<Booking?> TGetByIdAsync(int id)
        {
            return await _bookingDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Booking t)
        {
            await _bookingDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Booking t)
        {
            await _bookingDal.UpdateAsync(t);
        }
    }
}
