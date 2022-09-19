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

//        ///印刷処理

//        ///タイトル
//        private string title;
//        ///２次元か３次元か
//        private int dimension;
//        ///テーブル
//        private Table myTable;
//        ///節点情報
//        private InputNode Node = null;
//        ///材料情報
//        private InputElement Element = null; 


//        ///印刷前の初期化処理
//        ///
//        private void printInit(PdfDocument mc, PrintData data)
//        {
//            this.dimension = data.dimension;

//            ///テーブルの作成
//            this.myTable = new Table(2, 9);

//            ///テーブルの幅
//            this.myTable.ColWidth[0] = 45.0;//Case
//            this.myTable.ColWidth[1] = 60.0;//割増係数
//            this.myTable.ColWidth[2] = 30.0;//記号
//            this.myTable.ColWidth[3] = 240.0;//荷重名称
//            this.myTable.ColWidth[4] = 25.0;//支点
//            this.myTable.ColWidth[5] = 25.0;//断面
//            this.myTable.ColWidth[6] = 25.0;//部材
//            this.myTable.ColWidth[7] = 25.0;//地盤

//            switch (data.language)
//            {
//                case "en":
//                    this.title = "Basic Case";
//                    this.myTable[0, 0] = "Case";
//                    this.myTable[1, 0] = "No";
//                    this.myTable[1, 1] = "C.F";
//                    this.myTable[1, 2] = "Symbol";
//                    this.myTable[1, 3] = "Load name";
//                    this.myTable[0, 4] = "　　Structural conditions";
//                    this.myTable[1, 4] = "Support";
//                    this.myTable[1, 5] = "Section";
//                    this.myTable[1, 6] = "Spring";
//                    this.myTable[1, 7] = "Joint";
//                    break;

//                case "cn":
//                    this.title = "基本载重案例";
//                    this.myTable[0, 0] = "Case";
//                    this.myTable[1, 0] = "No";
//                    this.myTable[1, 1] = "附加系数";
//                    this.myTable[1, 2] = "符号";
//                    this.myTable[1, 3] = "载重名称";
//                    this.myTable[0, 4] = "　　结构条件";
//                    this.myTable[1, 4] = "支点";
//                    this.myTable[1, 5] = "截面";
//                    this.myTable[1, 6] = "弹簧";
//                    this.myTable[1, 7] = "连接";
//                    break;

//                default:
//                    this.title = "基本荷重DATA";
//                    this.myTable[0, 0] = "Case";
//                    this.myTable[1, 0] = "No";
//                    this.myTable[1, 1] = "割増係数";
//                    this.myTable[1, 2] = "記号";
//                    this.myTable[1, 3] = "荷重名称";
//                    this.myTable[0, 4] = "　　構造系条件";
//                    this.myTable[1, 4] = "支点";
//                    this.myTable[1, 5] = "断面";
//                    this.myTable[1, 6] = "バネ";
//                    this.myTable[1, 7] = "結合";
//                    break;
//            }

//            //表題の文字位置
//            this.myTable.AlignX[0, 5] = "L";    // 左寄せ
//        }

//        /// <summary>
//        /// 1ページに入れるコンテンツを集計する
//        /// </summary>
//        /// <param name="target">印刷対象の配列</param>
//        /// <param name="rows">行数</param>
//        /// <returns>印刷する用の配列</returns>
//        private Table getPageContents(Dictionary<int, LoadName> target)
//        {
//            int r = this.myTable.Rows;
//            int rows = target.Count;

//            // 行コンテンツを生成
//            var table = this.myTable.Clone();
//            table.ReDim(row: r + rows);

//            table.RowHeight[r] = printManager.LineSpacing2;

//            for (var i = 0; i < rows; i++)
//            {
//                int No = target.ElementAt(i).Key;
//                LoadName item = target.ElementAt(i).Value;

//                int j = 0;
//                table[r, j] = No.ToString();
//                table.AlignX[r, j] = "R";
//                j++;
//                table[r, j] = printManager.toString(item.rate, 3);
//                ///table.AlignX[r, j] = "R";
//                j++;
//                table[r, j] = printManager.toString(item.symbol);
//                table.AlignX[r, j] = "L";
//                j++;
//                table[r, j] = printManager.toString(item.name);
//                table.AlignX[r, j] = "L";
//                j++;
//                table[r, j] = printManager.toString(item.fix_node);
//                j++;
//                table[r, j] = printManager.toString(item.element);
//                j++;
//                table[r, j] = printManager.toString(item.fix_member);
//                j++;
//                table[r, j] = printManager.toString(item.joint);
//                j++;

//                r++;
//            }

//            return table;
//        }

//        /// <summary>
//        /// 印刷する
//        /// </summary>
//        /// <param name="mc"></param>
//        public void printPDF(PdfDocument mc, PrintData data)
//        {

//            // タイトル などの初期化
//            this.printInit(mc, data);

//            // 印刷可能な行数
//            var printRows = myTable.getPrintRowCount(mc);

//            // 行コンテンツを生成
//            var page = new List<Table>();

//            // 1ページ目に入る行数
//            int rows = printRows[0];

//            // 集計開始
//            var tmp1 = new Dictionary<int, LoadName>(this.loadnames); // clone
//            while (true)
//            {
//                // 1ページに納まる分のデータをコピー
//                var tmp2 = new Dictionary<int, LoadName>();
//                for (int i = 0; i < rows; i++)
//                {
//                    if (tmp1.Count <= 0)
//                        break;
//                    tmp2.Add(tmp1.First().Key, tmp1.First().Value);
//                    tmp1.Remove(tmp1.First().Key);
//                }

//                if (tmp2.Count > 0)
//                {
//                    var table = this.getPageContents(tmp2);
//                    page.Add(table);
//                }
//                else if (tmp1.Count <= 0)
//                {
//                    break;
//                }
//                else
//                { // 印刷するものもない
//                    mc.NewPage();
//                }

//                // 2ページ以降に入る行数
//                rows = printRows[1];
//            }

//            // 表の印刷
//            printManager.printTableContents(mc, page, new string[] { this.title });
//        }

//    }
//}