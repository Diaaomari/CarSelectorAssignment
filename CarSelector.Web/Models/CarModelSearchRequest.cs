namespace CarSelector.Web.Models
{
    public class CarModelSearchRequest
    {
        public int SelectedMake { get; set; }
        public int? SelectedYear { get; set; }
        public string? SelectedType { get; set; }
    }
}
