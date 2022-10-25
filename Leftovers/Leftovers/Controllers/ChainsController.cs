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
    [Route("api/chain")]
    public class ChainsController : ControllerBase
    {
        private readonly IChainsRepository _chainsRepository;
        private readonly IMapper _mapper;
        public ChainsController(IChainsRepository chainsRepository, IMapper mapper, IRestaurantsRepository restaurantsRepository, IMealsRepository mealsRepository)
        {
            _chainsRepository = chainsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ChainDto>> GetAllAsync()
        {
            var chains = await _chainsRepository.GetAsync();
            return chains.Select(o => _mapper.Map<ChainDto>(o));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ChainDto>> GetAsync(int id)
        {
            var chain = await _chainsRepository.GetAsync(id);
            if (chain == null) return NotFound($"Chain with id '{id}' not found");
            return Ok(_mapper.Map<ChainDto>(chain));
        }
        [HttpPost]
        public async Task<ActionResult<ChainDto>> PostAsync(CreateChainDto chainDto)
        {
            var chain = _mapper.Map<Chain>(chainDto);
            await _chainsRepository.InsertAsync(chain);
            return Created($"/api/chain/{chain.Id}", _mapper.Map<ChainDto>(chain));
        }


        [HttpPut("{id}")]
        
        public async Task<ActionResult<ChainDto>> PutAsync(int id, UpdateChainDto chainDto)
        {
            var oldChain = await _chainsRepository.GetAsync(id);
            if (oldChain == null) return NotFound($"Chain with id of '{id}' not found.");

            // oldRestaurant.Description = restaurantDto.Description;
            _mapper.Map(chainDto, oldChain);
            await _chainsRepository.UpdateAsync(oldChain);
            return Ok(_mapper.Map<ChainDto>(oldChain));
        }
        [HttpDelete("{id}")]
        
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var chian = await _chainsRepository.GetAsync(id);
            if (chian == null) return NotFound($"Chain with id of '{id}' not found.");
            await _chainsRepository.DeleteAsync(chian);
            //204
            return NoContent();

        }
    }
}
