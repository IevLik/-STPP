using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Leftovers.Data.Dtos.Restaurants
{
    public record CreateRestaurantDto([Required]string Name, [Required] string Description);
    
}
