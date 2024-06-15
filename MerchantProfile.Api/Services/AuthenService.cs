using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos.Request;
using MerchantProfile.Api.Models.Dtos.Response;
using MerchantProfile.Api.Models.Entities;
using MerchantProfile.Api.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MerchantProfile.Api.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly MerchantDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenService> _logger;
        public AuthenService(MerchantDbContext context, IConfiguration configuration, ILogger<AuthenService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string Message, TokenResponse Data)> LoginAsync(LoginDto dto)
        {
            _logger.LogInformation("Login attempt for user: {Username}", dto?.Username);

            if (dto == null || string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                _logger.LogWarning("Login failed: Username and Password are required.");
                return (false, "Username and Password are required", null);
            }

            var merchant = _context.Merchants.SingleOrDefault(u => u.Username == dto.Username);
            if (merchant == null)
            {
                _logger.LogWarning("Login failed: Invalid username - {Username}", dto.Username);
                return (false, "Invalid username!", null);
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, merchant.Password);
            if (!isPasswordValid)
            {
                _logger.LogWarning("Login failed: Invalid password for user - {Username}", dto.Username);
                return (false, "Invalid password!", null);
            }

            var authClaims = GetClaims(merchant);
            (string token, DateTime expiration, string tokenId) = GenerateToken(authClaims);

            _logger.LogInformation("Token generated successfully for user: {Username}", dto.Username);

            return (true, "Login successfully !", new TokenResponse
            {
                AccessToken = token,
                MerchantId = merchant.Id,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = expiration
            });
        }

        public IEnumerable<Claim> GetClaims(Merchant merchant)
        {
            _logger.LogDebug("Generating claims for user: {Username}", merchant.Username);

            return new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name, merchant.Username),
                new Claim("MerchantId", merchant.Id.ToString())
            };
        }

        public (string token, DateTime expire, string tokenId) GenerateToken(IEnumerable<Claim> claims)
        {
            _logger.LogDebug("Generating token");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["Jwt:TokenExpiryTimeInHour"]);
            var _TokenExpiryTimeInDays = Convert.ToInt64(_configuration["Jwt:TokenExpiryTimeInDay"]);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddDays(_TokenExpiryTimeInDays),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _logger.LogDebug("Token generated with ID: {TokenId}", token.Id);

            return (tokenHandler.WriteToken(token), token.ValidTo, token.Id);
        }
    }
}
