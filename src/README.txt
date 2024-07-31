Add-Migration -Name InitialCreate -OutputDir Data/Migrations -Context AppDbContext -Project Identity.API -StartupProject Identity.API
Update-Database -Project Identity.API -StartupProject Identity.API

Stopped at 13 mins
