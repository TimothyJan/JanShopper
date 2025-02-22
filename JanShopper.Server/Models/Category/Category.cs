﻿using JanShopper.Server.Validation;
using System.ComponentModel.DataAnnotations;

namespace JanShopper.Server.Models
{
    public class Category
    {
        private string _name;

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters.")]
        [UppercaseOnly(ErrorMessage = "Category name must contain only uppercase letters.")]
        public string Name {
            get => _name;
            set => _name = value?.ToUpper(); // Convert to uppercase
        }

        // Navigation property for related products
        public ICollection<Product> Products { get; set; }
    }
}