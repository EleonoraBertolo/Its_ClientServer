using System;
using System.Collections.Generic;
using Client.Sensors;
using CoAP;

namespace Client_Test_3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SensorInterface> sensors = new List<SensorInterface>();
            sensors.Add(new VirtualTemperatureSensor());
            sensors.Add(new VirtualPositionSensor());
            string targa = "ab123cd";

            var client = new CoapClient(new Uri("coap://192.168.101.33/kitt/auto/ "+targa+" / tipo_sensore"));
            Request request;
            Response response;

            int i = 0;

            while (i<5)
            {
                foreach (SensorInterface sensor in sensors)
                {
                    request = new Request(Method.POST);
                    request.Token = new byte[2];
                    request.URI = client.Uri;
                    request.SetPayload($"data from client {i}");

                    request.ID = i;
                    request.Token[0] = Convert.ToByte(i);
                    request.Token[1] = Convert.ToByte(i);

                    Console.WriteLine("Request: \t\t" + request);
                    request.Send();

                    response = request.WaitForResponse();

                    if (response == null)       // timeout
                    {
                        Console.WriteLine("Request timeout");
                        break;
                    }
                    else        // success
                    {
                        Console.WriteLine("Response: \t\t" + response);
                        i++;
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
