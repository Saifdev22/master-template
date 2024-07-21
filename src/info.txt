Add-Migration -Name ProductDMAdd2 -OutputDir Data/Migrations -Context ApplicationDbContext -Project Starter.Infrastructure -StartupProject Starter.API
Update-Database