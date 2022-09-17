//using Newtonsoft.Json.Linq;
//using System;
//using System.IO;
//using System.Reflection;
//using System.Runtime.Serialization.Json;
//using System.Text.Json;
//using Newtonsoft.Json;
//using System.Linq;
//using System.Collections;
//using System.Collections.Generic;

//namespace PDF_Manager.Printing
//{
//    public class Shell
//    {
//        public string e;   // 材料
//        public double[] Points;
//    }


//    internal class InputShell
//    {
//        public const string KEY = "shell";

//        private Dictionary<string, Shell> shells = new Dictionary<string, Shell>();


//        public InputShell( Dictionary<string, object> value)
//        {
//            if (!value.ContainsKey(KEY))
//                return;

//            // memberデータを取得する
//            var target = JObject.FromObject(value[KEY]).ToObject<Dictionary<string, object>>();

//            // データを抽出する
//            for (var i = 0; i < target.Count; i++)
//            {
//                var key = target.ElementAt(i).Key;
//                var item = JObject.FromObject(target.ElementAt(i).Value);

//                var s = new Shell();
//                s.e = dataManager.toString(item["e"]);

//                // nodeデータを取得する
//                var itemPoints = item["nodes"];
//                var _points = new List<double>();
//                for (int j = 0; j < itemPoints.Count(); j++)
//                {
//                    var d = dataManager.parseDouble(itemPoints[j]);
//                    _points.Add(d);
//                }
//                s.Points = _points.ToArray();

//                this.shells.Add(key, s);
//            }
//        }
//        #region 印刷処理
//        // タイトル
//        private string title;
//        // 2次元か3次元か
//        private int dimension;
//        // テーブル
//        private Table myTable;
//        // 節点情報
//        private InputNode Node = null;
//        // 材料情報
//        private InputElement Element = null;


//        /// <summary>
//        /// 印刷前の初期化処理
//        /// </summary>
//        private void printInit(PdfDocument mc, PrintData data)
//        {
//            this.dimension = data.dimension;

//            if (this.dimension == 3)
//            {   // 3次元

//                //テーブルの作成
//                this.myTable = new Table(2, 6);

//                // テーブルの幅
//                this.myTable.ColWidth[0] = 30.0; // パネルNo
//                this.myTable.ColWidth[1] = 40.0; // 材料No
//                this.myTable.ColWidth[2] = 30.0; // 節点No1
//                this.myTable.ColWidth[3] = this.myTable.ColWidth[2]; // 節点No2
//                this.myTable.ColWidth[4] = this.myTable.ColWidth[2]; // 節点No3   
//                this.myTable.ColWidth[5] = this.myTable.ColWidth[2]; // 節点No4

//                switch (data.language)
//                {
//                    case "en":
//                        this.title = "Member Data";
//                        this.myTable[1, 0] = "No";
//                        this.myTable[0, 2] = "　Node";
//                        this.myTable[1, 1] = "Node-I";
//                        this.myTable[1, 2] = "Node-J";
//                        this.myTable[0, 3] = "Distance";
//                        this.myTable[1, 3] = "(m)";
//                        this.myTable[0, 4] = "Material";
//                        this.myTable[1, 4] = "No";
//                        this.myTable[0, 5] = "Angle of";
//                        this.myTable[1, 5] = "Rotation";
//                        this.myTable[0, 6] = "    Name of Material";
//                        break;

//                    case "cn":
//                        this.title = "构件";
//                        this.myTable[1, 0] = "编码";
//                        this.myTable[0, 2] = "　节点";
//                        this.myTable[1, 1] = "I端";
//                        this.myTable[1, 2] = "J端";
//                        this.myTable[0, 3] = "构件长";
//                        this.myTable[1, 3] = "(m)";
//                        this.myTable[0, 4] = "材料编码";
//                        this.myTable[0, 5] = "转动角度";
//                        this.myTable[1, 5] = "(°)";
//                        this.myTable[0, 6] = "    材料名称";
//                        break;

//                    default:
//                        this.title = "パネルデータ";
//                        this.myTable[0, 0] = "Panel";
//                        this.myTable[1, 0] = "No";
//                        this.myTable[0, 1] = "材料";
//                        this.myTable[1, 1] = "No";
//                        this.myTable[0, 4] = "節点 ";
//                        break;
//                }

//                // 表題の文字位置
//                this.myTable.AlignX[0, 1] = "R";    // 右寄せ
//                //this.myTable.AlignX[0, 3] = "L";    // 左寄せ
//                this.myTable.AlignX[0, 5] = "R";    // 右寄せ
//                this.myTable.AlignX[1, 0] = "R";    // 右寄せ
//                this.myTable.AlignX[1, 1] = "R";    // 右寄せ
//                this.myTable.AlignX[1, 2] = "R";    // 右寄せ
//                this.myTable.AlignX[1, 3] = "R";    // 右寄せ
//                this.myTable.AlignX[1, 4] = "R";    // 右寄せ
//                this.myTable.AlignX[1, 5] = "R";    // 右寄せ
//            }
//        }


