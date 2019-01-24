using System;
using System.Collections.Generic;
using System.Text;
using Client.Sensors;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Client_Test4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SensorInterface> sensors = new List<SensorInterface>();
            sensors.Add(new VirtualTemperatureSensor());
            sensors.Add(new VirtualPositionSensor());
            string targa = "ab123cd";

            int i = 0;

            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri= new Uri("amqp://vdznsexb:3gE_psy4Q32UB1hgzyOy1-ovSt7vHmUr@golden-kangaroo.rmq.cloudamqp.com/vdznsexb");

            using (var connection = factory.CreateConnection())

            while (i < 5)
            {
                foreach (SensorInterface sensor in sensors)
                {
                    using (var channel = connection.CreateModel())
                    {
                            /*  3. publish/subscribe 
                            channel.ExchangeDeclare(exchange: "logs", type: "fanout");

                            var queueName = channel.QueueDeclare().QueueName;

                            var message1 = sensor.toJson();
                            var body1 = Encoding.UTF8.GetBytes(message1);
                            channel.BasicPublish(exchange: "logs",
                                                 routingKey: "",
                                                 basicProperties: null,
                                                 body: body1);

                            Console.WriteLine(" [*] Waiting for logs.");*/

                            //  4. Routing
                            channel.ExchangeDeclare(exchange: "direct_logs",
                                                    type: "direct");

                            var severity = (args.Length > 0) ? args[0] : "info";
                            var message = sensor.toJson();
                            var body = Encoding.UTF8.GetBytes(message);
                            channel.BasicPublish(exchange: "direct_logs",
                                                 routingKey: severity,
                                                 basicProperties: null,
                                                 body: body);
                            Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, message);
                        }
                }
                i++;
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
