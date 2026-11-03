using AutoMapper;
using FluentValidation;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.BookingDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;
        private readonly IMapper mapper;
        private readonly IValidator<CreateBookingDto> _validator;
        private readonly IValidator<UpdateBookingDto> _updateValidator;

        public BookingController(IBookingService bookingService, IMapper mapper, IValidator<CreateBookingDto> validator, IValidator<UpdateBookingDto> updateValidator)
        {
            this.bookingService = bookingService;
            this.mapper = mapper;
            _validator = validator;
            _updateValidator = updateValidator;
        }
   
        [HttpGet]
        public async Task<IActionResult> BookingList()
        {
            var values = await bookingService.TGetAllAsync();
            var result = mapper.Map<List<ResultBookingDto>>(values);
            return Ok(result);
        }

        [HttpPut("BookingStatusPending/{id}")]
        public async Task<IActionResult> BookingStatusPending(int id)
        {
            await bookingService.TBookingStatusPending(id);
            return Ok("Rezervasyon Beklemeye Alındı");
        }

        [HttpPut("BookingStatusApproved/{id}")]
        public async Task<IActionResult> BookingStatusApproved(int id)
        {
            await bookingService.TBookingStatusApproved(id);
            return Ok("Rezervasyon Onaylandı");
        }


        [HttpPut("BookingStatusCancelled/{id}")]
        public async Task<IActionResult> BookingStatusCancelled(int id)
        {
            await bookingService.TCancelledStatusApproved(id);
            return Ok("Rezervasyon İptal Edildi");
        }


        [HttpGet("TotalBookingCount")]
        public async Task<IActionResult> TotalBookingCount()
        {
            var result = await bookingService.TGetTotalReservationCount();
            return Ok(result);
        }

        [HttpGet("TodayBookingCount")]
        public async Task<IActionResult> TodayBookingCount()
        {
            var result = await bookingService.TGetTodayReservationCount();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var value = await bookingService.TGetByIdAsync(id);
            var result = mapper.Map<GetBookingByIdDto>(value);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDto createBookingDto)
        {
            var validationResult=_validator.Validate(createBookingDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

            var alreadyBooking = await bookingService.TIsAlreadyBooking(createBookingDto.Date);
            if (alreadyBooking)
            {
                return BadRequest("Bu tarih ve saat için zaten rezervasyon var");
            }
            var value = mapper.Map<Booking>(createBookingDto);
            if (await bookingService.TIsDuplicateBookingAsync(value))
            {
                return BadRequest("Bu bilgilerle zaten bir rezervasyonun var");
            }
            await bookingService.TInsertAsync(value);
            return Ok("Rezervasyon eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var validationResult = _updateValidator.Validate(updateBookingDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

            var value = mapper.Map<Booking>(updateBookingDto);
            await bookingService.TUpdateAsync(value);
            return Ok("Rezervasyon güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var value = await bookingService.TGetByIdAsync(id);
            await bookingService.TDeleteAsync(value);
            return Ok("Rezervasyon silindi");
        }
    }
}