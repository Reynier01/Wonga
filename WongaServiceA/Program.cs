using System;
using RabbitMQ.Client;
using System.Text;

namespace WongaServiceA
{
    class Program
    {
        private const string QUEUE_NAME = "myQueue";
        static void Main(string[] args)
        {
            string name = string.Empty;
            QueuePublisher queuePublisher = new QueuePublisher(QUEUE_NAME);
            do
            {
                Console.Write("Enter a name ['q' to quit]: ");
                name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name) && name.ToLower() != "q")
                {
                    queuePublisher.PublishMessage($"Hello my name is, {name}");
                    Console.Clear();
                    Console.WriteLine("Published Message: {0}", name);
                }
            } while (name.ToLower() != "q");
        }
    }


    /// <summary>
    /// Used to publish to RabbitMQ Queue
    /// </summary>
    public class QueuePublisher
    {
        private readonly string queueName;
        private readonly ConnectionFactory connectionFactory = null;
        private const string HOST_NAME = "localhost";

        /// <summary>
        /// Constructor for QueueuPublisher object
        /// </summary>
        /// <param name="queueName">The name of the Queue that object should publish to</param>
        public QueuePublisher(string queueName)
        {
            this.queueName = queueName;
            connectionFactory = new ConnectionFactory() { HostName = HOST_NAME };
        }

        /// <summary>
        /// Publish a message to a queue
        /// </summary>
        /// <param name="message">The message to be published</param>
        public void PublishMessage(string message)
        {
            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName, false, false, false, null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish("", queueName, null, body);
            }
        }
    }

}
