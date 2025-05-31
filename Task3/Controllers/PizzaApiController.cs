using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task3.Models;
using Task3.Repositories;

[ApiController]
[Route("api/pizza")]
public class PizzaApiController : Controller
{
    private readonly IPizzaRepository _pizzaRepository;
    private readonly ILogger<PizzaApiController> _logger;
    public PizzaApiController(ILogger<PizzaApiController> logger, IPizzaRepository pizzaRepository)
    {
        _logger = logger;
        _pizzaRepository = pizzaRepository;
    }

    [HttpGet("sizes")]
    public async Task<IActionResult> GetAvaiableSizes()
    {
        var sizes = await _pizzaRepository.GetAvaiableSizes();
        return Ok(sizes.Select(s => s.Value));
    }
    [HttpGet("types")]
    public async Task<IActionResult> GetAvaiableTypes()
    {
        var types = await _pizzaRepository.GetAvaiableDoughTypes();
        return Ok(types.Select(s => s.Name));
    }

    [HttpGet("getPizzaById")]
    public IActionResult GetPizzaById(int id)
    {
        var pizza = _pizzaRepository.GetPizzaById(id);
        if (pizza == null)
        {
            _logger.LogError("Пицца не найдена");
            return NotFound();
        }
        return Json(pizza);
    }

    [HttpGet("getAllPizzas")]
    public List<PizzaModel> GetAllPizzas()
    {
        using (PizzasStoreContext db = new PizzasStoreContext())
        {
            var pizzasFromDb = db.Pizzas.Include("Sizes").Include("DoughTypes");
            var pizzas = pizzasFromDb.Select(p => new PizzaModel(p)).ToList();
            return pizzas;
        }
    }
}