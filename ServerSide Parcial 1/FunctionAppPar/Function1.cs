using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionAppPar
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task RunAsync(
            [ServiceBusTrigger("par", Connection = "MyConn")]string myQueueItem,
            [CosmosDB(databaseName: "dbPar", collectionName: "Pares", ConnectionStringSetting = "strCosmos")] IAsyncCollector<Models.Random> datos,
            ILogger log)
        {
            try
            {
                log.LogInformation($"C# ServiceBus queue trigger function proceso: {myQueueItem}");
                var random = Models.Random.FromJson(myQueueItem);
                await datos.AddAsync(random);
            }
            catch(Exception ex){
                log.LogError($"No se puede agregar datos: {ex.Message}");
            }
        }
    }
}
