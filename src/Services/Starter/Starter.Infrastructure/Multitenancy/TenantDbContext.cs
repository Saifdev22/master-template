﻿using Microsoft.EntityFrameworkCore;

namespace Starter.Infrastructure.Multitenancy
{
    public class TenantDbContext : DbContext
    {
        // This context is for looking up the tenant when a request comes in.
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) { }

        public DbSet<Tenant> Tenants { get; set; }

    }
}
