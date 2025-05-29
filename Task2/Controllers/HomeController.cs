using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task2.Models;
using Task2.Repositories;

namespace Task2.Controllers
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

        public IActionResult Details(int id)
        {
            var foundedPizza = _pizzaRepository.GetPizzaById(id);
            return View(foundedPizza);
        }
        
        public IActionResult IndexNew()
        {
            var pizzas = _pizzaRepository.GetAllPizzas();
            return View(pizzas);
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
            return PartialView("PizzaPartials/_CardModal", pizza);
        }

        [HttpGet]
        public IActionResult GetPizzaByIdJSON(int id)
        {
            var pizza = _pizzaRepository.GetPizzaById(id);
            if(pizza == null) return NotFound();
            return Json(pizza);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
