﻿namespace BuildingBlocksClient.Application.Identity.DTOs
{
    public class RoleDTO
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Notes { get; set; }
    }
}
