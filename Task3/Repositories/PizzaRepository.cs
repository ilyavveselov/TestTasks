using Microsoft.AspNetCore.Mvc;
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
        public async Task EditPizza(PizzaModel model)
        {
            if (model == null) return;

            using (var db = new PizzasStoreContext())
            {
                var pizzaFromDb = await db.Pizzas
                    .Include(p => p.Sizes)
                    .Include(p => p.DoughTypes)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);

                if (pizzaFromDb != null)
                {
                    pizzaFromDb.Name = model.Name;
                    pizzaFromDb.Image = model.Image;
                    pizzaFromDb.Description = model.Description;
                    pizzaFromDb.Price = model.Price;
                    pizzaFromDb.Weight = model.Weight;
                    pizzaFromDb.CanHalf = model.CanHalf;
                    pizzaFromDb.ShowHalf = model.ShowHalf;
                    pizzaFromDb.IsHit = model.IsHit;

                    pizzaFromDb.Sizes.Clear();
                    pizzaFromDb.DoughTypes.Clear();

                    if (model.Sizes != null && model.Sizes.Any())
                    {
                        var sizesFromDb = await db.Sizes
                            .Where(s => model.Sizes.Contains(s.Value))
                            .ToListAsync();

                        foreach (var size in sizesFromDb)
                        {
                            pizzaFromDb.Sizes.Add(size);
                        }
                    }

                    if (model.Types != null && model.Types.Any())
                    {
                        var doughsFromDb = await db.DoughTypes
                            .Where(d => model.Types.Contains(d.Name))
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



    }
}
