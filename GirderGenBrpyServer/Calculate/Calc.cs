using GirderGenBrpyServer.Printing;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json;
using System.Diagnostics;


namespace GirderGenBrpyServer.Calculate
{
    /// <summary>
    /// 計算するクラス
    /// </summary>
    public class Calc
    {
        /// <summary>
        /// テスト用
        /// </summary>
        public void Test()
        {
            var printer = new GirderGenBrpyServer.Printing.PrintData();
            string responseMessage = "通信中";
            try { 
            // テストデータ作って
            var param = new Dictionary<string, object>()
            {
                ["name"] = "ぺんた",
                ["note"] = "大阪府出身",
                ["age"] = 30,
                ["registerDate"] = "2021-12-01",
            };
            Console.WriteLine(printer.printDatas);
            // GCPへポストする
            Debug.WriteLine(printer);
            string jsonString = JsonConvert.SerializeObject(printer.printDatas,Formatting.Indented);
            //string jsonString =System.Text.Json.JsonSerializer.Serialize(printer);

            //POST
            PostConfigureOptions(jsonString);
            //GET
            GetConfigureOptions();
        }
            catch (Exception ex) {
                responseMessage = "失敗" + ex.Message;
    }
            }

        private async void PostConfigureOptions(string jsonString)
        {
            var content = new StringContent(jsonString, Encoding.UTF8, @"application/json");
            var client = new HttpClient();
            var result = await client.PostAsync(@"https://asia-northeast1-the-structural-engine.cloudfunctions.net/frameWeb-2", content);
            var responseMessage = await result.Content.ReadAsStringAsync();
             Console.WriteLine(responseMessage);
        }

        private async void GetConfigureOptions()
        {
            var client = new HttpClient();
            var resultGet = await client.GetAsync(@"https://asia-northeast1-the-structural-engine.cloudfunctions.net/frameWeb-2");
            var responseMessage = await resultGet.Content.ReadAsStringAsync();
            Console.WriteLine(responseMessage);
        }

}
    }
