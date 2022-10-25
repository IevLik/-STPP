using Leftovers.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leftovers.Data.Repositories
{
    public interface IMealsRepository
    {
        Task<List<Meal>> GetAsync(int restaurantId);
        Task<Meal> GetAsync(int restaurantId, int mealId);
        Task InsertAsync(Meal meal);
        Task UpdateAsync(Meal meal);
        Task DeleteAsync(Meal meal);
    }

    public class MealsRepository : IMealsRepository
    {
        private readonly LeftoversContext _leftoversContext;
        public MealsRepository(LeftoversContext leftoversContext)
        {
            _leftoversContext = leftoversContext;
        }
        public async Task<Meal> GetAsync(int restaurantId, int mealId)
        {
            return await _leftoversContext.Meals.FirstOrDefaultAsync(o => o.RestaurantId == restaurantId && o.Id == mealId);
        }

        public async Task<List<Meal>> GetAsync(int restaurantId)
        {
            return await _leftoversContext.Meals.Where(o => o.RestaurantId == restaurantId).ToListAsync();
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
