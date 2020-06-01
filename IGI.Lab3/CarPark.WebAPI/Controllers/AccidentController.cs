using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarPark.BLL.Models;
using CarPark.BLL.Services;
using CarPark.WebAPI.Models;
using CarPark.WebUI.Areas.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarPark.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtInformation.AuthSchemes)]
    public class AccidentController : Controller
    {
        private readonly AccidentService _accidentService;

        private readonly ILogger<AccidentService> _logger;

        public AccidentController(AccidentService accidentService, ILogger<AccidentService> logger)
        {
            _accidentService = accidentService;
            _logger = logger;
        }

        // GET: api/Accident
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Accident>>> Get()
        {
            var accident = (await _accidentService.GetAllAsync()).ToList();

            return accident;
        }

        // GET: api/Accident/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Accident>> Get(int id)
        {
            try
            {
                var accidents = await _accidentService.GetAsync(id);
                return Ok(accidents);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during getting accidents. Exception: {ex.Message}");
                return BadRequest();
            }
        }

        // POST: api/Accident
        [HttpPost]
        public async Task<ActionResult<Accident>> Post([FromBody] Accident accident)
        {
            try
            {
                await _accidentService.AddAsync(accident);
                return Ok(accident);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during creating accidents. Exception: {ex.Message}");
                return BadRequest();
            }
        }

        // PUT: api/Accident/5
        [HttpPut]
        public async Task<ActionResult<Accident>> Put([FromBody] Accident accident)
        {
            try
            {
                await _accidentService.EditAsync(accident);
                return Ok(accident);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during editing accident. Exception: {ex.Message}");
                return BadRequest();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Accident>> Delete(int id)
        {
            try
            {
                await _accidentService.RemoveAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during deleting accident. Exception: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
