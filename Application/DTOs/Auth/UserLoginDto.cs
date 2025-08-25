﻿using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Application.Dtos.Auth
{

    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;
    }

}
