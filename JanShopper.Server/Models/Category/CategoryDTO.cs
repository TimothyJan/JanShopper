﻿using System.ComponentModel.DataAnnotations;

namespace JanShopper.Server.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
        public string Name { get; set; }
    }
}