using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroMaster.BusinessLayer.Concrete
{
    public class CashRegisterManager:ICashRegisterService
    {
        private readonly ICashRegisterDal _cashRegisterDal;

        public CashRegisterManager(ICashRegisterDal cashRegisterDal)
        {
            _cashRegisterDal = cashRegisterDal;
        }

        public async Task<decimal> TGetTotalAmountAsync()
        {
            return await _cashRegisterDal.GetTotalAmountAsync();    
        }
    }
}
