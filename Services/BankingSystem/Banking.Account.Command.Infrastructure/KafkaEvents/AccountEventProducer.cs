using Banking.Account.Command.Application.Models;
using Banking.CQRS.Core.Events;
using Banking.CQRS.Core.Producers;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Command.Infrastructure.KafkaEvents
{
    internal class AccountEventProducer : IEventProducer
    {

        private readonly KafkaSettings _kafkaSettings;
        private readonly string _kafkaServer;

        public AccountEventProducer(IOptions<KafkaSettings> kafkaSettings)
        {
            _kafkaSettings = kafkaSettings.Value;
            _kafkaServer = $"{_kafkaSettings.Hostname}:{_kafkaSettings.Port}";
        }

        public void Produce(string topic, EventBase eventToProduce)
        {
            var config = new ProducerConfig {
                BootstrapServers = _kafkaServer
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var eventType = eventToProduce.GetType();
                string serializedEvent = JsonConvert.SerializeObject(eventToProduce);

                var message = new Confluent.Kafka.Message<Null, string> { Value = serializedEvent };

                producer.ProduceAsync(topic, message).GetAwaiter().GetResult();
            }

        }
    }
}
