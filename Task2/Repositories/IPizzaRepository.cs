using Task2.Models;

namespace Task2.Repositories
{
    public interface IPizzaRepository
    {
        List<PizzaModel> GetAllPizzas();

        PizzaModel GetPizzaById(int id);

        void CreatePizza(PizzaModel pizza);
    }
}
