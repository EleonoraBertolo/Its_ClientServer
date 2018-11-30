using System;
using System.Collections.Generic;
using Client.Sensors;
using System.Net;
using System.IO;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                // init sensors
                TemperatureSensorInterface temperatureSensor = new VirtualTemperatureSensor();
                // TODO add more sensors

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/cars/AB123CD");
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(temperatureSensor.toJson());
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                Console.Out.WriteLine(httpResponse.StatusCode);

                System.Threading.Thread.Sleep(1000);

            }

        }

    }

}
