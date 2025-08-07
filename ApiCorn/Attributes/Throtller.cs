using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Concurrent;

[AttributeUsage(AttributeTargets.Method)]
public class Throttle : Attribute, IAsyncActionFilter
{
    private static readonly ConcurrentDictionary<string, SemaphoreSlim> _semaphores = new();
    private readonly int _maxRequests;
    private readonly int _perSeconds;
    private readonly string _throttleGroup;

    public Throttle(int maxRequests, int perSeconds, string throttleGroup = null)
    {
        if (maxRequests <= 0) throw new ArgumentOutOfRangeException(nameof(maxRequests));
        if (perSeconds <= 0) throw new ArgumentOutOfRangeException(nameof(perSeconds));
        
        _maxRequests = maxRequests;
        _perSeconds = perSeconds;
        _throttleGroup = throttleGroup;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var key = GetThrottleKey(context);
        var semaphore = _semaphores.GetOrAdd(key, _ => new SemaphoreSlim(_maxRequests, _maxRequests));

        if (!await semaphore.WaitAsync(TimeSpan.Zero))
        {
            context.Result = new ObjectResult(new
            {
                Error = "Too many requests",
                RetryAfter = $"{_perSeconds} seconds"
            })
            {
                StatusCode = StatusCodes.Status429TooManyRequests
            };
            return;
        }

        try
        {
            await next();
        }
        finally
        {
            _ = ReleaseAfterDelay(semaphore, TimeSpan.FromSeconds(_perSeconds));
        }
    }

    private string GetThrottleKey(ActionExecutingContext context)
    {
        if (!string.IsNullOrEmpty(_throttleGroup))
            return _throttleGroup;

        return $"{context.HttpContext.Request.Path}:{context.ActionDescriptor.DisplayName}";
    }

    private async Task ReleaseAfterDelay(SemaphoreSlim semaphore, TimeSpan delay)
    {
        try
        {
            await Task.Delay(delay);
            semaphore.Release();
        }
        catch
        {
            if (semaphore.CurrentCount < _maxRequests)
                semaphore.Release();
        }
    }

    public static IReadOnlyDictionary<string, int> GetThrottleStates()
    {
        return _semaphores.ToDictionary(
            kv => kv.Key,
            kv => kv.Value.CurrentCount
        );
    }
}