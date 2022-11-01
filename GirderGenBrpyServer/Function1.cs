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
 
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // ÉfÅ[É^ÇÃì«Ç›çûÇ›
            var inp = new GirderData.GirderData(requestBody);

            var calc = new Printing.CalcPrint(inp);

            string responseMessage = calc.getPdfSource();

            return new OkObjectResult(responseMessage);
        }
    
    
    }
}
