using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.RegularExpressions;

namespace WongaServiceB
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("myQueue", false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                // Subscribe to Message Received event
                consumer.Received += (m, a) =>
                {
                    var body = a.Body;
                    var message = Encoding.UTF8.GetString(body);
                    try
                    { 
                        Console.WriteLine($"Hello {ExtractName(message)}, I am your father!");
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine($"Invalid message: {message}");
                    }
                };
                channel.BasicConsume("myQueue", true, consumer);
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Validates Message format
        /// </summary>
        /// <param name="message">The message to be validated as string</param>
        /// <returns>Indicates if message is in correct format</returns>
        public static bool IsValidMessage(string message) => Regex.IsMatch(message, @"^Hello my name is,\s[a-zA-Z ]*$");


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string ExtractName(string message)
        {
            if (!IsValidMessage(message))
                throw new ArgumentException();
            else
                return message.Split(',')[1].Trim();
        }
    }
}