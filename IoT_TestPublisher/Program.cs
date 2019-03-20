using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IoT_TestPublisher.Entities;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Configuration;

namespace IoT_TestPublisher
{
    class Program
    {
        const string ServiceBusConnectionString = "<your_connection_string>";
        const string QueueName = "<your_queue_name>";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult(); ;
        }

        static async Task MainAsync()
        {
            const int numberOfMessages = 10;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            // Send messages.
            await SendMessagesAsync(numberOfMessages);

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    IoTMessage iotmessage = new IoTMessage()
                    {
                        DeviceId = "000-000-00" + i.ToString(),
                        TimeStamp = DateTime.Now,
                        Status = IoTStatus.Alive,
                        message = "Testing Message " + i.ToString()
                    };
                    // Create a new message to send to the queue.
                    string messageBody = JsonConvert.SerializeObject(iotmessage, Formatting.Indented);
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the queue.
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
