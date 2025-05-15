using System.ComponentModel.DataAnnotations;

namespace AECPrototype.ViewModel
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Please enter the product title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the product's category.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please select a date.")]
        public DateOnly Date { get; set; }
    }
}
