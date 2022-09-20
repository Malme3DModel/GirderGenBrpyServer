using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Printing.PrintInput
{
    //public class LoadName
    //{
    //    public double rate;
    //    public string symbol;
    //    public string Actual_load;
   //     public string name;
   //     public int fix_node;
   //     public int element;
   //     public int fix_member;
   //     public int joint;
   // }

    //internal class InputLoadName
   // {
    //    public const string KEY = "loadName";
    //    public Dictionary<string, LoadName> loadnames = new Dictionary<string, LoadName>();
//
    //    public InputLoadName()
//{
//var l = new LoadName();
   //         l.fix_node = 1;
    //        l.element = 1;
//this.loadnames.Add("1",l);
   //     }

  //  }
}

//    internal class inputloadname
//    {
//        public const string key = "loadname";

//        public dictionary<int, loadname> loadnames = new dictionary<int, loadname>();

//        public inputloadname(dictionary<string, object> value)
//        {
//            if (!value.containskey(inputload.key))
//                return;

//            //nodeデータを取得する
//            var target = jobject.fromobject(value[inputload.key]).toobject<dictionary<string, object>>();

//            // データを抽出する
//            for (var i = 0; i < target.count; i++)
//            {
//                var key = target.elementat(i).key;
//                var index = int.parse(key);
//                var item = jobject.fromobject(target.elementat(i).value);
//                var ln = new loadname();


//                ln.rate = datamanager.parsedouble(item["rate"]);
//                ln.symbol = datamanager.tostring(item["symbol"]);
//                ln.name = datamanager.tostring(item["name"]);
//                ln.fix_node = datamanager.parseint(item["fix_node"]);
//                ln.element = datamanager.parseint(item["element"]);
//                ln.fix_member = datamanager.parseint(item["fix_member"]);
//                ln.joint = datamanager.parseint(item["joint"]);

//                this.loadnames.add(index, ln);
//            }
//        }

//    }
//}