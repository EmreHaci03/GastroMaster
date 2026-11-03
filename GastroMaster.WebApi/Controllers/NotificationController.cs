using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.NotificationDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> NotificationList()
        {
            var values = await _notificationService.TGetAllAsync();
            var result = _mapper.Map<List<ResultNotificationDto>>(values);
            return Ok(result);
        }

        [HttpGet("NotificationCountByStatusFalse")]
        public async Task<IActionResult> NotificationCountByStatusFalseAsync()
        {
            var values = await _notificationService.TNotificationCountByStatusFalse();
            return Ok(values);
        }

        [HttpGet("NotificationListByFalseStatus")]
        public async Task<IActionResult> GetNotificationListByFalseStatus()
        {
            var values = await _notificationService.TGetNotificationListByFalseStatus();
            var mapper=_mapper.Map<List<ResultNotificationDto>>(values);
            return Ok(mapper);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var value = await _notificationService.TGetByIdAsync(id);
            var result = _mapper.Map<GetNotificationByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification(CreateNotificationDto createNotificationDto)
        {
            var value = _mapper.Map<Notification>(createNotificationDto);
            await _notificationService.TInsertAsync(value);
            return Ok("Bildirim eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            var value = _mapper.Map<Notification>(updateNotificationDto);
            await _notificationService.TUpdateAsync(value);
            return Ok("Bildirim güncellendi");
        }
        [HttpPut("ChangeNotificationStatusToTrue/{id}")]
        public async Task<IActionResult> ChangeNotificationStatusToTrue(int id)
        {
            await _notificationService.TChangeNotificationStatusByTrue(id);
            return Ok("Bildirim Okundu olarak işaretlendi");

        }

        [HttpPut("ChangeNotificationStatusToFalse/{id}")]
        public async Task<IActionResult> ChangeNotificationStatusToFalse(int id)
        {
            await _notificationService.TChangeNotificationStatusByFalse(id);
            return Ok("Bildirim Okunmadı olarak işaretlendi");

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var value = await _notificationService.TGetByIdAsync(id);
            await _notificationService.TDeleteAsync(value);
            return Ok("Bildirim silindi");
        }
    }
}
