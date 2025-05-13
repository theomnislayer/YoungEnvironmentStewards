using Microsoft.Extensions.DependencyInjection;

namespace YES.Tests;

public abstract class BaseTest
{
    private IServiceScope _scope;

    protected abstract void Setup();

    [TestInitialize()]
    public void Initialize() 
    {
        _scope = Startup.ServiceProvider.CreateScope();
        Setup();
    }

    [TestCleanup()]
    public void Cleanup() 
    {
        _scope.Dispose();
    }

    public T GetService<T>() => _scope.ServiceProvider.GetService<T>();

}
