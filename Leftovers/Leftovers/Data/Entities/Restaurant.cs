using Leftovers.Auth.Model;
using Leftovers.Data.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leftovers.Data.Entities
{
    public class Restaurant : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ChainId { get; set; }
        public Chain Chain { get; set; }

        [Required]
        public string UserId { get; set; }
        public LeftoversUser User { get; set; }
    }
}
