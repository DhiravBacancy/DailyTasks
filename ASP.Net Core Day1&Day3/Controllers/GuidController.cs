using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/guid")]
public class GuidController : ControllerBase
{
    private readonly ISingletonGuidService _singleton;
    private readonly IScopedGuidService _scoped;
    private readonly IServiceProvider _serviceProvider; // Add service provider

    public GuidController(
        ISingletonGuidService singleton,
        IScopedGuidService scoped,
        IServiceProvider serviceProvider)
    {
        _singleton = singleton;
        _scoped = scoped;
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    public IActionResult GetGuids()
    {
        // Requesting a new transient instance every time
        var transient1 = _serviceProvider.GetService<ITransientGuidService>();
        var transient2 = _serviceProvider.GetService<ITransientGuidService>();

        var firstCall = new
        {
            Singleton = _singleton.GetGuid(),
            Scoped = _scoped.GetGuid(),
            Transient = transient1.GetGuid()  // First transient call
        };

        var secondCall = new
        {
            Singleton = _singleton.GetGuid(),
            Scoped = _scoped.GetGuid(),
            Transient = transient2.GetGuid()  // Second transient call
        };
        Console.WriteLine(firstCall.Singleton , secondCall.Singleton);
        return Ok(new { FirstCall = firstCall, SecondCall = secondCall });
    }
}
