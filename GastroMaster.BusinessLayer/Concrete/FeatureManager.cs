using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.EntityLayer.Entities;


namespace GastroMaster.BusinessLayer.Concrete
{
    public class FeatureManager:IFeatureService
    {
        private readonly IFeatureDal _featureDal;

        public FeatureManager(IFeatureDal featureDal)
        {
            _featureDal = featureDal;
        }

        public async Task TDeleteAsync(Feature t)
        {
            await _featureDal.DeleteAsync(t);
        }

        public async Task<List<Feature>> TGetAllAsync()
        {
            return await _featureDal.GetAllAsync();
        }

        public async Task<Feature?> TGetByIdAsync(int id)
        {
            return await _featureDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(Feature t)
        {
            await _featureDal.InsertAsync(t);
        }

        public async Task TUpdateAsync(Feature t)
        {
            await _featureDal.UpdateAsync(t);
        }
    }
}
