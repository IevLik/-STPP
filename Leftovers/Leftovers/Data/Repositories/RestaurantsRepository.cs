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
        Task<List<Restaurant>> GetAsync(int chainId);
        Task<Restaurant> GetAsync(int chainId, int restaurantId);
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
        public async Task<Restaurant> GetAsync(int chainId, int restaurantId)
        {
            return await _leftoversContext.Restaurants.FirstOrDefaultAsync(o => o.ChainId == chainId && o.Id == restaurantId);
        }
        public async Task<List<Restaurant>> GetAsync(int chainId)
        {
            return await _leftoversContext.Restaurants.Where(o => o.ChainId == chainId).ToListAsync();
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
