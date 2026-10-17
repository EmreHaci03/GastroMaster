using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class TestimonialManager:ITestimonialService
    {
        private readonly ITestimonialDal _testimonialDal;

        public TestimonialManager(ITestimonialDal testimonialDal)
        {
            _testimonialDal = testimonialDal;
        }

        public async Task TDeleteAsync(Testimonial t)
        {
            await _testimonialDal.DeleteAsync(t);
        }

        public async Task<List<Testimonial>> TGetAllAsync()
        {
            return await _testimonialDal.GetAllAsync();
        }

        public async Task<Testimonial?> TGetByIdAsync(int id)
        {
            return await _testimonialDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Testimonial t)
        {
            await _testimonialDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Testimonial t)
        {
            await _testimonialDal.UpdateAsync(t);
        }
    }
}
