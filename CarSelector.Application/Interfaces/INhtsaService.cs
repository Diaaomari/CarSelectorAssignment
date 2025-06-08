using CarSelector.Application.DTOs;

namespace CarSelector.Application.Interfaces
{
    public interface INhtsaService
    {
        public Task<List<MakeDto>> GetMakesAsync();
        public Task<List<VehicleTypeDTO>> GetVehicleTypesByMakeAsync(string makeId);
        public Task<List<ModelDto>> GetVehicleModelsByMakeAndYearAsync(string? makeId, string? vehicleType, int? year);
    }
}
