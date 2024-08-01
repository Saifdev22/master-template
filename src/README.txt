Add-Migration -Name InitialCreate -OutputDir Data/Migrations -Context ApplicationDbContext -Project Starter.Infrastructure -StartupProject Starter.API
Update-Database -Project Identity.API -StartupProject Identity.API

Stopped at 13 mins
