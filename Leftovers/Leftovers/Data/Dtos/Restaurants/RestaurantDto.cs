using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Leftovers.Data.Dtos.Restaurants
{
    public record RestaurantDto(int Id, string Name, string Description);
    
}
