using Archi.api.Data;
using Archi.api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archi.Library.Test.data
{
    public class MockDbContext : ArchiDbContext
    {
        public MockDbContext(DbContextOptions options):base(options)
        {

        }
        public static MockDbContext GetDbContext(bool withData = true)
        {
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase("dbtest").Options;
            var db = new MockDbContext(options);

            if(withData)
            {
                db.Customers.Add(new Customer { Active = true, Lastname = "Bob", Firstname = "Bob", Email = "bob@bob.fr" });
                db.Customers.Add(new Customer { Active = true, Lastname = "Bob", Firstname = "Bob", Email = "bob@bob.fr" });
                db.Customers.Add(new Customer { Active = true, Lastname = "Bob", Firstname = "Bob", Email = "bob@bob.fr" });
                db.SaveChanges();
            }

            return db;
        }
    }
}
