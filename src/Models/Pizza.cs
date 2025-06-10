﻿namespace SemanticKernel.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Ingredients { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
