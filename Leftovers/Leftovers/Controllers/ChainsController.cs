using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Leftovers.Data.Repositories;
using Leftovers.Data.Entities;
using Leftovers.Data.Dtos.Chains;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Leftovers.Controllers
{
    [ApiController]
    [Route("api/meals/{mealId}/restaurant/{restaurantId}/chain")]
    public class ChainsController : ControllerBase
    {
        private readonly IChainsRepository _chainsRepository;
        private readonly IMapper _mapper;
        private readonly IMealsRepository _mealsRepository;
        private readonly IRestaurantsRepository _restaurantsRepository;
        public ChainsController(IChainsRepository chainsRepository, IMapper mapper, IRestaurantsRepository restaurantsRepository, IMealsRepository mealsRepository)
        {
            _chainsRepository = chainsRepository;
            _mapper = mapper;
            _mealsRepository = mealsRepository;
            _restaurantsRepository = restaurantsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ChainDto>> GetAllAsync(int restaurantId)
        {
            var chains = await _chainsRepository.GetAsync(restaurantId);
            return chains.Select(o => _mapper.Map<ChainDto>(o));
        }
        [HttpGet("{chainId}")]
        public async Task<ActionResult<ChainDto>> GetAsync(int restaurantId, int chainId)
        {
            var chain = await _chainsRepository.GetAsync(restaurantId, chainId);
            if (chain == null) return NotFound($"Chain with id '{chainId}' not found");
            return Ok(_mapper.Map<ChainDto>(chain));
        }
        [HttpPost]
        public async Task<ActionResult<ChainDto>> PostAsync(int mealId,int restaurantId, CreateChainDto chainDto)
        {
            var meal = await _mealsRepository.GetAsync(mealId);
            if (meal == null) return NotFound($"Couldn't find a meal with id of '{mealId}'.");

            var restaurant = await _restaurantsRepository.GetAsync(mealId, restaurantId);
            if (restaurant == null) return NotFound($"Couldn't find a restaurant with id of '{restaurantId}'.");

            var chain = _mapper.Map<Chain>(chainDto);
            chain.RestaurantId = restaurantId;
            await _chainsRepository.InsertAsync(chain);
            return Created($"api/meals/{mealId}/restaurant/{restaurantId}/chain/{chain.Id}", _mapper.Map<ChainDto>(chain));
        }
        [HttpPut("{chainId}")]
        public async Task<ActionResult<ChainDto>> PutAsync(int mealId, int restaurantId, int chainId, UpdateChainDto chainDto)
        {
            var meal = await _mealsRepository.GetAsync(mealId);
            if (meal == null) return NotFound($"Couldn't find a meal with id of '{mealId}'.");

            var restaurant = await _restaurantsRepository.GetAsync(mealId, restaurantId);
            if (restaurant == null) return NotFound($"Couldn't find a restaurant with id of '{restaurantId}'.");

            var oldChain = await _chainsRepository.GetAsync(restaurantId, chainId);
            if (oldChain == null) return NotFound($"Chain with id '{chainId}' not found");
            _mapper.Map(chainDto, oldChain);
            await _chainsRepository.UpdateAsync(oldChain);
            return Ok(_mapper.Map<ChainDto>(oldChain));
        }
        [HttpDelete("{chainId}")]
        public async Task<ActionResult<Chain>> DeleteAsync(int restaurantId, int chainId)
        {
            var chain = await _chainsRepository.GetAsync(restaurantId, chainId);
            if (chain == null) return NotFound($"Chain with id '{chainId}' not found");
            await _chainsRepository.DeleteAsync(chain);
            //204
            return NoContent();
        }
    }
}
