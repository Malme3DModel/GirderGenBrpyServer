using FrameData.InputData;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GirderData
{


    public class GirderData
    {
        private Dictionary<string, object> myData = null;

        public GirderData(string jsonString)
        {
            // データを読み込む
            JObject data = JObject.Parse(jsonString);
            this.myData = data.ToObject<Dictionary<string, object>>();
            //　準備のためのclassの呼び出し
            setGirderData();
        }

        /// <summary>
        /// 
        /// </summary>
        public string title {
            get
            {
                if (this.myData.ContainsKey("title"))
                {
                    var obj = this.myData["title"];
                    return obj.ToString();
                }
                return "";
            }
        }

        public int amount_V
        {
            get
            {
                if (this.myData.ContainsKey("beam"))
                {
                    var obj = this.myData["beam"];
                    JObject data = JObject.FromObject(obj);
                    var beam = data.ToObject<Dictionary<string, object>>();
                    if (beam.ContainsKey("amount_V"))
                    {
                        var obj2 = beam["amount_V"];
                        return Convert.ToInt32(obj2);
                    }
                    
                }
                return 1;
            }
        }

        public int amount_H
        {
            get
            {
                if (this.myData.ContainsKey("others"))
                {
                    var obj = this.myData["others"];
                    JObject data = JObject.FromObject(obj);
                    var beam = data.ToObject<Dictionary<string, object>>();
                    if (beam.ContainsKey("amount_H"))
                    {
                        var obj2 = beam["amount_H"];
                        return Convert.ToInt32(obj2);
                    }

                }
                return 1;
            }
        }

        public string crossbeam
        {
            get
            {
                if (this.myData.ContainsKey("display"))
                {
                    var obj = this.myData["display"];
                    JObject data = JObject.FromObject(obj);
                    var beam = data.ToObject<Dictionary<string, object>>();
                    if (beam.ContainsKey("crossbeam"))
                    {
                        var obj2 = beam["crossbeam"];
                        return obj2.ToString();
                    }

                }
                return "";
            }
        }

        public int amount_C
        {
            get
            {
                if (this.myData.ContainsKey("crossbeam"))
                {
                    var obj = this.myData["crossbeam"];
                    JObject data = JObject.FromObject(obj);
                    var beam = data.ToObject<Dictionary<string, object>>();
                    if (beam.ContainsKey("lacation2"))
                    {
                        var obj2 = beam["location2"];
                        return Convert.ToInt32(obj2);
                    }

                }
                return 1;
            }
        }



        public string pageSize {
            get
            {
                if (this.myData.ContainsKey("pageSize"))
                {
                    var obj = this.myData["pageSize"];
                    return obj.ToString();
                }
                return "A4";
            }
        }
        public string pageOrientation {
            get
            {
                if (this.myData.ContainsKey("pageOrientation"))
                {
                    var obj = this.myData["pageOrientation"];
                    return obj.ToString();
                }
                return "Vertical";
            }
        }

        /// <summary>
        /// データの整合性などをチェックし調整する
        /// </summary>
        /// <param name="data"></param>
        public void setGirderData()
        {
            if (this.myData.ContainsKey("others"))
            {
                // ごにょごにょ
            }

        }

    }
}
