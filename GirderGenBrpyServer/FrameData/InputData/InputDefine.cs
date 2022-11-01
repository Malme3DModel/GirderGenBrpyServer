//using Newtonsoft.Json.Linq;
//using System.Collections.Generic;
//using System.Linq;

//namespace FrameData.InputData
//{
//    public class Define
//    {
//        public Dictionary<string, int> no = new Dictionary<string, int>();
//    }

//    internal class InputDefine
//    {
//        public const string KEY = "define";

//        private Dictionary<int, Define> defines = new Dictionary<int, Define>();

//        public InputDefine(Dictionary<string, object> value)
//        {
//            if (!value.ContainsKey(KEY))
//                return;

//            // データを取得する．
//            var target = JObject.FromObject(value[KEY]).ToObject<Dictionary<string, object>>();

//            // データを抽出する
//            for (var i = 0; i < target.Count; i++)
//            {
//                // defineNo
//                var key = target.ElementAt(i).Key; 
//                int index = dataManager.parseInt(key);

//                // define を構成する 基本荷重No群
//                var item = JObject.FromObject(target.ElementAt(i).Value).ToObject<Dictionary<string, object>>();

//                var _define = new Define();
//                for (int j = 0; j < item.Count; j++)
//                {
//                    var id = item.ElementAt(j).Key;  // "C1", "C2"...
//                    if (!id.Contains("C"))
//                        continue;

//                    int no = dataManager.parseInt(item.ElementAt(j).Value);

//                    _define.no.Add(id, no);
//                }
//                this.defines.Add(index, _define);
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


//        /// <summary>
//        /// 印刷前の初期化処理
//        /// </summary>
//        private void printInit(PdfDocument mc, PrintData data)
//        {
//            this.dimension = data.dimension;

//            //テーブルの作成
//            this.myTable = new Table(1, 11);

//            // テーブルの幅
//            this.myTable.ColWidth[0] = 30.0; // 格点No
//            this.myTable.ColWidth[1] = 50.0;
//            this.myTable.ColWidth[2] = this.myTable.ColWidth[1];
//            this.myTable.ColWidth[3] = this.myTable.ColWidth[1];
//            this.myTable.ColWidth[4] = this.myTable.ColWidth[1];
//            this.myTable.ColWidth[5] = this.myTable.ColWidth[1];
//            this.myTable.ColWidth[6] = this.myTable.ColWidth[1];
//            this.myTable.ColWidth[7] = this.myTable.ColWidth[1];
//            this.myTable.ColWidth[8] = this.myTable.ColWidth[1];
//            this.myTable.ColWidth[9] = this.myTable.ColWidth[1];
//            this.myTable.ColWidth[10] = this.myTable.ColWidth[1];


//            this.myTable[0, 0] = "DefineNo";
//            this.myTable[0, 1] = "C1";
//            this.myTable[0, 2] = "C2";
//            this.myTable[0, 3] = "C3";
//            this.myTable[0, 4] = "C4";
//            this.myTable[0, 5] = "C5";
//            this.myTable[0, 6] = "C6";
//            this.myTable[0, 7] = "C7";
//            this.myTable[0, 8] = "C8";
//            this.myTable[0, 9] = "C9";
//            this.myTable[0, 10] = "C10";

//            switch (data.language)
//            {
//                case "en":
//                    this.title = "Define DATA";
//                    break;

//                case "cn":
//                    this.title = "定义";
//                    break;

//                default:
//                    this.title = "Defineデータ";
//                    break;
//            }

//            this.myTable.AlignX[0, 0] = "L";    // 左寄せ
//            this.myTable.AlignX[0, 1] = "R";    // 右寄せ
//            this.myTable.AlignX[0, 2] = "R";    // 右寄せ
//            this.myTable.AlignX[0, 3] = "R";    // 右寄せ
//            this.myTable.AlignX[0, 4] = "R";    // 右寄せ
//            this.myTable.AlignX[0, 5] = "R";    // 右寄せ
//            this.myTable.AlignX[0, 6] = "R";    // 右寄せ
//            this.myTable.AlignX[0, 7] = "R";    // 右寄せ
//            this.myTable.AlignX[0, 8] = "R";    // 右寄せ
//            this.myTable.AlignX[0, 9] = "R";    // 右寄せ
//            this.myTable.AlignX[0, 10] = "R";    // 右寄せ

//        }


//        /// <summary>
//        /// 1ページに入れるコンテンツを集計する 3次元の場合
//        /// </summary>
//        /// <param name="target">印刷対象の配列</param>
//        /// <param name="rows">行数</param>
//        /// <returns>印刷する用の配列</returns>
//        private Table getPageContents3D(List<Define> target)
//        {
//            int r = this.myTable.Rows;
//            int rows = target.Count;

//            // 行コンテンツを生成
//            var table = this.myTable.Clone();
//            table.ReDim(row: r + rows);
//            table.RowHeight[1] = printManager.LineSpacing2; // 表題と body の間

//            for (var i = 0; i < target.Count; i++)
//            {
//                Define item = target[i];

//                for (var j = 0; j < 3; j++)
//                {
//                    table[r, 0] = printManager.toString(i+1);
//                    table.AlignX[r, j] = "R";
//                    j++;
//                    //for (var k = 0; k < item.no.Count; k++)
//                    //{
//                    //    table[r, j] = printManager.toString(item.no['C' + (1 + k).ToString()]);
//                    //    table.AlignX[r, j] = "R";
//                    //    j++;
//                    //}

//                    // 以下修正
//                    var count = item.no.Count();
//                    var m = 0;
//                    for (var k = 0; k < item.no.Count(); k++)
//                    {
//                        if (item.no.Count() > 10)
//                        {
//                            if (k % 10 == 0 && k != 0)
//                            {
//                                r++;
//                                j = 1;
//                                rows++;
//                            }
//                            table.ReDim(row: this.myTable.Rows + rows);
//                        }
//                        table[r, j] = printManager.toString(item.no['C' + (1 + k).ToString()], 3);
//                        table.AlignX[r, j] = "R";
//                        j++;
//                    }


//                }
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
//            if (this.defines.Count == 0)
//                return;

//            // タイトル などの初期化
//            this.printInit(mc, data);

//            // 印刷可能な行数
//            var printRows = myTable.getPrintRowCount(mc);

//            // 行コンテンツを生成
//            var page = new List<Table>();

//            // 1ページ目に入る行数
//            int rows = printRows[0];

//            var tmp1 = new List<Define>(); // clone
//            for (int i = 0; i < this.defines.Count; i++)
//            {
//                tmp1.Add(this.defines[i + 1]);
//            }

//            if (tmp1.Count > 0)
//            {
//                var table = this.getPageContents3D(tmp1);
//                page.Add(table);
//            }

//            // 印刷するものもない
//            mc.NewPage();

//            // 表の印刷
//            printManager.printTableContents(mc, page, new string[] { this.title });
//        }
//    }
//    #endregion

//}

