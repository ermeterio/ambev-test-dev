namespace Ambev.DeveloperEvaluation.WebApi.Infrastructure
{
    public class RabbitMqSettings
    {
        public string Connection { get; set; } = string.Empty;
        public string QueueName { get; set; } = string.Empty;
    }
}