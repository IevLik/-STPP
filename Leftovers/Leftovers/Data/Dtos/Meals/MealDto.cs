using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Leftovers.Data.Dtos.Meals
{
        public record MealDto(int Id, string Name, string Price/*, DateTime CreationTimeUtc*/);
}
