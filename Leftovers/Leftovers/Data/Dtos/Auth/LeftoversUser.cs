using System;
using Microsoft.AspNetCore.Identity;

namespace Leftovers.Data.Dtos.Auth
{
    public class LeftoversUser : IdentityUser
    {
        [PersonalData]
        public string? AdditonalInfo { get; set; }   

    }
}
