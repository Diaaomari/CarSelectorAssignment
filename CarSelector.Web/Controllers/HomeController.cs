using CarSelector.Application.DTOs;
using CarSelector.Web.Models;
using CarSelector.Web.Route;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarSelector.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory _httpClientFactory)
        {
            _logger = logger;
            this._httpClientFactory = _httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FindModels(CarModelSearchRequest model)
        {
            try
            {
                if (model.SelectedMake <= 0)
                {
                    return Json(new { success = false, message = "Make ID is required." });
                }
                if (model.SelectedYear <= 0 && string.IsNullOrEmpty(model.SelectedType))
                {
                    return Json(new { success = false, message = "Please select at least a year or a vehicle type." });
                }
                var route = string.Format(VehiclesRoute.modelsUrl, model.SelectedMake, model.SelectedType, model.SelectedYear);
                var result = await HttpClient.GetAsync(route);
                if (!result.IsSuccessStatusCode)
                {
                    return Json(new { success = false, message = "Sorry, we couldn't load car models at the moment. Please try again later." });
                }
                var models = await result.Content.ReadFromJsonAsync<List<ModelDto>>();
                return Json(new { success = true, data = models?.Select(x => x.ModelName).ToList() ?? [] });

            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An unexpected error occurred while contacting the server. Please try again later." });
            }
        }

        public async Task<JsonResult> GetVehicleTypes(int makeId)
        {
            if (makeId <= 0)
            {
                return Json(new { success = false, message = "Make ID is required." });
            }
            var route = string.Format(VehiclesRoute.vehicletypesUrl, makeId);
            var result = await HttpClient.GetAsync(route);
            if (!result.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = "" });
            }
            var makes = await result.Content.ReadFromJsonAsync<List<CarSelector.Application.DTOs.VehicleTypeDTO>>();
            var typesName = makes?.Select(x => x.Name).ToList() ?? new List<string>();
            return Json(new { success = true, data = typesName });
        }

        public async Task<JsonResult> GetMakes()
        {
            try
            {
                var result = await HttpClient.GetAsync(VehiclesRoute.makesUrl);
                if (!result.IsSuccessStatusCode)
                {
                    return Json(new { success = false, message = "Sorry, we couldn't load makes at the moment. Please try again later." });
                }
                var makes = await result.Content.ReadFromJsonAsync<List<Application.DTOs.MakeDto>>();
                return Json(new { success = true, data = makes ?? [] });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An unexpected error occurred while contacting the server. Please try again later." });

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private System.Net.Http.HttpClient HttpClient
        {
            get
            {
                var httpClientClone = this._httpClientFactory.CreateClient("CarApi");
                return httpClientClone;

            }
        }

    }
}
