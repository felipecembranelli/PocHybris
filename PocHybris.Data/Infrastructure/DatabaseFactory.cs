using PocHybris.Data;
using System.Data.Entity;

namespace PocHybris.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private PocHybrisEntities dataContext;
        public DbContext Get()
        {
            return dataContext ?? (dataContext = new PocHybrisEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
