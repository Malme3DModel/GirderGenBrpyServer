using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace FrameData
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
            var frame = new FrameData();
            string responseMessage = "通信中";
            try
            {
                // テストデータ作って
                var param = new Dictionary<string, object>()
                {
                    ["name"] = "ぺんた",
                    ["note"] = "大阪府出身",
                    ["age"] = 30,
                    ["registerDate"] = "2021-12-01",
                };
                Console.WriteLine(frame.frameDatas);
                // GCPへポストする
                Debug.WriteLine(frame);
                string jsonString = JsonConvert.SerializeObject(frame.frameDatas, Formatting.Indented);
                //string jsonString =System.Text.Json.JsonSerializer.Serialize(printer);

                //POST
                PostConfigureOptions(jsonString);
                //GET
                GetConfigureOptions();
            }
            catch (Exception ex)
            {
                responseMessage = "失敗" + ex.Message;
            }
        }

        private async void PostConfigureOptions(string jsonString)
        {
            var content = new StringContent(jsonString, Encoding.UTF8, @"application/json");
            var client = new HttpClient();
            var result = await client.PostAsync(@"https://asia-northeast1-the-structural-engine.cloudfunctions.net/frameWeb-3", content);
            var responseMessage = await result.Content.ReadAsStringAsync();
            Console.WriteLine(responseMessage);
        }

        private async void GetConfigureOptions()
        {
            var client = new HttpClient();
            var resultGet = await client.GetAsync(@"https://asia-northeast1-the-structural-engine.cloudfunctions.net/frameWeb-3");
            var responseMessage = await resultGet.Content.ReadAsStringAsync();
            Console.WriteLine(responseMessage);
        }

    }
}
