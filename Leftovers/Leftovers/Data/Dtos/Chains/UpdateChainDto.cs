using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Leftovers.Data.Dtos.Chains
{
    public record UpdateChainDto([Required] string Name);
    
}
