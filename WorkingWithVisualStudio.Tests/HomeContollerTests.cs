using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using WorkingWithVisualStudio.Controllers;
using WorkingWithVisualStudio.Models;
using Xunit;
using Moq;


namespace WorkingWithVisualStudio.Tests
{
    public class HomeContollerTests
    {
        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; set; }

            public void AddProduct(Product p) { }

        }

        [Theory]
        //[InlineData(275, 48.95, 19.50, 24.95)]
        //[InlineData(5, 48.95, 19.50, 24.95)]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsComplete(Product[] products)
        {
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(products);
            var controller = new HomeController() { Repository = mock.Object };

            controller.Repository = new ModelCompleteFakeRepository
            {
                Products = products
            };

            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            Assert.Equal(controller.Repository.Products, model, Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name && p1.Price == p2.Price));
        }

        class PropertyOnceFakeRepository : IRepository
        {
            public int PropertyCounter { get; set; } = 0;
            public IEnumerable<Product> Products
            {
                get
                {
                    PropertyCounter++;
                    return new[] { new Product { Name = "P1", Price = 100 } };
                }
            }
            public void AddProduct(Product p) { }
        }

        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products)
                .Returns(new[] { new Product { Name = "P1", Price = 100 } });
            var controller = new HomeController { Repository = mock.Object };

            var result = controller.Index();

            //Assert.Equal(1, repo.PropertyCounter);
            mock.VerifyGet(m => m.Products, Times.Once);
        }
    }
}
