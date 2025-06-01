using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IActionResult> GetAvaiableSizes()
        {
            var sizes = await _pizzaRepository.GetAvaiableSizes();
            return Ok(sizes.Select(s => s.Value));
        }
        [HttpGet]
        public async Task<IActionResult> GetAvaiableTypes()
        {
            var types = await _pizzaRepository.GetAvaiableDoughTypes();
            return Ok(types.Select(s => s.Name));
        }

        public async Task<IActionResult> Index()
        {
            var pizzas = await _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> IndexNew()
        {
            var pizzas = await _pizzaRepository.GetAllPizzas();
            return View(pizzas);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pizza = await _pizzaRepository.GetPizzaById(id);
            return View(pizza);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pizzas = await _pizzaRepository.GetAllPizzas();
            return Json(pizzas);
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzaById(int id)
        {
            var pizza = await _pizzaRepository.GetPizzaById(id);
            if (pizza == null)
            {
                _logger.LogError("Пицца не найдена");
                return NotFound();
            }
            return PartialView("PizzaPartials/_CardModal", pizza);
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzaByIdJSON(int id)
        {
            var pizza = await _pizzaRepository.GetPizzaById(id);
            if (pizza == null)
            {
                _logger.LogError("Пицца не найдена");
                return NotFound();
            }
            return Json(pizza);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.AllSizes = await _pizzaRepository.GetAvaiableSizes();
            ViewBag.AllDoughTypes = await _pizzaRepository.GetAvaiableDoughTypes();
            return PartialView("PizzaPartials/_Form", new PizzaModel(new Pizza()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PizzaModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        _logger.LogError($"Ошибка валидации поля {entry.Key}: {error.ErrorMessage}");
                    }
                }
                ViewBag.AllSizes = await _pizzaRepository.GetAvaiableSizes();
                ViewBag.AllDoughTypes = await _pizzaRepository.GetAvaiableDoughTypes();
                return PartialView("PizzaPartials/_Form", model);
            }

            await _pizzaRepository.CreatePizza(model);
            return Json(new { redirect = string.IsNullOrEmpty(returnUrl) ? Url.Action("Index") : returnUrl });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sizes = await _pizzaRepository.GetAvaiableSizes();
            var types = await _pizzaRepository.GetAvaiableDoughTypes();
            ViewBag.AllSizes = sizes;
            ViewBag.AllDoughTypes = types;
            using (var db = new PizzasStoreContext())
            {
                var pizza = db.Pizzas.Include("Sizes").Include("DoughTypes").FirstOrDefault(p => p.Id == id);
                if (pizza == null)
                    return NotFound();

                return PartialView("PizzaPartials/_Form", new PizzaModel(pizza));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PizzaModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    foreach (var error in entry.Value.Errors)
                    {
                        _logger.LogError($"Ошибка валидации поля {entry.Key}: {error.ErrorMessage}");
                    }
                }

                ViewBag.AllSizes = await _pizzaRepository.GetAvaiableSizes();
                ViewBag.AllDoughTypes = await _pizzaRepository.GetAvaiableDoughTypes();

                return PartialView("PizzaPartials/_Form", model);
            }

            await _pizzaRepository.EditPizza(model);
            return Json(new { redirect = Url.Action("Index") });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PizzaModel pizza)
        {
            await _pizzaRepository.DeletePizza(pizza);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
