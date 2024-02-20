namespace KafkaExample.Consumer.Worker;

public class ConsumerWorker : BackgroundService
{
    private readonly ILogger<ConsumerWorker> _logger;
    private readonly IConfiguration _configuration;
    private readonly IConsumer<Ignore, string> _consumer;

    public ConsumerWorker(ILogger<ConsumerWorker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        ConsumerConfig consumerConfig = new()
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"],
            GroupId = configuration["Kafka:GroupId"],
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string? topic = _configuration["Kafka:Topic"];
        _logger.LogInformation("Consuming messages from topic: {topic}", topic);
        _consumer.Subscribe(topic);
        while (!stoppingToken.IsCancellationRequested)
        {
            ProcessKafkaMessage(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }

        _consumer.Close();
    }

    public void ProcessKafkaMessage(CancellationToken stoppingToken)
    {
        try
        {
            var consumeResult = _consumer.Consume(stoppingToken);

            _logger.LogInformation("Received message: {message}", consumeResult.Message.Value);
        }
        catch (ConsumeException ex)
        {
            _logger.LogError("Error occured: {exceptionMessage}", ex.Message);
        }
    }

}
