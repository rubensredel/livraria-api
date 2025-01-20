using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using RR.Livraria.Domain.Interfaces.Notification;

namespace RR.Livraria.API.Filters;

public class DomainNotificationFilter : IAsyncResultFilter
{
    private readonly IDomainNotification _domainNotification;

    public DomainNotificationFilter(IDomainNotification domainNotification)
    {
        _domainNotification = domainNotification;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!context.ModelState.IsValid || _domainNotification.HasNotifications)
        {
            var validations = !context.ModelState.IsValid ?
                JsonConvert.SerializeObject(
                    context.ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                ) :
                JsonConvert.SerializeObject(
                    _domainNotification.Notifications.Select(x => x.Value)
                );

            var problemDetails = new ProblemDetails
            {
                Title = "Bad Request",
                Status = StatusCodes.Status400BadRequest,
                Instance = context.HttpContext.Request.Path.Value,
                Detail = validations
            };

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/problem+json";

            await System.Text.Json.JsonSerializer.SerializeAsync(
                context.HttpContext.Response.Body,
                problemDetails
            );

            return;
        }

        await next();
    }
}
