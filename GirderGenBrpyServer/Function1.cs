using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GirderGenBrpyServer
{
    public static class OnDataReady
    {
        [FunctionName("OnDataReady")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string responseMessage = "í êMíÜ";
            try
            {
                var param = new Dictionary<string, object>()
                {
                    ["name"] = "ÇÿÇÒÇΩ",
                    ["note"] = "ëÂç„ï{èoêg",
                    ["age"] = 30,
                    ["registerDate"] = "2021-12-01",
                };
                var client = new HttpClient();

                var jsonString = System.Text.Json.JsonSerializer.Serialize(param);
                var content = new StringContent(jsonString, Encoding.UTF8, @"application/json");
                //POST
                var result = await client.PostAsync(@"https://asia-northeast1-the-structural-engine.cloudfunctions.net/frameWeb-2", content);

                //GET
                var resultGet = await client.GetAsync(@"https://asia-northeast1-the-structural-engine.cloudfunctions.net/frameWeb-2");
                responseMessage = await resultGet.Content.ReadAsStringAsync();

            }
            catch (Exception e)
            {
                responseMessage = "é∏îs:" + e.Message;
            }
            return new OkObjectResult(responseMessage);
        }
    
    
    }
}
