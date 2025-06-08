using CarSelector.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarSelectorAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly INhtsaService _nhtsaService;

        public VehiclesController(INhtsaService nhtsaService)
        {
            _nhtsaService = nhtsaService;
        }

        [HttpGet("make")]
        public async Task<IActionResult> MakeAsync()
        {
            return Ok(await _nhtsaService.GetMakesAsync());
        }
        [HttpGet("vehicle-types/{makeId}")]
        public async Task<IActionResult> GetVehicleTypes(string makeId) => Ok(await _nhtsaService.GetVehicleTypesByMakeAsync(makeId));

        [HttpGet("models/{makeId}")]
        public async Task<IActionResult> GetModels(string makeId, [FromQuery] string? vehicleType, [FromQuery] int? year)
        {

            if (!year.HasValue && string.IsNullOrEmpty(vehicleType))
            {
                return BadRequest("Either year or vehicleType must be provided.");
            }
            return Ok(await _nhtsaService.GetVehicleModelsByMakeAndYearAsync(makeId, vehicleType, year));


        }


    }
}
