using FluentValidation;

namespace GameStore.Api.Presentation.Middleware;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argument = context.Arguments
            .OfType<T>()
            .FirstOrDefault();

        if (argument == null)
        {
            return await next(context);
        }

        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator == null)
        {
            return await next(context);
        }

        var validationResult = await validator.ValidateAsync(argument);

        if (!validationResult.IsValid)
        {
            return CreateValidationErrorResponse(validationResult.Errors);
        }

        return await next(context);
    }

    private static IResult CreateValidationErrorResponse(IEnumerable<FluentValidation.Results.ValidationFailure> errors)
    {
        var errorDetails = errors.Select(error => new
        {
            field = error.PropertyName,
            message = error.ErrorMessage,
            errorCode = error.ErrorCode
        });

        var response = new
        {
            error = new
            {
                type = "ValidationError",
                message = "One or more validation errors occurred.",
                details = errorDetails,
                timestamp = DateTime.UtcNow
            }
        };

        return Results.BadRequest(response);
    }
}

public static class ValidationFilterExtensions
{
    public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) where T : class
    {
        return builder.AddEndpointFilter<ValidationFilter<T>>();
    }
}