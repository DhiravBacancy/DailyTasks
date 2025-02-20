public interface ISingletonGuidService
{
    string GetGuid();
}

public interface IScopedGuidService
{
    string GetGuid();
}

public interface ITransientGuidService
{
    string GetGuid();
}
public class SingletonGuidService : ISingletonGuidService
{
    private readonly string _guid;

    public SingletonGuidService()
    {
        _guid = Guid.NewGuid().ToString();
        Console.WriteLine($"Singleton instance created: {_guid}");
    }

    public string GetGuid()
    {
        return _guid;
    }
}

public class ScopedGuidService : IScopedGuidService
{
    private readonly string _guid;

    public ScopedGuidService()
    {
        _guid = Guid.NewGuid().ToString();
        Console.WriteLine($"Scoped instance created: {_guid}");
    }

    public string GetGuid()
    {
        return _guid;
    }
}

public class TransientGuidService : ITransientGuidService
{
    private readonly string _guid;

    public TransientGuidService()
    {
        _guid = Guid.NewGuid().ToString();
        Console.WriteLine($"Transient instance created: {_guid}");
    }

    public string GetGuid()
    {
        return _guid;
    }
}

