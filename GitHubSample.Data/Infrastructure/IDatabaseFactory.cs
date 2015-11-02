using System;
using GitHubSample.Data;
using System.Data.Entity;

namespace GitHubSample.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        DbContext Get();
    }
}
