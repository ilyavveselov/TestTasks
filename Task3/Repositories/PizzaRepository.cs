using Task3.Models;

namespace Task3.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private List<PizzaModel> _pizzas = new List<PizzaModel>
        {
            new() {
                Id = 1,
                Name = "Capriccio",
                Image = "/images/pizzas/capriccio_1.webp",
                Description = " Сыр моцарелла, Соус \"Барбекю\", Соус \"Кальяри\", Пепперони, Овощи гриль, Бекон, Ветчина"
                + " Томаты черри, Шампиньоны",
                Sizes = new List<int>{ 40, 60 },
                Types = new List<string>{ "Традиционное", "Толстое" },
                Price = 500,
                Weight = 800,
                ShowHalf = true,
                CanHalf = false,
                IsHit = true
            },
            new() {
                Id = 2,
                Name = "XXXL",
                Image = "/images/pizzas/xxxl_1.webp",
                Description = "Сыр моцарелла, Соус \"1000 островов\", Куриный рулет, Ветчина, Колбаски охотничьи, Бекон,"
                + " Сервелат, Огурцы маринованные, Томаты черри, Маслины, Лук маринованный",
                Sizes = new List<int>{ 30, 40 },
                Types = new List<string>{ "Традиционное", "Толстое" },
                Price = 880,
                Weight = 1440,
                ShowHalf = true,
                CanHalf = true,
                IsHit = false
            },
            new() {
                Id = 3,
                Name = "4 вкуса",
                Image = "/images/pizzas/4_vkusa_1.webp",
                Description = "Соус \"1000 островов\", Сыр моцарелла, Рулет куриный, Ветчина, Пепперони, Сыр пармезан, "
                + "Шампиньоны, Томаты свежие, Маслины/оливки",
                Sizes = new List<int>{ 30, 40, 60 },
                Types = new List<string>{ "Традиционное", "Толстое" },
                Price = 540,
                Weight = 540,
                ShowHalf = false,
                CanHalf = false,
                IsHit = false
            },
            new() {
                Id = 4,
                Name = "Амазонка",
                Image = "/images/pizzas/amazonka_1.webp",
                Description = "Соус \"Томатный\", Сыр моцарелла, Куриная грудка, Брокколи, Огурцы маринованные,"
                + " Перец болгарский, Шампиньоны, Томаты черри, Маслины, Лук маринованный",
                Sizes = new List<int>{ 30, 40, 60 },
                Types = new List<string>{ "Традиционное", "Толстое" },
                Price = 550,
                Weight = 600,
                ShowHalf = true,
                CanHalf = false,
                IsHit = false
            },
            new() {
                Id = 5,
                Name = "БананZZа",
                Image = "/images/pizzas/bananzza_1.webp",
                Description = "Соус \"1000 островов\", Сыр моцарелла, Рулет куриный, Ветчина, Пепперони, Сыр пармезан, "
                + "Шампиньоны, Томаты свежие, Маслины/оливки",
                Sizes = new List<int>{ 30, 40,  60 },
                Price = 530,
                Weight = 520,
                ShowHalf = true,
                CanHalf = false,
                IsHit = false
            },
            new() {
                Id = 6,
                Name = "Барбекю",
                Image = "/images/pizzas/barbeq_1.webp",
                Description = "Соус \"Томатный\", Сыр моцарелла, Ветчина, Бекон, Пепперони, Соус \"Барбекю\", Томаты, "
                + "Перец болгарский, Лук маринованный",
                Sizes = new List<int>{ 30, 40, 60 },
                Types = new List<string>{ "Традиционное", "Толстое" },
                Price = 570,
                Weight = 590,
                ShowHalf = true,
                CanHalf = true,
                IsHit = false
            },
            new() {
                Id = 7,
                Name = "Гавайская",
                Image = "/images/pizzas/hawai_1.webp",
                Description = "Ветчина, Соус \"Гавайский\", Сыр моцарелла, Ананас, Перец болгарский",
                Sizes = new List<int>{ 30, 40, 60 },
                Types = new List<string>{ "Традиционное", "Толстое" },
                Price = 530,
                Weight = 550,
                ShowHalf = true,
                CanHalf = false,
                IsHit = false
            },
            new() {
                Id = 8,
                Name = "Гавайская Premium",
                Image = "/images/pizzas/hawai_premium_1.webp",
                Description =  "Соус \"Гавайский\", Сыр моцарелла, Ананас, Ветчина, Куриный рулет, Кукуруза, Перец болгарский",
                Sizes = new List<int>{ 30, 40, 60 },
                Types = new List<string>{ "Традиционное", "Толстое" },
                Price = 550,
                Weight = 590,
                ShowHalf = true,
                CanHalf = false,
                IsHit = false
            }
        };

        public List<PizzaModel> GetAllPizzas()
        {
            return _pizzas;
        }

        public PizzaModel GetPizzaById(int id)
        {
            var foundedPizza = _pizzas.FirstOrDefault(p => p.Id == id);
            return foundedPizza;
        }

        public void CreatePizza(PizzaModel pizza)
        {
            _pizzas.Add(pizza);
        }
    }
}
