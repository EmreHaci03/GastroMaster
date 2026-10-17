

namespace GastroMaster.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task TInsertAsync(T t);
        Task TUpdateAsync(T t);
        Task TDeleteAsync(T t);
        Task<T?> TGetByIdAsync(int id);
        Task<List<T>> TGetAllAsync();
    }
}
