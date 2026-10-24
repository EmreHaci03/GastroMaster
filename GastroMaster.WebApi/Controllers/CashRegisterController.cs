using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.CashRegisterDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashRegisterController : ControllerBase
    {
        private readonly ICashRegisterService _cashRegisterService;
        public CashRegisterController(ICashRegisterService cashRegisterService)
        {
            _cashRegisterService = cashRegisterService;
        }

        [HttpGet("TotalCashRegisterAmount")]
        public async Task<IActionResult> GetTotalCashRegisterAmount()
        {
            var values = await _cashRegisterService.TGetTotalAmountAsync();
            return Ok(values);
        }
    }
}
