using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionAppImpar
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async System.Threading.Tasks.Task RunAsync(
            [ServiceBusTrigger("impar", Connection = "MyConn")] string myQueueItem,
            [CosmosDB(databaseName: "dbImpar", collectionName: "Impares", ConnectionStringSetting = "strCosmos")] IAsyncCollector<Models.Random> datos,
            ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function proceso: {myQueueItem}");
                var random = JsonConvert.DeserializeObject<Models.Random>(myQueueItem);
                await datos.AddAsync(random);
            }
            catch (Exception ex)
            {
                log.LogError($"No se puede agregar datos: {ex.Message}");
            }
        }
    }
}
