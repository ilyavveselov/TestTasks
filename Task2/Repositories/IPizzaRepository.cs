using Task2.Models;

namespace Task2.Repositories
{
    public interface IPizzaRepository
    {
        List<PizzaModel> GetAllPizzas();
    }
}
