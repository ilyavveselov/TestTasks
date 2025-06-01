using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task3.Models;

namespace Task3.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        public async Task<List<PizzaModel>> GetAllPizzas()
        {
            using (PizzasStoreContext db = new PizzasStoreContext())
            {
                var pizzasFromDb = await db.Pizzas.Include("Sizes").Include("DoughTypes").ToListAsync(); ;
                var pizzas = pizzasFromDb.Select(p => new PizzaModel(p)).ToList();
                return pizzas;
            }
        }

        public async Task<List<Size>> GetAvaiableSizes()
        {
            using (PizzasStoreContext db = new PizzasStoreContext())
            {
                var sizes = await db.Sizes.Include(s => s.Pizzas).OrderBy(s => s.Value).ToListAsync();
                return sizes;
            }
        }
        public async Task<List<DoughType>> GetAvaiableDoughTypes()
        {
            using (PizzasStoreContext db = new PizzasStoreContext())
            {
                var types = await db.DoughTypes.Include(s => s.Pizzas).OrderBy(t => t.Name).ToListAsync();
                return types;
            }
        }
        public async Task<PizzaModel> GetPizzaById(int id)
        {
            using (PizzasStoreContext db = new PizzasStoreContext())
            {
                var foundedPizzaFromDb = await db.Pizzas.Include("Sizes").Include("DoughTypes").FirstOrDefaultAsync(p => p.Id == id);
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

        public async Task CreatePizza(PizzaModel model)
        {
            using (var db = new PizzasStoreContext())
            {
                var sizes = await db.Sizes
                    .Where(s => model.Sizes.Contains(s.Value))
                    .ToListAsync();

                var doughTypes = await db.DoughTypes
                    .Where(d => model.Types.Contains(d.Name))
                    .ToListAsync();

                var pizza = new Pizza
                {
                    Name = model.Name,
                    Image = model.Image,
                    Description = model.Description,
                    Price = model.Price,
                    Weight = model.Weight,
                    ShowHalf = model.ShowHalf,
                    CanHalf = model.CanHalf,
                    IsHit = model.IsHit,
                    Sizes = sizes,
                    DoughTypes = doughTypes
                };

                db.Pizzas.Add(pizza);
                await db.SaveChangesAsync();
            }
        }
        public async Task EditPizza(PizzaModel pizza)
        {
            if (pizza == null) return;

            using (var db = new PizzasStoreContext())
            {
                var pizzaFromDb = await db.Pizzas
                    .Include(p => p.Sizes)
                    .Include(p => p.DoughTypes)
                    .FirstOrDefaultAsync(p => p.Id == pizza.Id);

                if (pizzaFromDb != null)
                {
                    pizzaFromDb.Name = pizza.Name;
                    pizzaFromDb.Image = pizza.Image;
                    pizzaFromDb.Description = pizza.Description;
                    pizzaFromDb.Price = pizza.Price;
                    pizzaFromDb.Weight = pizza.Weight;
                    pizzaFromDb.CanHalf = pizza.CanHalf;
                    pizzaFromDb.ShowHalf = pizza.ShowHalf;
                    pizzaFromDb.IsHit = pizza.IsHit;

                    pizzaFromDb.Sizes.Clear();
                    pizzaFromDb.DoughTypes.Clear();

                    if (pizza.Sizes != null && pizza.Sizes.Any())
                    {
                        var sizesFromDb = await db.Sizes
                            .Where(s => pizza.Sizes.Contains(s.Value))
                            .ToListAsync();

                        foreach (var size in sizesFromDb)
                        {
                            pizzaFromDb.Sizes.Add(size);
                        }
                    }

                    if (pizza.Types != null && pizza.Types.Any())
                    {
                        var doughsFromDb = await db.DoughTypes
                            .Where(d => pizza.Types.Contains(d.Name))
                            .ToListAsync();

                        foreach (var dough in doughsFromDb)
                        {
                            pizzaFromDb.DoughTypes.Add(dough);
                        }
                    }

                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task DeletePizza(PizzaModel pizza)
        {
            using (var db = new PizzasStoreContext())
            {
                var pizzaToDelete = await db.Pizzas
                    .Include("Sizes")
                    .Include("DoughTypes")
                    .FirstOrDefaultAsync(p => p.Id == pizza.Id);
                if (pizzaToDelete != null)
                {
                    var sizesToDelete = await db.Sizes
                             .Where(s => pizza.Sizes.Contains(s.Value))
                             .ToListAsync();
                    var typesToDelete = await db.DoughTypes
                             .Where(t => pizza.Types.Contains(t.Name))
                             .ToListAsync();
                    pizzaToDelete.Sizes.Clear();
                    pizzaToDelete.DoughTypes.Clear();

                    db.Pizzas.Remove(pizzaToDelete);
                    await db.SaveChangesAsync();
                }
            }
        }

    }
}
