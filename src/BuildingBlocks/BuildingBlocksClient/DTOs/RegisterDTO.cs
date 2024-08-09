﻿using System.ComponentModel.DataAnnotations;

namespace BuildingBlocksClient.DTOs
{
    public class RegisterDTO
    {
        public string? Id { get; set; } = string.Empty;

        [Required]
        public string Nickname { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Tenant { get; set; } = string.Empty;
    }
}
