﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using WorkingWithVisualStudio.Controllers;
using WorkingWithVisualStudio.Models;
using Xunit;


namespace WorkingWithVisualStudio.Tests
{
    public class HomeContollerTests
    {
        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; } = new Product[] {
                new Product {Name = "P1", Price = 275M},
                new Product {Name = "P2", Price = 48.95M},
                new Product {Name = "P3", Price = 19.50M},
                new Product {Name = "P4", Price = 34.95M}
            };

            public void AddProduct(Product p) { }

        }

        [Fact]
        public void IndexActionModelIsComplete()
        {
            var controller = new HomeController();
            controller.Repository = new ModelCompleteFakeRepository();

            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }
    }
}
