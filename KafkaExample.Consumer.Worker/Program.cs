// Guideline Docs: https://medium.com/simform-engineering/creating-microservices-with-net-core-and-kafka-a-step-by-step-approach-1737410ba76a

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<ConsumerWorker>();

var host = builder.Build();
host.Run();
