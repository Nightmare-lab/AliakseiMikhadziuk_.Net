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
    public class ContractController : ControllerBase
    {
        private readonly ContractService _contractService;

        private readonly ILogger<ContractController> _logger;

        public ContractController(ContractService contractService, ILogger<ContractController> logger)
        {
            _contractService = contractService;
            _logger = logger;
        }

        // GET: api/Contract
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> Get()
        {
            var contracts = (await _contractService.GetAllAsync()).ToList();

            return contracts;
        }

        // GET: api/Contract/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Contract>> Get(int id)
        {
            try
            {
                var contracts = await _contractService.GetAsync(id);
                return Ok(contracts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during getting contract. Exception: {ex.Message}");
                return BadRequest();
            }
        }

        // POST: api/Contract
        [HttpPost]
        [Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtToken.AuthSchemes)]
        public async Task<ActionResult<Contract>> Post([FromBody] Contract contract)
        {
            try
            {
                await _contractService.AddAsync(contract);
                return Ok(contract);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during creating contract. Exception: {ex.Message}");
                return BadRequest();
            }
        }

        // PUT: api/Contract/5
        [HttpPut]
        [Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtToken.AuthSchemes)]
        public async Task<ActionResult<Contract>> Put([FromBody] Contract contract)
        {
            try
            {
                await _contractService.EditAsync(contract);
                return Ok(contract);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during editing contract. Exception: {ex.Message}");
                return BadRequest();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtToken.AuthSchemes)]
        public async Task<ActionResult<Contract>> Delete(int id)
        {
            try
            {
                await _contractService.RemoveAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during deleting contract. Exception: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
