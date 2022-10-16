using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leftovers.Data.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public DateTime CreationTimeUtc { get; set; }
    }
}
