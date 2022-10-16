using Leftovers.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leftovers.Data.Repositories
{
    public interface IMealsRepository
    {
        Task DeleteAsync(Meal meal);
        Task<List<Meal>> GetAsync();
        Task<Meal> GetAsync(int mealId);
        Task InsertAsync(Meal meal);
        Task UpdateAsync(Meal meal);
    }

    public class MealsRepository : IMealsRepository
    {
        private readonly LeftoversContext _leftoversContext;
        public MealsRepository(LeftoversContext leftoversContext)
        {
            _leftoversContext = leftoversContext;
        }
        public async Task<List<Meal>> GetAsync()
        {
            return await _leftoversContext.Meals.ToListAsync();

        }
        public async Task<Meal> GetAsync(int mealId)
        {
            return await _leftoversContext.Meals.FirstOrDefaultAsync(o => o.Id == mealId);
        }

        public async Task InsertAsync(Meal meal)
        {
            _leftoversContext.Meals.Add(meal);
            await _leftoversContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Meal meal)
        {
            _leftoversContext.Meals.Update(meal);
            await _leftoversContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Meal meal)
        {
            _leftoversContext.Meals.Remove(meal);
            await _leftoversContext.SaveChangesAsync();
        }
    }

}
