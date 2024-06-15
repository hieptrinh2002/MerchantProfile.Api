using AutoMapper;
using MerchantProfile.Api.Models.Dtos;
using MerchantProfile.Api.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MerchantProfile.Api.Controllers
{
    [Route("api/merchant-profiles")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _authenServices;
        private readonly ILogger<AuthenController> _logger;

        public AuthenController(IAuthenService authenServices, ILogger<AuthenController> logger)
        {
            _authenServices = authenServices;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            _logger.LogInformation("Login API called for user: {Username}", dto?.Username);

            try
            {
                var result = await _authenServices.LoginAsync(dto);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Login successful for user: {Username}", dto.Username);
                    return Ok(new
                    {
                        Message = result.Message,
                        Token = result.Data
                    });
                }
                _logger.LogWarning("Login failed for user: {Username}, Message: {Message}", dto.Username, result.Message);
                return BadRequest(new { Message = result.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing login for user: {Username}", dto?.Username);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
