using AutoMapper;
using MerchantProfile.Api.Common;
using MerchantProfile.Api.Helper;
using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Cryptography;
using System.Text;

namespace MerchantProfile.Api.Controllers
{
    [Route("api/merchant-profiles")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly MerchantDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenController(MerchantDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> register([FromBody] MerchantRegistrationDto dto)
        {
            if (MerchantExistsWithEmail(dto.Email))
            {
                return Ok(
                    new
                    {
                        status = Status.MerchantExists,
                        message = ResponseMess.MerchantEmailExists,
                    }
                );
            }

            Merchant newMerchant = new Merchant();

            newMerchant.Name = dto.Name;
            newMerchant.Email = dto.Email;
            newMerchant.Password = dto.Password;
            newMerchant.Phone = dto.Phone;
            newMerchant.Description = dto.Description;
            newMerchant.LinkWebsite = dto.LinkWebsite;
            newMerchant.Address = dto.Address;

            var data = GenarateDataSignature(newMerchant.Email, newMerchant.Password);

            var signature = GenerateHmacSHA256Signature(_configuration["secret_key"], data);

            newMerchant.signature = signature;

            newMerchant.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            _context.Merchants.Add(newMerchant);

            _context.SaveChanges();

            var response = new
            {
                ClientId = newMerchant.Id,
                Signature = signature
            };

            return Ok(new
            {
                status = Status.Success,
                message = ResponseMess.CreateMerchantSuccess,
                data = response
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginMerchant([FromBody] AuthenDto loginRequest)
        {

            var merchant = await _context.Merchants.FirstOrDefaultAsync(e => e.Email == loginRequest.Email);
            if (merchant == null)
            {
                return Ok(new
                {
                    status = Status.MerchantNotFound,
                    message = ResponseMess.MerchantNotFound
                });
            }

            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, merchant.Password))
            {
                return Ok(new
                {
                    Message = "Invalid email or password",
                    IsAuthenticated = false
                });
            }

            var data = GenarateDataSignature(merchant.Email, merchant.Password);

            // Tạo Signature
            var signature = GenerateHmacSHA256Signature(_configuration["secret_key"], data);

            // Lưu Signature vào cookie
            Response.Cookies.Append("Signature", signature, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            // Tạo phản hồi
            var response = new
            {
                Message = "Login successful",
                IsAuthenticated = true
            };

            return Ok(response);
        }

        private bool MerchantExistsWithEmail(string email)
        {
            return _context.Merchants.Any(e => e.Email == email);
        }

        private string GenerateHmacSHA256Signature(string secretKey, string data)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using (var hmacSHA256 = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmacSHA256.ComputeHash(dataBytes);

                StringBuilder result = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    result.Append(b.ToString("x2"));
                }
                return result.ToString();
            }
        }

        private string GenarateDataSignature(string email, string password)
        {
            var data = new { username = email, password = password };
            return JsonConvert.SerializeObject(data);
        }
    }
}
