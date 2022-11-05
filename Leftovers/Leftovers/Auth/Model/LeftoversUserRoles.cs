namespace Leftovers.Auth.Model
{
    public class LeftoversUserRoles
    {
        public const string Admin = nameof(Admin);
        public const string SimpleUser = nameof(SimpleUser);
        public const string RestaurantUser = nameof(RestaurantUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, SimpleUser, RestaurantUser };
    }
}
