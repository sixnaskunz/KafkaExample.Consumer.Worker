using KafkaExample.Consumer.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<ConsumerWorker>();

var host = builder.Build();
host.Run();
