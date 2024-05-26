using AutoMapper;
using MerchantProfile.Api.Common;
using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MerchantProfile.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly MerchantDbContext _context;
        private readonly IMapper _mapper;
        public AuthenController(MerchantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Login(AuthenDto authenDto)
        {
            var merchant = await _context.Merchants.FirstOrDefaultAsync(m => m.Email == authenDto.Email);
            if (merchant == null)
            {
                return Ok(new
                {
                    status = Status.Success,
                    messages = ResponseMess.MerchantNotFound,
                });
            }

            if (!BCrypt.Net.BCrypt.Verify(authenDto.Password, merchant.PasswordHash))
            {
                return Unauthorized();
            }

            return Ok(new {
                    status = Status.Success, 
                    messages = "success",
                    data = merchant
                }
            );
        }
    }
}
