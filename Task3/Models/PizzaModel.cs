using System.ComponentModel.DataAnnotations;

namespace Task3.Models
{
    public class PizzaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Пицца обязательно должна иметь название")]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пицца обязательно должна иметь описание")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Укажите размеры")]
        [MinLength(1, ErrorMessage = "Должен быть хотя бы один размер")]
        public List<int> Sizes { get; set; } = new();
        public List<string> Types { get; set; } = new();

        [Required(ErrorMessage = "Пицца обязательно должна иметь цену")]
        [Range(100, int.MaxValue, ErrorMessage = "Цена должна быть от 100 руб.")]
        public int Price { get; set; }

        public int Weight { get; set; } = 100;
        public bool ShowHalf { get; set; } = false;
        public bool CanHalf { get; set; } = false;
        public bool IsHit { get; set; } = false;

        public PizzaModel(Pizza pizza)
        {
            Id = pizza.Id;
            Name = pizza.Name;
            Image = pizza.Image;
            Description = pizza.Description;
            Sizes = pizza.Sizes.Select(s => s.Value).ToList();
            Types = pizza.DoughTypes.Select(s => s.Name).ToList();
            Price = pizza.Price;
            Weight = pizza.Weight;
            ShowHalf = pizza.ShowHalf;
            CanHalf = pizza.CanHalf;
            IsHit = pizza.IsHit;
        }
    }
}
