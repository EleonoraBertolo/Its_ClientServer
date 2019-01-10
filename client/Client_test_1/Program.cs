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
            List<SensorInterface> sensors = new List<SensorInterface>();
            sensors.Add(new VirtualTemperatureSensor());
            sensors.Add(new VirtualPositionSensor());
            int i = 0;
            while (i<4)
            {
                foreach (SensorInterface sensor in sensors)
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.101.68:8080/cars/AB123CD");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        Console.WriteLine(sensor.toJson());
                        streamWriter.Write(sensor.toJson());
                        streamWriter.Close();
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    var responseString = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();

                    Console.WriteLine(responseString);
                    Console.Out.WriteLine(httpResponse.StatusCode);
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine();

                    System.Threading.Thread.Sleep(1000);

                }

                // init sensors
                //TemperatureSensorInterface temperatureSensor = new VirtualTemperatureSensor();
                // TODO add more sensors

                /*  HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.101.68:8080/cars/AB123CD");
                  httpWebRequest.ContentType = "application/json";
                  httpWebRequest.Method = "POST";

                  using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                  {
                      Console.WriteLine(temperatureSensor.toJson());
                      streamWriter.Write(temperatureSensor.toJson());
                      streamWriter.Close();
                  }

                  var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                  var responseString = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();

                  Console.WriteLine(responseString);
                  Console.Out.WriteLine(httpResponse.StatusCode);
                  Console.WriteLine("---------------------------------------------------");
                  Console.WriteLine();

                  System.Threading.Thread.Sleep(1000);*/
                i++;
            }
            Console.ReadLine();
            /*
             
              static void Main(string[] args)
        {

            // init sensors
            List<SensorInterface> sensors = new List<SensorInterface>();
            sensors.Add(new VirtualTemperatureSensor());
            sensors.Add(new VirtualPositionSensor());

            while (true)
            {
                foreach (SensorInterface sensor in sensors)
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:8080/cars/AB123CD");
                    httpWebRequest.ContentType = "text/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(sensor.toJson());
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    Console.Out.WriteLine(httpResponse.StatusCode);

                    System.Threading.Thread.Sleep(1000);

                }

            }

        }
             
             */

            /* while (true)
             {
                 // init sensors
                 TemperatureSensorInterface temperatureSensor = new VirtualTemperatureSensor();
                 // TODO add more sensors

                 HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.101.48:8080/cars/AB123CD");
                 httpWebRequest.ContentType = "text/json";
                 httpWebRequest.Method = "GET";

                 var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                 var responseStream = httpResponse.GetResponseStream();

                 if (responseStream != null)
                 {
                     var streamReader=new StreamReader(responseStream);
                     Console.Write("HTTP Response is: ");
                     Console.WriteLine(streamReader.ReadToEnd());
                 }

                 if (responseStream != null) 
                     responseStream.Close();

                 System.Threading.Thread.Sleep(1000);
             }*/
        }
    }
}
