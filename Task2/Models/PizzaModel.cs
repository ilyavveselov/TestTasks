using System.ComponentModel.DataAnnotations;

namespace Task2.Models
{
    public class PizzaModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
        public List<int> Sizes { get; set; } = new();
        public List<string> Types { get; set; } = new();

        [Required]
        public int Price { get; set; }
        public int Weight { get; set; }
        public bool ShowHalf { get; set; }
        public bool CanHalf { get; set; }
        public bool IsHit { get; set; }
    }
}
