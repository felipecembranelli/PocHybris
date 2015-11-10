using System;
using PocHybris.Data;
using System.Data.Entity;

namespace PocHybris.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        DbContext Get();
    }
}
