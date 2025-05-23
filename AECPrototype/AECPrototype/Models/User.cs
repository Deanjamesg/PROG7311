﻿using Microsoft.AspNetCore.Identity;

namespace AECPrototype.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
