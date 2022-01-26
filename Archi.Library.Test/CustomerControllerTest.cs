using Archi.api.Controllers;
using Archi.api.Models;
using Archi.Library.Test.data;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Archi.Library.Test
{
    public class CustomerControllerTest
    {
        private CustomersController _controller;
        private MockDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = MockDbContext.GetDbContext();
            _controller = new CustomersController();
        }

        [Test]
        public async Task TestGetAll()
        {
            var actionResult = await _controller.TestGetAll(new Settings());
            var result = actionResult.Result as ObjectResult;
            var value = result.Value as IEnumerable<Customer>;

            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(value);
            Assert.AreEqual(_context.Customers.Count(), value.Count());
        }
    }
}
