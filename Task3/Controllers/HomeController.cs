using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task3.Models;
using Task3.Repositories;

namespace Task3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPizzaRepository _pizzaRepository;
        public HomeController(ILogger<HomeController> logger, IPizzaRepository pizzaRepository)
        {
            _logger = logger;
            _pizzaRepository = pizzaRepository;
        }

        public IActionResult Index()
        {
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult IndexNew()
        {
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }

        public IActionResult Details(int id)
        {
            var pizza = _pizzaRepository.GetPizzaById(id);
            return View(pizza);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var pizzas = _pizzaRepository.GetAllPizzas();
            return Json(pizzas);
        }

        [HttpGet]
        public IActionResult GetPizzaById(int id)
        {
            var pizza = _pizzaRepository.GetPizzaById(id);
            if (pizza == null)
            {
                _logger.LogError("Пицца не найдена");
                return NotFound();
            }
            return PartialView("PizzaPartials/_CardModal", pizza);
        }

        [HttpGet]
        public IActionResult GetPizzaByIdJSON(int id)
        {
            var pizza = _pizzaRepository.GetPizzaById(id);
            if (pizza == null)
            {
                _logger.LogError("Пицца не найдена");
                return NotFound();
            }
            return Json(pizza);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind(include: "Name, Image, Description, Sizes, Types, Price, Weight")] PizzaModel pizza)
        {
            if (ModelState.IsValid)
            {
                _pizzaRepository.CreatePizza(pizza);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        _logger.LogError($"Ошибка валидации поля {entry.Key}: {error.ErrorMessage}");
                    }
                }
                return BadRequest();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
