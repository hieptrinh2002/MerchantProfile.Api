﻿using AutoMapper;
using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MerchantProfile.Api.Common;

namespace MerchantProfile.Api.Controllers
{
    [Route("api/marchant-profiles")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly MerchantDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public MerchantController(MerchantDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        // GET: api/marchant-profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Merchant>>> GetMerchants()
        {         
            var list = await _context.Merchants.ToListAsync();

            var response = new
            {
                status = Status.Success,
                message = ResponseMess.GetAllMerchantSuccess,
                data = list
            };
            return Ok(response);
        }

        // GET: api/marchant-profiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Merchant>> GetMerchant(int id)
        {
            var merchant = await _context.Merchants.FindAsync(id);

            if (merchant == null)
            {
                return Ok(new 
                {
                    status = Status.MerchantNotFound,
                    message = ResponseMess.MerchantNotFound
                });
            }

            return Ok(new
            {
                status = Status.Success,
                message = ResponseMess.GetOneMerchantSuccess,
                data = merchant
            });
        }

        // POST: api/marchant-profiles
        [HttpPost]
        public async Task<ActionResult> PostMerchant(MerchantDto merchantDto)
        {
            Merchant newMerchant = new Merchant();
            try
            {
                if(MerchantExistsWithEmail(merchantDto.Email))
                {
                    return Ok(
                        new{
                            status = Status.MerchantExists,
                            message = ResponseMess.MerchantEmailExists,
                        }
                    );
                }
                //VALIDATION_ERROR

                newMerchant = _mapper.Map<Merchant>(merchantDto);
               
                _context.Merchants.Add(newMerchant);
                await _context.SaveChangesAsync();
                return Ok(
                    new{
                        status = Status.Success,
                        message = ResponseMess.CreateMerchantSuccess,
                        data = newMerchant
                    }
                );
            }
            catch (Exception ex)
            {
                return Ok(
                    new{
                        status = Status.Failed,
                        message = ResponseMess.CreateMerchantFailed,
                    }
                );
            }
        }

        // PUT: api/marchant-profiles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMerchant(string id, MerchantDto merchantDto)
        {
            try
            {
                if(!MerchantExistsWithId(id))
                {
                    return Ok(new
                    {
                        status = Status.MerchantNotFound,
                        message = ResponseMess.MerchantNotFound
                    });
                }
                //VALIDATION_ERR
                var merchant = await _context.Merchants.FirstOrDefaultAsync(e => e.Id == id);
                _mapper.Map(merchantDto, merchant);

                _context.SaveChanges();

                return Ok(
                    new { 
                        Message = ResponseMess.UpdateMerchantSuccess, 
                        status = Status.Success
                    }
                );
            }
            catch (Exception ex) {
                return Ok(new 
                {
                    status = Status.Failed,
                    Message = ResponseMess.UpdateMerchantFailed,
                });
            }

        }

        // DELETE: api/marchant-profiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMerchant(int id)
        {
            var merchant = await _context.Merchants.FindAsync(id);
            if (merchant == null)
            {
                return Ok(new
                {
                    status = Status.MerchantNotFound,
                    message = ResponseMess.MerchantNotFound
                });
            }

            _context.Merchants.Remove(merchant);
            await _context.SaveChangesAsync();


            return Ok(new
            {
                status = Status.Success,
                message = ResponseMess.DeleteMerchantSuccess,
                data = merchant.Id
            });
        }

        private bool MerchantExistsWithId(string id)
        {
            return _context.Merchants.Any(e => e.Id == id);
        }

        private bool MerchantExistsWithEmail(string email)
        {
            return _context.Merchants.Any(e => e.Email == email);
        }
    }
}