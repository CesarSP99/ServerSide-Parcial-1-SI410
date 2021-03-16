using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ApiDoble.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiDoble.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        // POST api/<RandomController>
        [HttpPost]
        public async Task<string> PostAsync([FromBody] Models.Random random)
        {
            string connectionString = "";
            string queueName = "";
            //Si es par, usamos el connection string y cola par
            if (random.RandomNumber % 2 == 0)
            {
                connectionString = "Endpoint=sb://flujorandomcesar.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=+fHGmL6EME451T4F0WzqjlboGOkBv/AVpI3mH5aLMgI=;EntityPath=par";
                queueName = "par";
            }
            //Si no, usamos la cola impar
            else
            {
                connectionString = "Endpoint=sb://flujorandomcesar.servicebus.windows.net/;SharedAccessKeyName=Send;SharedAccessKey=yblvH/Si/7ZHT+FUl5dHes78QXSfoox9uJS12ZaFzB4=;EntityPath=impar";
                queueName = "impar";
            }
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(random.ToJson());

                // send the message
                await sender.SendMessageAsync(message);
            }
            return $"Recibi el numero {random.RandomNumber}";
        }
    }
}
