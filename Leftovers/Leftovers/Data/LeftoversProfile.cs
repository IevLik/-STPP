using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Leftovers.Data.Dtos.Chains;
using Leftovers.Data.Dtos.Restaurants;
using Leftovers.Data.Dtos.Meals;
using Leftovers.Data.Entities;


namespace Leftovers.Data
{
    public class LeftoversProfile : Profile
    {
        public LeftoversProfile()
        {
            CreateMap<Chain, ChainDto>();
            CreateMap<CreateChainDto, Chain>();
            CreateMap<UpdateChainDto, Chain>();

            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<CreateRestaurantDto, Restaurant>();
            CreateMap<UpdateRestaurantDto, Restaurant>();

            CreateMap<Meal, MealDto>();
            CreateMap<CreateMealDto, Meal>();
            CreateMap<UpdateMealDto, Meal>();
        }
    }
}
