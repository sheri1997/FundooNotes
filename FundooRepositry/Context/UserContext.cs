using FundooModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositry.Context
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options)
        {
            
        }
        public DbSet<UserModel> User { get; set; }

    }
}
