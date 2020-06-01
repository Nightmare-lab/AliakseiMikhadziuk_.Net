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
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        private readonly ILogger<CarController> _logger;

        public CarController(CarService carService, ILogger<CarController> logger)
        {
            _carService = carService;
            _logger = logger;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> Get()
        {
            var cars = (await _carService.GetAllAsync()).ToList();
            return cars;
        }

        // GET: api/Car/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            try
            {
                var car = await _carService.GetAsync(id);

                return car;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during getting car. Exception: {ex.Message}");
                return BadRequest();
            }
        }

        // POST: api/Car
        [Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtToken.AuthSchemes)]
        [HttpPost]
        public async Task<ActionResult<Car>> Post([FromBody] Car car)
        {
            try
            {
                await _carService.AddAsync(car);
                return Ok(car);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during creating car. Exception: {ex.Message}");
                return BadRequest();
            }
        }

        // PUT: api/Car/5
        [HttpPut]
        [Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtToken.AuthSchemes)]
        public async Task<ActionResult<Car>> Put([FromBody] Car car)
        {
            try
            {
                await _carService.EditAsync(car);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during editing car. Exception: {ex.Message}");
                return BadRequest();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = Roles.Admin, AuthenticationSchemes = JwtToken.AuthSchemes)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _carService.RemoveAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during deleting car. Exception: {ex.Message}");
                return BadRequest();
            }
        }
    }
}
