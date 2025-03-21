using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehavior(ILogger<TRequest>  logger)
        {
            _logger = logger;
        }
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;
            try
            {
                _logger.LogInformation("Handling Request {Name} {@Request}", name, request);
                var response = next();
                _logger.LogInformation("Handled Request {Name} {@Response}", name, response);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling Request {Name}", name);
                throw;
            }
            throw new NotImplementedException();
        }
    }
}
