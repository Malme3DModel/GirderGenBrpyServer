using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Printing.PrintInput
{
    public class NoticePoint
    {
        public string m;   // 要素番号
        public double[] Points;
    }

}

        #region 印刷処理
        // タイトル
        private string title;
        // 2次元か3次元か
        private int dimension;
        // テーブル
        private Table myTable;

        #region 印刷処理
        // 節点情報
        private InputNode Node = null;


        /// <summary>
        /// 印刷前の初期化処理
        /// </summary>
        private void printInit(PdfDocument mc, PrintData data)
        {
            this.dimension = data.dimension;


            //テーブルの作成
            this.myTable = new Table(2, 12);

            // テーブルの幅
            this.myTable.ColWidth[0] = 15.0; // 格点No
            this.myTable.ColWidth[1] = 40.0;
            this.myTable.ColWidth[2] = this.myTable.ColWidth[1];
            this.myTable.ColWidth[3] = this.myTable.ColWidth[1];
            this.myTable.ColWidth[4] = this.myTable.ColWidth[1];
            this.myTable.ColWidth[5] = this.myTable.ColWidth[1];
            this.myTable.ColWidth[6] = this.myTable.ColWidth[1];
            this.myTable.ColWidth[7] = this.myTable.ColWidth[1];
            this.myTable.ColWidth[8] = this.myTable.ColWidth[1];
            this.myTable.ColWidth[9] = this.myTable.ColWidth[1];
            this.myTable.ColWidth[10] = this.myTable.ColWidth[1];
            this.myTable.ColWidth[11] = this.myTable.ColWidth[1];

            // 表題
            this.myTable[1, 2] = "L1";
            this.myTable[1, 3] = "L2";
            this.myTable[1, 4] = "L3";
            this.myTable[1, 5] = "L4";
            this.myTable[1, 6] = "L5";
            this.myTable[1, 7] = "L6";
            this.myTable[1, 8] = "L7";
            this.myTable[1, 9] = "L8";
            this.myTable[1, 10] = "L9";
            this.myTable[1, 11] = "L10";

            switch (data.language)
            {
                case "en":
                    this.title = "Location";
                    this.myTable[0, 0] = "Member";
                    this.myTable[1, 0] = "No";
                    this.myTable[1, 1] = "Distance";
                    break;

                case "cn":
                    this.title = "焦距点";
                    this.myTable[0, 0] = "构件";
                    this.myTable[1, 0] = "编码";
                    this.myTable[1, 1] = "构件长";
                    break;

            default:
                    this.title = "着目点データ";
                    this.myTable[0, 0] = "部材";
                    this.myTable[1, 0] = "No";
                    this.myTable[1, 1] = "部材長";
                    break;
            }
            this.myTable.AlignX[0, 0] = "L";    // 左寄せ
            this.myTable.AlignX[1, 0] = "L";    // 右寄せ
        }


        /// <summary>
        /// 1ページに入れるコンテンツを集計する 3次元の場合
        /// </summary>
        /// <param name="target">印刷対象の配列</param>
        /// <param name="rows">行数</param>
        /// <returns>印刷する用の配列</returns>
        private Table getPageContents3D(List<NoticePoint> target)
        {
            int r = this.myTable.Rows;
            int rows = target.Count;

            // 行コンテンツを生成
            var table = this.myTable.Clone();
            table.ReDim(row: r + rows);

            for (var i = 0; i < target.Count; i++)
            {

                NoticePoint item = target[i];

                for (var j = 0; j < 3; j++)
                {
                    table[r, 0] = printManager.toString(item.m);
                    table.AlignX[r, j] = "R";
                    j++;
                    if(item.m != null)
                    {
                        table[r, j] = printManager.toString(this.Member.GetMemberLength(item.m), 3);
                        table.AlignX[r, j] = "R";
                    }
                    j++;

                    var count = item.Points.Count();
                    var m = 0;
                    for (var k = 0; k < item.Points.Count(); k++)
                    {
                        if(item.Points.Count() > 10)
                        {
                            if(k % 10 == 0)
                            {
                                r++;
                                j = 2;
                                rows++;
                            }
                            table.ReDim(row: this.myTable.Rows + rows);
                        }
                        table[r, j] = printManager.toString(item.Points[k], 3);
                        table.AlignX[r, j] = "R";
                        j++;
                    }

                }
                r++;

            }
            table.RowHeight[2] = printManager.LineSpacing2; // 表題と body の間

            return table;
        }

        private List<NoticePoint> ReSize(List<NoticePoint> target)
        {
            int rows = target.Count;

            // 行コンテンツを生成
            var list = new List<NoticePoint>();

            var point_list1 = new List<Double>();
            var point_list2 = new List<Double>();

            foreach (Double Point in target[0].Points)
            {
                point_list1.Add(Point);
            }

            var list_rows = point_list1.Count/10 + 1;

            for (var i = 0; i < list_rows; i++ )
            {

                if (point_list1.Count == 0)
                    break;

                var _np = new NoticePoint();

                if(i == 0)
                {
                    _np.m = target[i].m;
                }

                for (int j = 0; j < 10; j++)
                {
                    point_list2.Add(point_list1.First());
                    point_list1.Remove(point_list1.First());

                    if (point_list1.Count == 0)
                        break;
                }

                _np.Points = point_list2.ToArray();
                point_list2.Clear();

                list.Add(_np);
            }

            return list;
        }

        /// <summary>
        /// 印刷する
        /// </summary>
        /// <param name="mc"></param>
        public void printPDF(PdfDocument mc, PrintData data)
        {
            if (this.noticepoints.Count == 0)
                return;

            // 要素を取得できる状態にする
            this.Member = (InputMember)data.printDatas[InputMember.KEY];

            // タイトル などの初期化
            this.printInit(mc, data);

            // 印刷可能な行数
            var printRows = myTable.getPrintRowCount(mc);

            // 行コンテンツを生成
            var page = new List<Table>();

            // 1ページ目に入る行数
            int rows = printRows[0];

            // 集計開始
            var tmp1 = new List<NoticePoint>(this.noticepoints); // clone

            var tmp3 = new List<NoticePoint>();
            var tmp4 = new List<NoticePoint>();

            while (true)
            {
                if (tmp4.Count <= 0 && rows > 0 && tmp1.Count <= 0)
                    break;

                // 1ページに納まる分のデータをコピー
                var tmp2 = new List<NoticePoint>();

                for (int i = 0; i < rows; i++)
                {
                    if (tmp1.Count > 0)
                    {
                        if (tmp1.First().Points.Count() >= 10)
                        {
                            tmp3.Add(tmp1.First());

                            tmp4 = this.ReSize(tmp3);//着目点を10ずつに分解する
                            tmp1.Remove(tmp1.First());
                            tmp3.Clear();
                        }
                        else
                        {
                            tmp4.Add(tmp1.First());
                            tmp1.Remove(tmp1.First());
                        }
                    }

                    if (tmp4.Count <= 0)
                        break;
                    tmp2.Add(tmp4.First());
                    tmp4.Remove(tmp4.First());
                }

                if (tmp2.Count > 0)
                {
                    var table = this.getPageContents3D(tmp2);
                    page.Add(table);
                }
                else if (tmp1.Count <= 0)
                {
                    break;
                }
                else
                { // 印刷するものもない
                    mc.NewPage();
                }

                // 2ページ以降に入る行数
                rows = printRows[1];
            }

            // 表の印刷
            printManager.printTableContents(mc, page, new string[] { this.title });

            #endregion
        
        }

    }


}
#endregion
