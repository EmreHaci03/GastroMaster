using AutoMapper;
using FluentValidation;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.TimeLineDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineService _timelineService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTimeLineDto> _createValidator;
        private readonly IValidator<UpdateTimeLineDto> _updateValidator;

        public TimelineController(ITimelineService timelineService, IMapper mapper, IValidator<CreateTimeLineDto> createValidator, IValidator<UpdateTimeLineDto> updateValidator)
        {
            _timelineService = timelineService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> TimelineList()
        {
            var values = await _timelineService.TGetAllAsync();
            var result = _mapper.Map<List<ResultTimeLineDto>>(values.OrderBy(x => x.Year));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimelineById(int id)
        {
            var value = await _timelineService.TGetByIdAsync(id);
            var result = _mapper.Map<GetTimeLineByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTimeline(CreateTimeLineDto createTimelineDto)
        {
            var validationResult = _createValidator.Validate(createTimelineDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

            var value = _mapper.Map<Timeline>(createTimelineDto);
            await _timelineService.TInsertAsync(value);
            return Ok("Yol haritası kaydı eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTimeline(UpdateTimeLineDto updateTimelineDto)
        {
            var validationResult = _updateValidator.Validate(updateTimelineDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

            var value = _mapper.Map<Timeline>(updateTimelineDto);
            await _timelineService.TUpdateAsync(value);
            return Ok("Yol haritası kaydı güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeline(int id)
        {
            var value = await _timelineService.TGetByIdAsync(id);
            await _timelineService.TDeleteAsync(value);
            return Ok("Yol haritası kaydı silindi");
        }
    }
}