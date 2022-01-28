using Archi.api.Controllers;
using Archi.api.Models;
using Archi.Library.Models;
using Archi.Library.Test.data;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Archi.Library.Test
{
    public class CustomerControllerTests
    {
        private CustomersController _controller;
        private MockDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = MockDbContext.GetDbContext();
            _controller = new CustomersController(_context);
        }

        [Test]
        public async Task TestGetAll()
        {
            var actionResult = await _controller.GetAll(new Params());
            var Result = actionResult.Result as ObjectResult;
            var values = Result.Value as IEnumerable<Customer>;

            Assert.AreEqual((int)HttpStatusCode.OK, Result.StatusCode);
            Assert.IsNotNull(values);
        }
    }
}
