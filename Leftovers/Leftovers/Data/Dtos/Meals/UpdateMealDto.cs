﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Leftovers.Data.Dtos.Meals
{
        public record UpdateMealDto([Required] string Name, [Required] string Price);
}
