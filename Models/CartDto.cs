﻿using System.Collections.Generic;
 using WebApplication.Models;

 namespace WebApplication.Dtos
{
    public class CartDto
    {
        public string Id { get; set; }

        public List<ProductDto> Products { get; set; }

        public int TotalPrice { get; set; }

        public CartStatus Status { get; set; }
    }
}