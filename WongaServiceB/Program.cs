using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.RegularExpressions;

namespace WongaServiceB
{
    public class Program
    {
        private const string QUEUE_NAME = "myQueue";
        private const string HOST_NAME = "localhost";

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = HOST_NAME };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(QUEUE_NAME, false, false, false, null);

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
                channel.BasicConsume(QUEUE_NAME, true, consumer);
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
        /// Extracts name from message if name is valid
        /// </summary>
        /// <param name="message">The message received as string</param>
        /// <returns>The extracted name as string</returns>
        public static string ExtractName(string message)
        {
            if (!IsValidMessage(message))
                throw new ArgumentException(message);
            else
                return message.Split(',')[1].Trim();
        }
    }
}