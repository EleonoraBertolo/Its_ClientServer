using System;
using System.Text;
using System.Collections.Generic;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Client.Sensors;

namespace Client_Test_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SensorInterface> sensors = new List<SensorInterface>();
            sensors.Add(new VirtualTemperatureSensor());
            sensors.Add(new VirtualPositionSensor());
            string targa = "ab123cd";

            MqttClient client = new MqttClient("test.mosquitto.org");        // creo l'istanza per la connessione
            ushort msgId;

            int i = 0;
            while (i<4) {

                foreach (SensorInterface sensor in sensors)
                {
                    client.ProtocolVersion = MqttProtocolVersion.Version_3_1;       // decido il tipo di versione del protocollo

                    byte code = client.Connect(Guid.NewGuid().ToString());          // connessione

                    msgId = client.Publish("kitt/auto/"+targa+"/tipo_sensore",                       // topic
                                      Encoding.UTF8.GetBytes(sensor.toJson()),      // message body -> JSON
                                      MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,           // QoS level
                                      true);                                       // retained

                    System.Threading.Thread.Sleep(1000);
                }

                i++;
            }

            msgId = client.Subscribe(new string[] { "kitt/auto/"+targa+"/action"},
                                             new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
                                                          MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            Console.ReadLine();
        }
    }
}