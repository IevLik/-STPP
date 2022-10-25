using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leftovers.Data.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ChainId { get; set; }
        public Chain Chain { get; set; }
    }
}
