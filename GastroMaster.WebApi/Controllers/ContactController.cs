using AutoMapper;
using GastroMaster.BusinessLayer.Abstract;
using GastroMaster.DtoLayer.DTOS.ContactDtos;
using GastroMaster.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GastroMaster.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService contactService;
        private readonly IMapper mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            this.contactService = contactService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await contactService.TGetAllAsync();
            var result = mapper.Map<List<ResultContactDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var value = await contactService.TGetByIdAsync(id);
            var result = mapper.Map<GetContactByIdDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            var value = mapper.Map<Contact>(createContactDto);
            await contactService.TInsertAsync(value);
            return Ok("İletişim bilgisi eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            var value = mapper.Map<Contact>(updateContactDto);
            await contactService.TUpdateAsync(value);
            return Ok("İletişim bilgisi güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var value = await contactService.TGetByIdAsync(id);
            await contactService.TDeleteAsync(value);
            return Ok("İletişim bilgisi silindi");
        }
    }
}