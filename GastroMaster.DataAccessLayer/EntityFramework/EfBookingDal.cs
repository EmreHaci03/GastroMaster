using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfBookingDal : GenericRepository<Booking>, IBookingDal
    {
        private readonly GastroMasterContext _context;
        public EfBookingDal(GastroMasterContext context) : base(context)
        {
            _context = context;
        }

        public async Task BookingStatusApproved(int id)
        {
            var booking=await _context.Bookings.FindAsync(id);
            if(booking != null)
            {
                booking.Status = "Rezervasyon Onaylandı";
                await _context.SaveChangesAsync();
            }
        }

        public async Task BookingStatusPending(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if( booking != null )
            {
                booking.Status = "Rezervasyon Alındı";
                await _context.SaveChangesAsync();  
            }
        }

        public async Task CancelledStatusApproved(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                booking.Status = "Rezervasyon İptal Edildi";
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetTodayReservationCount()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            return await _context.Bookings.Where(x => x.Date >= today && x.Date < tomorrow).CountAsync();
        }

        public async Task<int> GetTotalReservationCount()
        {
            return await _context.Bookings.CountAsync();
        }

        public async Task<bool> IsAlreadyBooking(DateTime date)
        {
            return await _context.Bookings.AnyAsync(x => x.Date == date);
        }

        public async Task<bool> IsDuplicateBookingAsync(Booking booking)
        {
            return await _context.Bookings.AnyAsync(x =>
                x.Name == booking.Name &&
                x.Phone == booking.Phone &&
                x.Mail == booking.Mail &&
                x.PersonCount == booking.PersonCount &&
                x.Date == booking.Date);
        }
    }
}