//        /// <summary>
//        /// 1ページに入れるコンテンツを集計する
//        /// </summary>
//        /// <param name="target">印刷対象の配列</param>
//        /// <param name="rows">行数</param>
//        /// <returns>印刷する用の配列</returns>
//        private Table getPageContents(Dictionary<string, Shell> target)
//        {
//            int r = this.myTable.Rows;
//            int rows = target.Count;

//            // 行コンテンツを生成
//            var table = this.myTable.Clone();
//            table.ReDim(row: r + rows);

//            table.RowHeight[r] = printManager.LineSpacing2;

//            for (var i = 0; i < rows; i++)
//            {
//                string No = target.ElementAt(i).Key;
//                Shell item = target.ElementAt(i).Value;

//                var roop_count = item.Points.Count();
//                var list = new List<Double>();

//                int j = 0;
//                table[r, j] = No;
//                table.AlignX[r, j] = "R";
//                j++;

//                table[r, j] = printManager.toString(item.e);
//                table.AlignX[r, j] = "R";
//                j = j+2;

//                foreach(var point in item.Points)
//                {
//                    list.Add(point);
//                }

//                table[r, j] = printManager.toString(string.Join(", ", list));
//                table.AlignX[r, j] = "L";
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
//            if (this.shells.Count == 0)
//                return;

//            if (dimension != 3)
//                return;

//            // タイトル などの初期化
//            this.printInit(mc, data);

//            // 印刷可能な行数
//            var printRows = myTable.getPrintRowCount(mc);

//            // 行コンテンツを生成
//            var page = new List<Table>();

//            // 1ページ目に入る行数
//            int rows = printRows[0];

//            // 集計開始
//            var tmp1 = new Dictionary<string, Shell>(this.shells); // clone
//            while (true)
//            {
//                // 1ページに納まる分のデータをコピー
//                var tmp2 = new Dictionary<string, Shell>();
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

//        #endregion


//        /*
//        private Dictionary<string, object> value = new Dictionary<string, object>();
//        List<List<string[]>> data = new List<List<string[]>>();

//        public void init(PdfDoc mc, Dictionary<string, object> value_)
//        {
//            value = value_;

//            var target = JObject.FromObject(value["shell"]).ToObject<Dictionary<string, object>>();

//            // 集まったデータはここに格納する
//            data = new List<List<string[]>>();
//            List<string[]> body = new List<string[]>();

//            for (int i = 0; i < target.Count; i++)
//            {
//                var item = JObject.FromObject(target.ElementAt(i).Value);

//                string[] line = new String[5];
//                line[0] = dataManager.TypeChange(item["e"]);

//                int count = 0;
//                var itemPoints = item["nodes"];

//                for (int j = 0; j < itemPoints.Count(); j++)
//                {
//                    line[count + 1] = dataManager.TypeChange(itemPoints[count]);
//                    count++;
//                    if (count == 4)
//                    {
//                        body.Add(line);
//                        count = 0;
//                        line = new string[5];
//                        line[0] = "";
//                    }
//                }
//                if (count > 0)
//                {
//                    for (int k = 1; k < 5; k++)
//                    {
//                        line[k] = line[k] == null ? "" : line[k];
//                    }

//                    body.Add(line);
//                }
//                if (body.Count > 0)
//                {
//                    data.Add(body);
//                }
//            }
//            data.Add(body);
//        }
//        */

//        /*
//        public void ShellPDF(PdfDoc mc)
//        {
//            int bottomCell = mc.bottomCell;

//            // 全行の取得
//            int count = 20;
//            for (int i = 0; i < data.Count; i++)
//            {
//                count += (data[i].Count + 2) * mc.single_Yrow;
//            }
//            // 改ページ判定
//            mc.DataCountKeep(count);

//            //　ヘッダー
//            string[,] header_content = {
//                { "材料", "", "頂点No", "", ""},
//                { "No", "1", "2", "3", "4"}
//            };

//            switch (mc.language)
//            {
//                case "ja":
//                    mc.PrintContent("パネルデータ", 0);
//                    break;
//                case "en":
//                    mc.PrintContent("Plates Data", 0);
//                    header_content[0, 0] = "Material";
//                    header_content[0, 2] = "Node No.";
//                    break;
//            }

//            mc.CurrentRow(2);
//            mc.CurrentColumn(0);

//            // ヘッダーのx方向の余白
//            int[,] header_Xspacing = {
//                 { 10, 70, 175, 210, 280 },
//                 { 10, 70, 140, 210, 280 } 
//            };

//            mc.Header(header_content, header_Xspacing);

//            // ボディーのx方向の余白
//            int[,] body_Xspacing = { { 17, 78, 148, 218,288 } };

//            for (int i = 0; i < data.Count; i++)
//            {
//                for (int j = 0; j < data[i].Count; j++)
//                {
//                    for (int l = 0; l < data[i][j].Length; l++)
//                    {
//                        mc.CurrentColumn(body_Xspacing[0, l]); //x方向移動
//                        mc.PrintContent(data[i][j][l]);  // print
//                    }
//                    if (!(i == data.Count - 1 && j == data[i].Count - 1))
//                    {
//                        mc.CurrentRow(1); // y方向移動
//                    }
//                }
//            }

//        }
//        */
//    }
//}

