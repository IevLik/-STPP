using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Leftovers.Data.Repositories;
using Leftovers.Data.Entities;
using Leftovers.Data.Dtos.Restaurants;
using Microsoft.AspNetCore.Authorization;
using Leftovers.Data.Dtos.Meals;
using AutoMapper;
using Leftovers.Auth.Model;
using System.Security.Claims;

namespace Leftovers.Controllers
{
    [ApiController]
    [Route("api/chain/{chainId}/restaurant")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IMapper _mapper;
        private readonly IChainsRepository _chainsRepository;
        private readonly IAuthorizationService _authorizationService;
        public RestaurantsController(IRestaurantsRepository restaurantsRepository, IMapper mapper, IChainsRepository chainsRepository, IAuthorizationService authorizationService)
        {
            _restaurantsRepository = restaurantsRepository;
            _mapper = mapper;
            _chainsRepository = chainsRepository;
            _authorizationService = authorizationService;
        }
        [HttpGet]

        public async Task<IEnumerable<RestaurantDto>> GetAllAsync(int chainId)
        {
            var restaurants = await _restaurantsRepository.GetAsync(chainId);
            return restaurants.Select(o => _mapper.Map<RestaurantDto>(o));
        }
        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<RestaurantDto>> GetAsync(int chainId, int restaurantId)
        {
            var restaurant = await _restaurantsRepository.GetAsync(chainId, restaurantId);
            if (restaurant == null) return NotFound($"Restaurant with id '{restaurantId}' not found");
            return Ok(_mapper.Map<RestaurantDto>(restaurant));
        }

        [HttpPost]
        [Authorize(Roles = LeftoversUserRoles.RestaurantUser)]
        public async Task<ActionResult<RestaurantDto>> PostAsync(int chainId, CreateRestaurantDto restaurantDto)
        {
            var meal = await _chainsRepository.GetAsync(chainId);
            if (meal == null) return NotFound($"Restaurant with id '{chainId}' not found");
            var restaurant = _mapper.Map<Restaurant>(restaurantDto);
            restaurant.ChainId = chainId;

            restaurant.UserId = User.FindFirstValue(CustomClaims.UserId);////////////


            await _restaurantsRepository.InsertAsync(restaurant);
            return Created($"/api/chain/{chainId}/restaurant/{restaurant.Id}", _mapper.Map<RestaurantDto>(restaurant));
        }

        [HttpPut("{restaurantId}")]
        public async Task<ActionResult<RestaurantDto>> PutAsync(int chainId, int restaurantId, UpdateRestaurantDto restaurantDto)
        {
            var chain = await _chainsRepository.GetAsync(chainId);
            if (chain == null) return NotFound($"Restaurant with id '{chainId}' not found");
            var oldRestaurant = await _restaurantsRepository.GetAsync(chainId, restaurantId);
            if (oldRestaurant == null) return NotFound($"Restaurant with id '{restaurantId}' not found");
            
            // oldRestaurant.Description = restaurantDto.Description;
            _mapper.Map(restaurantDto, oldRestaurant);
            await _restaurantsRepository.UpdateAsync(oldRestaurant);
            return Ok(_mapper.Map<RestaurantDto>(oldRestaurant));
        }

        [HttpDelete("{restaurantId}")]
        public async Task<ActionResult> DeleteAsync(int chainId, int restaurantId)
        {
            var restaurant = await _restaurantsRepository.GetAsync(chainId, restaurantId);
            if (restaurant == null) return NotFound($"Restaurant with id '{restaurantId}' not found");
            await _restaurantsRepository.DeleteAsync(restaurant);
            //204
            return NoContent();
        }
    }
}
