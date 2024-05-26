using AutoMapper;
using MerchantProfile.Api.Common;
using MerchantProfile.Api.Helper;
using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;

namespace MerchantProfile.Api.Controllers
{
    [Route("api/merchant-profiles")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MerchantDbContext _context;
        private readonly IMapper _mapper;
        public AccountController(MerchantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterMerchant(MerchantDto merchantDto)
        {
            // Kiểm tra xem merchant đã tồn tại hay chưa
            var existingMerchant = await _context.Merchants
                                        .FirstOrDefaultAsync(m => m.Email == merchantDto.Email);
            if (existingMerchant != null)
            {
                return Ok(new
                {
                    status = Status.MerchantExists,
                    Message = ResponseMess.MerchantEmailExists,
                });
            }

            // Tạo mới merchant
            var merchant = _mapper.Map<Merchant>(merchantDto);
             
            // Tạo mật khẩu mặc định và gửi email cho merchant
            var password = ApplicationHelper.GenerateToken();
            merchant.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // Lưu merchant vào database
            await _context.Merchants.AddAsync(merchant);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = Status.Success,
                Message = ResponseMess.RegisterSuccess,
            });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            // Tìm merchant dựa trên email
            var merchant = await _context.Merchants.FirstOrDefaultAsync(m => m.Email == model.Email);
            if (merchant == null)
            {
                return Ok(new
                {
                    status = Status.MerchantNotFound,
                    Message = ResponseMess.MerchantNotFound,
                });
            }
            
            // Kiểm tra mật khẩu hiện tại
            if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, merchant.PasswordHash))
            {
                return Ok(new
                {
                    status = Status.Failed,
                    Message = ResponseMess.CurrentPasswordIncorect,
                });
            }

            // Cập nhật mật khẩu mới
            merchant.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            _context.Merchants.Update(merchant);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = Status.Success,
                Message = ResponseMess.PasswordChangedSuccess
            });
        }
    }
}
