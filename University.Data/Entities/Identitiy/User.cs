﻿using Microsoft.AspNetCore.Identity;

namespace University.Data.Entities.Identitiy
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
