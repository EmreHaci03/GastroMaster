using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.FeedBackDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackService _feedBackService;
        private readonly IMapper _mapper;

        public FeedBackController(IFeedBackService feedBackService, IMapper mapper)
        {
            _feedBackService = feedBackService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> FeedBackList()
        {
            var values = await _feedBackService.TGetAllAsync();
            var result = _mapper.Map<List<ResultFeedBackDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedBackById(int id)
        {
            var value = await _feedBackService.TGetByIdAsync(id);
            var result = _mapper.Map<GetFeedBackByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedBack(CreateFeedBackDto createFeedBackDto)
        {
            var value = _mapper.Map<FeedBack>(createFeedBackDto);
            await _feedBackService.TInsertAsync(value);
            return Ok("Geri Bildirim alanı eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeedBack(UpdateFeedBackDto updateFeedBackDto)
        {
            var value = _mapper.Map<FeedBack>(updateFeedBackDto);
            await _feedBackService.TUpdateAsync(value);
            return Ok("Geri Bildirim alanı güncellendi");
        }

        [HttpPut("MarkAsRead/{id}")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _feedBackService.TMarkAsReadAsync(id);
            return Ok("Geri bildirim okundu olarak işaretlendi");
        }

        [HttpPut("MarkAsUnread/{id}")]
        public async Task<IActionResult> MarkAsUnread(int id)
        {
            await _feedBackService.TMarkAsUnreadAsync(id);
            return Ok("Geri bildirim okunmadı olarak işaretlendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedBack(int id)
        {
            var value = await _feedBackService.TGetByIdAsync(id);
            await _feedBackService.TDeleteAsync(value);
            return Ok("Geri Bildirim alanı silindi");
        }
    }
}
