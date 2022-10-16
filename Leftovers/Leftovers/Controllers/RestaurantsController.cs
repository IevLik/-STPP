using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Leftovers.Data.Repositories;
using Leftovers.Data.Entities;
using Leftovers.Data.Dtos.Restaurants;
using Leftovers.Data.Dtos.Meals;
using AutoMapper;

namespace Leftovers.Controllers
{
    [ApiController]
    [Route("api/meals/{mealId}/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;
        private readonly IMealsRepository _mealsRepository;
        public RestaurantsController(IRestaurantsRepository restaurantsRepository, IMapper mapper, IMealsRepository mealsRepository)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
            _mealsRepository = mealsRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<RestaurantDto>> GetAllAsync(int mealId)
        {
            var restaurants = await _restaurantsRepository.GetAsync(mealId);
            return restaurants.Select(o => _mapper.Map<RestaurantDto>(o));
        }
        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<RestaurantDto>> GetAsync(int mealId, int restaurantId)
        {
            var restaurant = await _restaurantsRepository.GetAsync(mealId, restaurantId);
            if (restaurant == null) return NotFound($"Restaurant with id '{restaurantId}' not found");
            return Ok(_mapper.Map<RestaurantDto>(restaurant));
        }

        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> PostAsync(int mealId, CreateRestaurantDto restaurantDto)
        {
            var meal = await _mealsRepository.GetAsync(mealId);
            if (meal == null) return NotFound($"Restaurant with id '{mealId}' not found");
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);
            restaurant.MealId = mealId;
            await _restaurantsRepository.InsertAsync(restaurant);
            return Created($"/api/meals/{mealId}/restaurants/{restaurant.Id}", _mapper.Map<RestaurantDto>(restaurant));
        }

        [HttpPut("{restaurantId}")]
        public async Task<ActionResult<RestaurantDto>> PutAsync(int mealId, int restaurantId, UpdateRestaurantDto restaurantDto)
        {
            var meal = await _mealsRepository.GetAsync(mealId);
            if (meal == null) return NotFound($"Restaurant with id '{mealId}' not found");
            var oldRestaurant = await _restaurantsRepository.GetAsync(mealId, restaurantId);
            if (oldRestaurant == null) return NotFound($"Restaurant with id '{restaurantId}' not found");
            
            // oldRestaurant.Description = restaurantDto.Description;
            _mapper.Map(restaurantDto, oldRestaurant);
            await _restaurantsRepository.UpdateAsync(oldRestaurant);
            return Ok(_mapper.Map<RestaurantDto>(oldRestaurant));
        }

        [HttpDelete("{restaurantId}")]
        public async Task<ActionResult> DeleteAsync(int mealId, int restaurantId)
        {
            var restaurant = await _restaurantsRepository.GetAsync(mealId, restaurantId);
            if (restaurant == null) return NotFound($"Restaurant with id '{restaurantId}' not found");
            await _restaurantsRepository.DeleteAsync(restaurant);
            //204
            return NoContent();
        }
    }
}
