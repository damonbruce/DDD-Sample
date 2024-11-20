using NServiceBus;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace people_service.Handlers;
public class PeopleHandler : IHandleMessages<NewPerson>
{
    private readonly ILogger<PeopleHandler> _logger;

    public PeopleHandler(ILogger<PeopleHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(NewPerson message, IMessageHandlerContext context)
    {
        _logger.LogInformation($"Received message: {message.Name} ({message.Email})");
        
        return Task.CompletedTask;
    }
}