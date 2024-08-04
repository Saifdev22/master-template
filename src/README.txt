Add-Migration -Name InitialCreate -OutputDir Multitenancy/Migrations -Context TenantDbContext -Project Starter.Infrastructure -StartupProject Starter.API
Update-Database -Project Starter.Infrastructure -StartupProject Starter.API -Context TenantDbContext

Stopped at 13 mins
