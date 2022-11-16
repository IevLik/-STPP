using Microsoft.AspNetCore.Mvc;
using Leftovers.Data.Repositories;
using Leftovers.Data.Entities;
using Leftovers.Data.Dtos.Meals;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Leftovers.Auth.Model;
using System.Security.Claims;
namespace Leftovers.Controllers
{
    [ApiController]
    [Route("api/chain/{chainId}/restaurant/{restaurantId}/meal")]
    public class MealsController : ControllerBase
    {
        private readonly IMealsRepository _mealsRepository;
        private readonly IMapper _mapper;
        private readonly IRestaurantsRepository _restaurantsRepository;
        private readonly IChainsRepository _chainsRepository;
        private readonly IAuthorizationService _authorizationService;
        public MealsController(IMealsRepository mealsRepository, IRestaurantsRepository restaurantsRepository, IChainsRepository chainsRepository, IMapper mapper, IAuthorizationService authorizationService)
        {
            _mealsRepository = mealsRepository;
            _mapper = mapper;
            _chainsRepository = chainsRepository;
            _restaurantsRepository = restaurantsRepository;
            _authorizationService = authorizationService;
        }
        [HttpGet]
        public async Task<IEnumerable<MealDto>> GetAllAsync(int restaurantId)
        {
            var meals = await _mealsRepository.GetAsync(restaurantId);
            return meals.Select(o => _mapper.Map<MealDto>(o));
        }
        [HttpGet("{mealId}")]
        public async Task<ActionResult<MealDto>> GetAsync(int restaurantId, int mealId)
        {
            var meal = await _mealsRepository.GetAsync(restaurantId,  mealId);
            if (meal == null) return NotFound($"Meal with id of '{mealId}' not found.");
            return Ok(_mapper.Map<MealDto>(meal));
        }

        [HttpPost]
        [Authorize(Roles = LeftoversUserRoles.RestaurantUser)]
        public async Task<ActionResult<MealDto>> PostAsync(int chainId, int restaurantId, CreateMealDto mealDto)
        {
            var chain = await _chainsRepository.GetAsync(chainId);
            if (chain == null) return NotFound($"Couldn't find a chain with id of '{chainId}'.");

            var restaurant = await _restaurantsRepository.GetAsync(chainId, restaurantId);
            if (restaurant == null) return NotFound($"Couldn't find a restaurant with id of '{restaurantId}'.");

            var meal = _mapper.Map<Meal>(mealDto);
            meal.RestaurantId = restaurantId;
            

            meal.UserId = User.FindFirstValue(CustomClaims.UserId);
            await _mealsRepository.InsertAsync(meal);

            return Created($"api/chain/{chainId}/restaurant/{restaurantId}/meal/{meal.Id}", _mapper.Map<MealDto>(meal));
        }

        [HttpPut("{mealId}")]
        [Authorize(Roles = LeftoversUserRoles.RestaurantUser)]
        public async Task<ActionResult<MealDto>> PutAsync(int chainId, int restaurantId, int mealId, UpdateMealDto mealDto)
        {
            var chain = await _chainsRepository.GetAsync(chainId);
            if (chain == null) return NotFound($"Couldn't find a chain with id of '{chainId}'.");

            var restaurant = await _restaurantsRepository.GetAsync(chainId, restaurantId);
            if (restaurant == null) return NotFound($"Couldn't find a restaurant with id of '{restaurantId}'.");

            var oldMeal = await _mealsRepository.GetAsync(restaurantId, mealId);
            if (oldMeal == null) return NotFound($"Meal with id '{mealId}' not found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, oldMeal, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();//403
            }


            _mapper.Map(mealDto, oldMeal);
            await _mealsRepository.UpdateAsync(oldMeal);
            return Ok(_mapper.Map<MealDto>(oldMeal));
        }
        

        [HttpDelete("{mealId}")]
        [Authorize(Roles = LeftoversUserRoles.RestaurantUser)]
        public async Task<ActionResult<Meal>> DeleteAsync(int restaurantId, int mealId)
        {
            var meal = await _mealsRepository.GetAsync(restaurantId, mealId);
            if (meal == null) return NotFound($"Meal with id '{mealId}' not found");

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, meal, PolicyNames.SameUser);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();//403
            }

            await _mealsRepository.DeleteAsync(meal);
            //204
            return NoContent();
        }
    }
}
