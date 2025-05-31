using Task3.Models;

namespace Task3.Repositories
{
    public interface IPizzaRepository
    {
        List<PizzaModel> GetAllPizzas();

        PizzaModel GetPizzaById(int id);

        Task CreatePizza(PizzaModel pizza);

        Task EditPizza(PizzaModel model);

        Task<List<Size>> GetAvaiableSizes();

        Task<List<DoughType>> GetAvaiableDoughTypes();

        Task DeletePizza(PizzaModel pizza);
    }
}
