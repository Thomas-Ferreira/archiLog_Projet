using Archi.Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Archi.Library.Data
{
    public class BaseDbContext :  DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {

        }

        public override int SaveChanges()
        {
            ChangedDeletedState();
            ChangedCreatedState();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangedDeletedState();
            ChangedCreatedState();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ChangedDeletedState()
        {
            var delEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);
            foreach (var item in delEntities)
            {
                if (item.Entity is BaseModel)
                {
                    ((BaseModel)item.Entity).Active = false;
                    ((BaseModel)item.Entity).DeletedAt = DateTime.Now;
                    item.State = EntityState.Modified;
                }
            }
        }

        private void ChangedCreatedState()
        {
            var delEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach(var item in delEntities)
            {
                if(item.Entity is BaseModel model)
                {
                    model.Active = true;
                }
            }
        }
    }
}
