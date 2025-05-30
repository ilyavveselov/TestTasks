using Task3.Models;

namespace Task3.Repositories
{
    public interface IPizzaRepository
    {
        List<PizzaModel> GetAllPizzas();

        PizzaModel GetPizzaById(int id);

        void CreatePizza(PizzaModel pizza);
    }
}
