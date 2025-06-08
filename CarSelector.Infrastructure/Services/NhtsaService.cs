using CarSelector.Application.DTOs;
using CarSelector.Application.Interfaces;
using CarSelector.Infrastructure.Services.Models;
using CarSelector.Infrastructure.Services.Route;

namespace CarSelector.Infrastructure.Services
{
    public class NhtsaService : INhtsaService
    {
        private readonly HttpClient _httpClient;

        public NhtsaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MakeDto>> GetMakesAsync()
        {
            var response = await _httpClient.GetAsync(NhtsaRoute.MakesUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var makesResponse = System.Text.Json.JsonSerializer.Deserialize<NhtsaResponse>(content);
            return makesResponse?.Results
                .Select(r => new MakeDto { Id = r.Make_ID, Name = r.Make_Name })
                .ToList() ?? [];
        }

        public async Task<List<ModelDto>> GetVehicleModelsByMakeAndYearAsync(string? makeId, string? vehicleType, int? year)
        {
            var route = BuildVehicleModelsRoute(makeId, vehicleType, year);
            var response = await _httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var vehicleTypesResponse = System.Text.Json.JsonSerializer.Deserialize<NhtsaResponse>(content);
            return vehicleTypesResponse?.Results
                .Select(r => new ModelDto { ModelName = r.Model_Name, MakeName = r.Make_Name })
            .ToList() ?? [];
        }

        public async Task<List<VehicleTypeDTO>> GetVehicleTypesByMakeAsync(string makeId)
        {
            var route = string.Format(NhtsaRoute.VehicleTypessUrl, makeId);

            var response = await _httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var vehicleTypesResponse = System.Text.Json.JsonSerializer.Deserialize<NhtsaResponse>(content);
            return vehicleTypesResponse?.Results
                .Select(r => new VehicleTypeDTO { Name = r.VehicleTypeName })
                .ToList() ?? [];
        }
        private static string BuildVehicleModelsRoute(string? makeId, string? vehicleType, int? year)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendFormat(NhtsaRoute.VehicleModelsUrl, makeId);

            if (year.HasValue)
                sb.Append($"/modelyear/{year.Value}");

            if (!string.IsNullOrEmpty(vehicleType))
                sb.Append($"/vehicletype/{vehicleType}");

            sb.Append("?format=json");
            return sb.ToString();
        }

    }
}
