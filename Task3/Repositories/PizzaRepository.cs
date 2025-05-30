using Microsoft.EntityFrameworkCore;
using Task3.Models;

namespace Task3.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        public List<PizzaModel> GetAllPizzas()
        {
            using (PizzasStoreContext db = new PizzasStoreContext())
            {
                var pizzasFromDb = db.Pizzas.Include("Sizes").Include("DoughTypes");
                var pizzas = pizzasFromDb.Select(p => new PizzaModel(p)).ToList();
                return pizzas;
            }
        }

        public PizzaModel GetPizzaById(int id)
        {
            using (PizzasStoreContext db = new PizzasStoreContext())
            {
                var foundedPizzaFromDb = db.Pizzas.Include("Sizes").Include("DoughTypes").FirstOrDefault(p => p.Id == id);
                if (foundedPizzaFromDb != null)
                {
                    var foundedPizza = new PizzaModel(foundedPizzaFromDb);
                    return foundedPizza;
                }
                else
                {
                    return null;
                }
            }
        }

        public void CreatePizza(PizzaModel pizza)
        {
            using (PizzasStoreContext db = new PizzasStoreContext())
            {
                db.Add(pizza);
            }
        }
    }
}
