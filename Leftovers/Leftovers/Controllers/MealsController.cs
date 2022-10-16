using Microsoft.AspNetCore.Mvc;
using Leftovers.Data.Repositories;
using Leftovers.Data.Entities;
using Leftovers.Data.Dtos.Meals;
using AutoMapper;

namespace Leftovers.Controllers
{
    [ApiController]
    [Route("api/meals")]
    public class MealsController : ControllerBase
    {
        private readonly IMealsRepository _mealsRepository;
        private readonly IMapper _mapper;
        public MealsController(IMealsRepository mealsRepository, IMapper mapper)
        {
            _mealsRepository = mealsRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<MealDto>> GetAllAsync()
        {
            var meals = await _mealsRepository.GetAsync();
            return meals.Select(o => _mapper.Map<MealDto>(o));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MealDto>> GetAsync(int id)
        {
            var meal = await _mealsRepository.GetAsync(id);
            if (meal == null) return NotFound($"Meal with id of '{id}' not found.");
            return Ok(_mapper.Map<MealDto>(meal));
        }

        [HttpPost]
        public async Task<ActionResult<MealDto>> PostAsync(CreateMealDto mealDto)
        {
            var meal = _mapper.Map<Meal>(mealDto);
            await _mealsRepository.InsertAsync(meal);
            return Created($"/api/meals/{meal.Id}", _mapper.Map<MealDto>(meal));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MealDto>> PutAsync(int id, UpdateMealDto mealDto)
        {
            var oldMeal = await _mealsRepository.GetAsync(id);
            if (oldMeal == null) return NotFound($"Meal with id of '{id}' not found.");

            // oldRestaurant.Description = restaurantDto.Description;
            _mapper.Map(mealDto, oldMeal);
            await _mealsRepository.UpdateAsync(oldMeal);
            return Ok(_mapper.Map<MealDto>(oldMeal));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var meal = await _mealsRepository.GetAsync(id);
            if (meal == null) return NotFound($"Meal with id of '{id}' not found.");
            await _mealsRepository.DeleteAsync(meal);
            //204
            return NoContent();
        }
    }
}
