using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Identity
{
    public class GoloIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public GoloIdentityDbContext(DbContextOptions<GoloIdentityDbContext> options)
            : base(options)
        {
        }
    }
}
