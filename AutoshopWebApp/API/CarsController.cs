using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using AutoshopWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carService;
        IAuthorizationService _authorizationService;

        public CarsController(ICarService carService, IAuthorizationService authorizationService)
        {
            _carService = carService;
            _authorizationService = authorizationService;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<IEnumerable<Car>> GetCars([FromQuery] string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                return await _carService.ReadAllAsync();
            }
            else
            {
                return await _carService.ReadAllAsync(search);
            }
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var car = await _carService.ReadAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, car, Operations.Details);

            if(!isAuthorized.Succeeded)
            {
                return Unauthorized();
            }

            return Ok(car);
        }

        // PUT: api/Cars/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar([FromRoute] int id, [FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.CarId)
            {
                return BadRequest();
            }


            var isAuthorized = await _authorizationService
               .AuthorizeAsync(User, car, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return Unauthorized();
            }

            try
            {
                await _carService.UpdateAsync(car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _carService.IsExistAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cars
        [HttpPost]
        public async Task<IActionResult> PostCar([FromBody] Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAuthorized = await _authorizationService
               .AuthorizeAsync(User, car, Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Unauthorized();
            }

            await _carService.CreateAsync(car);

            return CreatedAtAction("GetCar", new { id = car.CarId }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAuthorized = await _authorizationService
               .AuthorizeAsync(User, new Car { CarId = id }, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return Unauthorized();
            }

            var result = await _carService.DeleteAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}