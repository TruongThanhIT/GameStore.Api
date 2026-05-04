using System.Diagnostics;

namespace GameStore.Api.Presentation.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        LogRequest(context);

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();

            LogResponse(context, stopwatch.ElapsedMilliseconds);
        }
    }

    private void LogRequest(HttpContext context)
    {
        var request = context.Request;

        var logData = new
        {
            request.Method,
            request.Path,
            QueryString = request.QueryString.ToString(),
            request.ContentType,
            request.ContentLength,
            UserAgent = request.Headers.UserAgent.ToString(),
            RemoteIpAddress = context.Connection.RemoteIpAddress?.ToString(),
            TraceId = context.TraceIdentifier
        };

        _logger.LogInformation(
            "HTTP Request: {Method} {Path}{QueryString} - ContentType: {ContentType}, ContentLength: {ContentLength}, UserAgent: {UserAgent}, RemoteIp: {RemoteIpAddress}, TraceId: {TraceId}",
            logData.Method,
            logData.Path,
            logData.QueryString,
            logData.ContentType,
            logData.ContentLength,
            logData.UserAgent,
            logData.RemoteIpAddress,
            logData.TraceId
        );
    }

    private void LogResponse(HttpContext context, long responseTimeMs)
    {
        var response = context.Response;

        var logLevel = response.StatusCode >= 400 ? LogLevel.Warning :
                      response.StatusCode >= 500 ? LogLevel.Error : LogLevel.Information;

        var logData = new
        {
            response.StatusCode,
            response.ContentType,
            response.ContentLength,
            ResponseTimeMs = responseTimeMs,
            TraceId = context.TraceIdentifier
        };

        _logger.Log(
            logLevel,
            "HTTP Response: {StatusCode} - ContentType: {ContentType}, ContentLength: {ContentLength}, ResponseTime: {ResponseTimeMs}ms, TraceId: {TraceId}",
            logData.StatusCode,
            logData.ContentType,
            logData.ContentLength,
            logData.ResponseTimeMs,
            logData.TraceId
        );
    }
}