using AutoMapper;
using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MerchantProfile.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly MerchantDbContext _context;
        private readonly IMapper _mapper;
        public RegisterController(MerchantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> register(RegisterDto dto)
        {
            var checkEmail = _context.Merchants.Any(m => m.Email == dto.Email);
            if (checkEmail)
                return BadRequest(new { Message = "Email already exists for other account" });
            try
            {
                Merchant newMerchant = _mapper.Map<Merchant>(dto);
                newMerchant.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

                await _context.Merchants.AddAsync(newMerchant);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Message = "register successfully!"
                });
            }
            catch (Exception ex)
            {
                return Conflict(new
                {
                    Message = "can't create new user!"
                });
                throw;
            }
        }
    }
}
