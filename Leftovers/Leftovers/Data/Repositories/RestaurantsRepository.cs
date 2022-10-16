using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Leftovers.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leftovers.Data.Repositories
{
    public interface IRestaurantsRepository
    {
        Task DeleteAsync(Restaurant restaurant);
        Task<List<Restaurant>> GetAsync(int mealId);
        Task<Restaurant> GetAsync(int mealId, int restaurantId);
        Task InsertAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
    }

    public class RestaurantsRepository : IRestaurantsRepository
    {
        private readonly LeftoversContext _leftoversContext;
        public RestaurantsRepository(LeftoversContext leftoversContext)
        {
            _leftoversContext = leftoversContext;
        }
        public async Task<Restaurant> GetAsync(int mealId, int restaurantId)
        {
            return await _leftoversContext.Restaurants.FirstOrDefaultAsync(o => o.MealId == mealId && o.Id == restaurantId);
        }
        public async Task<List<Restaurant>> GetAsync(int mealId)
        {
            return await _leftoversContext.Restaurants.Where(o => o.MealId == mealId).ToListAsync();
        }

        public async Task InsertAsync(Restaurant restaurant)
        {
            _leftoversContext.Restaurants.Add(restaurant);
            await _leftoversContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Restaurant restaurant)
        {
            _leftoversContext.Restaurants.Update(restaurant);
            await _leftoversContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Restaurant restaurant)
        {
            _leftoversContext.Restaurants.Remove(restaurant);
            await _leftoversContext.SaveChangesAsync();
        }
    }
}
