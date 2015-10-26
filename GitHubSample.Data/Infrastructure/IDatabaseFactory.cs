using System;
using GitHubSample.Data;

namespace GitHubSample.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        GitHubSampleEntities Get();
    }
}
