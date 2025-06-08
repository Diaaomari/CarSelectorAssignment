namespace CarSelector.Infrastructure.Services.Route
{
    public static class NhtsaRoute
    {
        public const string MakesUrl = "getallmakes?format=json";
        public const string VehicleModelsUrl = "GetModelsForMakeIdYear/makeId/{0}";
        public const string VehicleTypessUrl = "GetVehicleTypesForMakeId/{0}?format=json";
    }
}
