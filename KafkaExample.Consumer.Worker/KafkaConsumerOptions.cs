namespace KafkaExample.Consumer.Worker;

public class KafkaConsumerOptions
{
    public const string Kafka = "Kafka";

    public required string BootstrapServers { get; set; }

    public required string GroupId { get; set; }

    public required string Topic { get; set; }
}