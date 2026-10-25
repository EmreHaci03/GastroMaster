using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class SliderManager:ISliderService
    {
        private readonly ISliderDal _sliderDal;

        public SliderManager(ISliderDal sliderDal)
        {
            _sliderDal = sliderDal;
        }

        public async Task TDeleteAsync(Slider t)
        {
            await _sliderDal.DeleteAsync(t);
        }

        public async Task<List<Slider>> TGetAllAsync()
        {
            return await _sliderDal.GetAllAsync();
        }

        public async Task<Slider?> TGetByIdAsync(int id)
        {
            return await _sliderDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Slider t)
        {
            await _sliderDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Slider t)
        {
            await _sliderDal.UpdateAsync(t);
        }
    }
}
