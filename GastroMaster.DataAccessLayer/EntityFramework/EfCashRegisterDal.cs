using GastroMaster.DataAccessLayer.Abstract;
using GastroMaster.DataAccessLayer.Concrete;
using GastroMaster.DataAccessLayer.Repositories;
using GastroMaster.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace GastroMaster.DataAccessLayer.EntityFramework
{
    public class EfCashRegisterDal : GenericRepository<CashRegister>, ICashRegisterDal
    {
        private readonly GastroMasterContext _context;
        public EfCashRegisterDal(GastroMasterContext context) : base(context)
        {
            _context = context;
        }


        public async Task<decimal> GetTotalAmountAsync()
        {
            return await _context.CashRegisters.SumAsync(x=>x.TotalAmount);
        }
    }
}
