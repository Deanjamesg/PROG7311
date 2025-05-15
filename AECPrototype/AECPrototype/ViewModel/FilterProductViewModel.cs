namespace AECPrototype.ViewModel
{
    public class FilterProductViewModel
    {
        public DateOnly? ToDate { get; set; }
        public DateOnly? FromDate { get; set; }
        public List<string>? Categories { get; set; }
        public string? SelectedCategory { get; set; }
        public string? SelectedFarmer { get; set; }

        public List<FarmerViewModel>? Farmers { get; set; } = new List<FarmerViewModel>();

        public List<ProductViewModel>? Products { get; set; } = new List<ProductViewModel>();
    }
}
