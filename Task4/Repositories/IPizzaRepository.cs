using Task4.Models;

namespace Task4.Repositories
{
    public interface IPizzaRepository
    {
        Task<List<PizzaModel>> GetAllPizzas();

        Task<PizzaModel> GetPizzaById(int id);

        Task CreatePizza(PizzaModel pizza);

        Task EditPizza(PizzaModel model);

        Task<List<Size>> GetAvaiableSizes();

        Task<List<DoughType>> GetAvaiableDoughTypes();

        Task DeletePizza(PizzaModel pizza);
    }
}
