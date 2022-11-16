using Leftovers.Auth.Model;
using Leftovers.Data.Dtos.Auth;
using System.ComponentModel.DataAnnotations;

namespace Leftovers.Data.Entities
{
    public class Meal : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public string? UserId { get; set; }
        public LeftoversUser? User { get; set; }
    }
}
