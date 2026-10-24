namespace GastroMaster.BusinessLayer.Abstract
{
    public interface ICashRegisterService
    {
        Task<decimal> TGetTotalAmountAsync();
    }
}
