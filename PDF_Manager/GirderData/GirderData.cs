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
