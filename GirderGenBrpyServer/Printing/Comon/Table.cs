using PdfSharpCore.Drawing;
using System;
using System.Collections.Generic;

namespace Printing.Comon
{
    public class Table
    {
        #region This Class Palamator
        /*
         * Cols=5 →       0             1             2             3             4...
         * Rows=4
         * ↓     Point[0, 0]   Point[0, 1]   Point[0, 2]   Point[0, 3]   Point[0, 4]   Point[0, 5]
         *           ・──────・──────・──────・──────・──────・
         *  0        │  Cell[0,0] │  Cell[0,1] │  Cell[0,2] │  Cell[0,3] │  Cell[0,4] │
         *           │ Align[0,0] │ Align[0,1] │ Align[0,2] │ Align[0,3] │ Align[0,4] │　　　　
         *           ├──────┼──────┼──────┼──────┼──────┤
         *  1        │  Cell[1,0] │  Cell[1,1] │  Cell[1,2] │  Cell[1,3] │  Cell[1,4] │
         *           │ Align[1,0] │ Align[1,1] │ Align[1,2] │ Align[1,3] │ Align[1,4] │　　　　
         *           ├──────┼──────┼──────┼──────┼──────┤
         *  2        │  Cell[2,0] │  Cell[2,1] │  Cell[2,2] │  Cell[2,3] │  Cell[2,4] │
         *           │ Align[2,0] │ Align[2,1] │ Align[2,2] │ Align[2,3] │ Align[2,4] │　　　　
         *           ├──────┼──────┼──────┼──────┼──────┤
         *  3        │  Cell[3,0] │  Cell[3,1] │  Cell[3,2] │  Cell[3,3] │  Cell[3,4] │
         *  :        │ Align[3,0] │ Align[3,1] │ Align[3,2] │ Align[3,3] │ Align[3,4] │　　　　
         *           ・──────・──────・──────・──────・──────・
         *        Point[4, 0]   Point[4, 1]   Point[4, 2]   Point[4, 3]   Point[4, 4]   Point[4, 5]
         *                                                                              
         *
         *           ┌-HolLW[0,0]-┬-HolLW[0,1]-┬-HolLW[0,2]-┬-HolLW[0,3]-┬-HolLW[0,4]-┐
         *           │            │            │            │            │            │　　　　
         * VtcLW[0,0]┥  VtcLW[0,1]┥  VtcLW[0,2]┥  VtcLW[0,3]┥  VtcLW[0,4]┥  VtcLW[0,5]┥　RowHeight[0] 　　　　
         *           │            │            │            │            │            │　　　　
         *           ├-HolLW[1,0]-┼-HolLW[1,1]-┼-HolLW[1,2]-┼-HolLW[1,3]-┼-HolLW[1,4]-┤
         *           │            │            │            │            │            │　　　　
         * VtcLW[1,0]┥  VtcLW[1,1]┥  VtcLW[1,2]┥  VtcLW[1,3]┥  VtcLW[1,4]┥  VtcLW[1,5]┥　RowHeight[1] 　　　　
         *           │            │            │            │            │            │　　　　
         *           ├-HolLW[2,0]-┼-HolLW[2,1]-┼-HolLW[2,2]-┼-HolLW[2,3]-┼-HolLW[2,4]-┤
         *           │            │            │            │            │            │　　　　
         * VtcLW[2,0]┥  VtcLW[2,1]┥  VtcLW[2,2]┥  VtcLW[2,3]┥  VtcLW[2,4]┥  VtcLW[2,5]┥　RowHeight[2] 　　　　
         *           │            │            │            │            │            │　　　　
         *           ├-HolLW[3,0]-┼-HolLW[3,1]-┼-HolLW[3,2]-┼-HolLW[3,3]-┼-HolLW[3,4]-┤
         *           │            │            │            │            │            │　　　　
         * VtcLW[3,0]┥  VtcLW[3,1]┥  VtcLW[3,2]┥  VtcLW[3,3]┥  VtcLW[3,4]┥  VtcLW[3,5]┥　RowHeight[3]　　　　
         *           │            │            │            │            │            │　　　　
         *           └-HolLW[4,0]-┴-HolLW[4,1]-┴-HolLW[4,2]-┴-HolLW[4,3]-┴-HolLW[4,4]-┘
         * 
         *           │            │            │            │            │            │　　　　
         *           └──────┴──────┴──────┴──────┴──────┘
         *              ColWidth[0]   ColWidth[1]   ColWidth[2]   ColWidth[3]   ColWidth[4]
         */
        #endregion

        private string[,] Cell;
        private int CellRows;
        private int CellCols;

        public int Rows { get { return CellRows; } }
        public int Columns { get { return CellCols; } }

        public object Alignx { get; internal set; }

        public string[,] AlignY;    // T:Top, B:Bottm, C:Center
        public string[,] AlignX;    // R:Right, L:Left, C:Center
        public double[,] HolLW;     // Holizonal Line Width
        public double[,] VtcLW;     // Vertical  Line Width
        public double[] RowHeight;
        public double[] ColWidth;

        // 印刷に関する設定
        public double LineSpacing3; // 小さい（テーブル内などの）改行高さ pt ポイント


        private const double DEFAULT_LINE_WIDTH = 0; // デフォルトの線幅

        public Table(int _rows, int _cols)
        {
            // 印刷に関する初期設定
            LineSpacing3 = printManager.FontHeight;

            // 表題部分を初期化する
            CellRows = 0;
            CellCols = 0;
            ReDim(_rows, _cols);
        }

        /// <summary>
        /// 行を追加する
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="col">列数</param>
        public void ReDim(int row = -1, int col = -1)
        {
            if (row < 0)
                row = Rows;
            if (col < 0)
                col = Columns;


            // 昔の情報を取っておく
            var oldCell = Cell != null ? Cell.Clone() as string[,] : null;
            var oldAlignX = AlignX != null ? AlignX.Clone() as string[,] : null;
            var oldAlignY = AlignY != null ? AlignY.Clone() as string[,] : null;
            var oldRowHeight = RowHeight != null ? RowHeight.Clone() as double[] : null;
            var oldColWidth = ColWidth != null ? ColWidth.Clone() as double[] : null;
            var oldHolLW = HolLW != null ? HolLW.Clone() as double[,] : null;
            var oldVtcLW = VtcLW != null ? VtcLW.Clone() as double[,] : null;

            CellRows = Math.Min(row, Rows);
            CellCols = Math.Min(col, CellCols);

            //** Init Cell
            Cell = new string[row, col];
            for (int i = 0; i < CellRows; ++i)
                for (int j = 0; j < CellCols; ++j)
                    Cell[i, j] = oldCell[i, j];

            //** Init Align
            AlignX = new string[row, col];
            for (int i = 0; i < CellRows; ++i)
                for (int j = 0; j < CellCols; ++j)
                    AlignX[i, j] = oldAlignX[i, j];

            AlignY = new string[row, col];
            for (int i = 0; i < CellRows; ++i)
                for (int j = 0; j < CellCols; ++j)
                    AlignY[i, j] = oldAlignY[i, j];

            //** Init RowHeight
            RowHeight = new double[row];
            for (int i = 0; i < CellRows; ++i)
                RowHeight[i] = oldRowHeight[i];

            //** Init ColWidth
            ColWidth = new double[col];
            for (int j = 0; j < CellCols; ++j)
                ColWidth[j] = oldColWidth[j];

            //** Init Holizonal Line Width
            HolLW = new double[row + 1, col];
            for (int i = 0; i < CellRows; ++i)
                SetHolLW(i, HolLW[i, 0]);
            for (int i = CellRows; i <= row; ++i)
                for (int j = 0; j < col; ++j)
                    HolLW[i, j] = DEFAULT_LINE_WIDTH;

            //** Init Vertical Line Width
            VtcLW = new double[row, col + 1];
            for (int j = 0; j < CellCols; ++j)
                SetVtcLW(j, VtcLW[0, j]);
            for (int j = CellCols; j <= col; ++j)
                for (int i = 0; i < row; ++i)
                    VtcLW[i, j] = DEFAULT_LINE_WIDTH;

            //** Row, Col Count
            CellRows = row;
            CellCols = col;

        }

        /// <summary>
        /// セルの値を取得・設定する
        /// </summary>
        /// <param name="row">行番号 0～</param>
        /// <param name="col">列番号 0～</param>
        /// <returns></returns>
        public string this[int row, int col]
        {
            set
            {
                Cell[row, col] = value;
            }
            get
            {
                try
                {
                    return Cell[row, col];
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 現在の Table の簡易コピーを作成します。
        /// </summary>
        /// <returns></returns>
        public Table Clone()
        {
            // MemberwiseCloneメソッドを使用
            return (Table)MemberwiseClone();
        }

        /// <summary>
        /// テーブルの高さ
        /// </summary>
        /// <returns>pt</returns>
        public double GetTableHeight()
        {
            double result = 0;
            for (int i = 0; i < CellRows; ++i)
                if (RowHeight[i] == double.NaN || RowHeight[i] <= 0)
                    result += LineSpacing3;
                else
                    result += RowHeight[i];
            return result;
        }

        /// <summary>
        /// テーブルの幅
        /// </summary>
        /// <returns>pt</returns>
        public double GetTableWidth()
        {
            double result = 0;
            for (int i = 0; i < CellCols; ++i)
                if (ColWidth[i] == double.NaN || ColWidth[i] <= 0)
                    result += LineSpacing3;
                else
                    result += ColWidth[i];
            return result;
        }

        /// <summary>
        /// 横罫線幅の設定
        /// </summary>
        /// <param name="row">行番号</param>
        /// <param name="LineWidth">線幅</param>
        public void SetHolLW(int row, double LineWidth)
        {
            for (int j = 0; j < Columns; ++j)
                HolLW[row, j] = LineWidth;
        }

        /// <summary>
        /// 縦罫線幅の設定
        /// </summary>
        /// <param name="col">列番号</param>
        /// <param name="LineWidth">線幅</param>
        public void SetVtcLW(int col, double LineWidth)
        {
            for (int i = 0; i < Rows; ++i)
                VtcLW[i, col] = LineWidth;
        }

        /// <summary>
        /// 何行印刷できるか調べる
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="H1">デフォルト（改ページした場合）の印字位置</param>
        /// <returns>
        /// return[0] = 1ページ目の印刷可能行数, 
        /// return[1] = 2ページ目以降の印刷可能行数
        /// </returns>
        internal int[] getPrintRowCount(PdfDocument mc, int titles_count = 1)
        {
            // 表題の印字高さ + 改行高
            double H2 = GetTableHeight();

            // 1行当りの高さ + 改行高
            double H3 = LineSpacing3;


            //// 2ページ目以降（ページ全体を使ってよい場合）の行数
            //double Hx = mc.currentPageSize.Height;
            //// 高さはタイトルの分だけ小さくなる
            //Hx -= printManager.titlePos.Y;
            //Hx -= printManager.FontHeight;
            //Hx -= printManager.LineSpacing2;
            //Hx -= printManager.H1;
            //Hx -= H2;

            //int rows2 = (int)(Hx / H3); // 切り捨て

            //Hx = mc.currentPageSize.Height;
            //// 1ページ目（現在位置から）の行数
            //Hx -= mc.currentPos.Y;
            //// 高さはマージンの分だけ小さくなる
            //Hx -= mc.Margine.Top;
            //// 高さはタイトルの分だけ小さくなる
            //Hx -= printManager.titlePos.Y;
            //Hx -= printManager.FontHeight;
            //Hx -= printManager.LineSpacing2;

            //int rows1 = (int)(Hx / H3); // 切り捨て
            double SettingHeight = mc.Margine.Top;
            SettingHeight += printManager.titlePos.Y;
            SettingHeight += printManager.FontHeight;
            SettingHeight += printManager.LineSpacing2;

            //２枚目以降の時
            double Hx = mc.currentPageSize.Height;

            // mc.currentPos.Yの再現
            Hx -= SettingHeight;
            // 高さはマージンの分だけ小さくなる
            Hx -= mc.Margine.Bottom;

            int rows2 = (int)(Hx / H3); // 切り捨て

            //１枚目の時
            Hx = mc.currentPageSize.Height;
            // 1ページ目（現在位置から）の行数
            Hx -= mc.currentPos.Y;
            // 高さはマージンの分だけ小さくなる
            Hx -= mc.Margine.Bottom;

            //if (mc.currentPos.Y > SettingHeight)
            //{
            //    Hx -= printManager.LineSpacing1;
            //}

            int rows1 = (int)(Hx / H3); // 切り捨て

            return new int[] { rows1, rows2 };
        }


        /// <summary>
        /// 印刷する
        /// </summary>
        /// <param name="_mc"></param>
        internal void PrintTable(PdfDocument _mc)
        {
            #region Base Info

            XSize[,] textSize1 = new XSize[Rows, Columns];
            for (int i = 0; i < CellRows; ++i)
                for (int j = 0; j < CellCols; ++j)
                {
                    if (Cell[i, j] == null)
                        continue;
                    textSize1[i, j] = _mc.MeasureString(Cell[i, j]);
                }

            ///////////////////////////////////////////////
            for (int i = 0; i < CellRows; ++i)
            {
                if (RowHeight[i] == double.NaN || RowHeight[i] <= 0)
                    RowHeight[i] = LineSpacing3;
            }

            ///////////////////////////////////////////////
            for (int j = 0; j < CellCols; ++j)
            {
                if (ColWidth[j] == double.NaN || ColWidth[j] <= 0)
                    for (int i = 0; i < CellRows; ++i)
                        ColWidth[j] = Math.Max(ColWidth[j], textSize1[i, j].Width);
            }
            ///////////////////////////////////////////////
            XPoint[,] point = new XPoint[CellRows + 1, CellCols + 1];
            try
            {
                double y1 = _mc.currentPos.Y;
                for (int i = 0; i <= CellRows; ++i)
                {
                    if (0 < i)
                        y1 += RowHeight[i - 1];

                    double x1 = _mc.currentPos.X;
                    for (int j = 0; j <= CellCols; ++j)
                    {
                        if (0 < j)
                            x1 += ColWidth[j - 1];
                        point[i, j].Y = y1;
                        point[i, j].X = x1;
                    }
                }
            }
            catch { Text.PrtText(_mc, "Error: PrintTable() - Base Info"); }
            #endregion

            #region Draw Lines
            try
            {
                int i, j;
                try
                {
                    for (i = 0; i < CellRows; ++i)
                        for (j = 0; j < CellCols; ++j)
                            if (HolLW[i, j] != double.NaN)
                                if (0 < HolLW[i, j])
                                    Shape.DrawLine(_mc, point[i, j], point[i, j + 1], HolLW[i, j]);

                    for (i = 0; i < CellRows; ++i)
                        for (j = 0; j < CellCols; ++j)
                            if (VtcLW[i, j] != double.NaN)
                                if (0 < VtcLW[i, j])
                                    Shape.DrawLine(_mc, point[i, j], point[i + 1, j], VtcLW[i, j]);
                }
                catch
                {
                    Text.PrtText(_mc, "Error: PrintTable() - Draw Lines");
                }
            }
            catch { }
            #endregion

            #region Draw String
            try
            {
                for (int i = 0; i < CellRows; ++i)
                {
                    for (int j = 0; j < CellCols; ++j)
                    {
                        if (Cell[i, j] == null)
                            continue;

                        if (Cell[i, j].Length == 0)
                            continue;

                        double XPos = 0;
                        double YPos = 0;

                        XStringFormat align;

                        switch (AlignY[i, j])
                        {
                            case "T":
                                YPos = point[i, j].Y;
                                break;
                            case "C":
                                YPos = (point[i, j].Y + point[i + 1, j].Y) / 2;
                                break;
                            default:
                                YPos = point[i + 1, j].Y;
                                break;
                        }
                        switch (AlignX[i, j])
                        {
                            case "L":
                                XPos = point[i, j].X;
                                switch (AlignY[i, j])
                                {
                                    case "T":
                                        align = XStringFormats.TopLeft;
                                        break;
                                    case "C":
                                        align = XStringFormats.CenterLeft;
                                        break;
                                    default:
                                        align = XStringFormats.BottomLeft;
                                        break;
                                }
                                break;
                            case "R":
                                XPos = point[i, j + 1].X;
                                switch (AlignY[i, j])
                                {
                                    case "T":
                                        align = XStringFormats.TopRight;
                                        break;
                                    case "C":
                                        align = XStringFormats.CenterRight;
                                        break;
                                    default:
                                        align = XStringFormats.BottomRight;
                                        break;
                                }
                                break;
                            default: // case "C"
                                XPos = (point[i, j].X + point[i, j + 1].X) / 2;
                                switch (AlignY[i, j])
                                {
                                    case "T":
                                        align = XStringFormats.TopCenter;
                                        break;
                                    case "C":
                                        align = XStringFormats.Center;
                                        break;
                                    default:
                                        align = XStringFormats.BottomCenter;
                                        break;
                                }
                                break;
                        }
                        _mc.currentPos.X = XPos;
                        _mc.currentPos.Y = YPos;
                        Text.PrtText(_mc, Cell[i, j], align: align);
                    }
                }
            }
            catch { Text.PrtText(_mc, "Error: PrintTable() - Draw String"); }
            #endregion

            _mc.currentPos.X = point[CellRows, CellCols].X;
            _mc.currentPos.Y = point[CellRows, CellCols].Y;
        }

        /// <summary>
        /// テーブルにデータを追加する
        /// </summary>
        /// <param name="target"></param>
        public void Add(Table target)
        {
            var oldRowsCount = Rows;
            var newRowsCount = Rows + target.Rows;
            ReDim(row: newRowsCount);

            for (int r = oldRowsCount; r < newRowsCount; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    this[r, c] = target[r - oldRowsCount, c];
                    AlignX[r, c] = target.AlignX[r - oldRowsCount, c];
                    RowHeight[r] = target.RowHeight[r - oldRowsCount];
                }
            }

            RowHeight[oldRowsCount] = printManager.LineSpacing1;
        }

        /// <summary>
        /// 行を削除する
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="col">列数</param>
        public void ClearDraft()
        {
            // 昔の情報を取っておく
            var oldCell = Cell != null ? Cell.Clone() as string[,] : null;
            var oldAlignX = AlignX != null ? AlignX.Clone() as string[,] : null;
            var oldAlignY = AlignY != null ? AlignY.Clone() as string[,] : null;
            var oldRowHeight = RowHeight != null ? RowHeight.Clone() as double[] : null;
            var oldColWidth = ColWidth != null ? ColWidth.Clone() as double[] : null;
            var oldHolLW = HolLW != null ? HolLW.Clone() as double[,] : null;
            var oldVtcLW = VtcLW != null ? VtcLW.Clone() as double[,] : null;

            var oldRows = Rows;
            var oldCols = Columns;

            //** Init Cell
            Cell = new string[oldRows, oldCols];
            AlignX = new string[oldRows, oldCols];
            AlignY = new string[oldRows, oldCols];
            RowHeight = new double[oldRows];
            ColWidth = new double[oldCols];
            HolLW = new double[oldRows + 1, oldCols];
            VtcLW = new double[oldRows, oldCols + 1];

            var nullnumber = new List<int>();

            for (int i = 0; i < CellRows; ++i)// 消した時の行数の算定
            {
                int nullcount = 0;

                for (int j = 0; j < CellCols; ++j)
                {
                    Cell[i, j] = oldCell[i, j];
                    if (i == 0)
                    {

                    }
                    else if (Cell[i, j] == "" || Cell[i, j] == null)
                    {
                        nullcount++;
                    }
                }

                if (nullcount == oldCols)
                {
                    nullnumber.Add(i);
                }
            }

            for (int j = 0; j < CellCols; ++j)
            {
                ColWidth[j] = oldColWidth[j];
                SetVtcLW(j, VtcLW[0, j]);
            }

            //** Init Cell

            int newRows = oldRows - nullnumber.Count;

            Cell = new string[newRows, oldCols];
            AlignX = new string[newRows, oldCols];
            AlignY = new string[newRows, oldCols];
            RowHeight = new double[newRows];
            ColWidth = new double[oldCols];
            HolLW = new double[newRows, oldCols];
            VtcLW = new double[newRows, oldCols];

            //** Row, Col Count
            CellRows = newRows;
            CellCols = oldCols;

            int _nullcount = 0;

            for (int i = 0; i < newRows; ++i)// 白紙を詰めたTableの作成
            {
                for (int j = 0; j < CellCols; ++j)
                {
                    if (nullnumber.Count > 0 && _nullcount < nullnumber.Count)
                    {
                        while (i + _nullcount == nullnumber[_nullcount])
                        {
                            _nullcount++;
                            if (nullnumber.Count == _nullcount)
                                break;
                        }
                    }
                    Cell[i, j] = oldCell[i + _nullcount, j];
                    AlignX[i, j] = oldAlignX[i + _nullcount, j];
                    AlignY[i, j] = oldAlignY[i + _nullcount, j];
                    HolLW[i, j] = oldHolLW[i + _nullcount, j];
                    VtcLW[i, j] = oldVtcLW[i + _nullcount, j];

                    if (j == 0)
                    {
                        RowHeight[i] = oldRowHeight[i + _nullcount];
                        SetHolLW(i, HolLW[i, 0]);
                    }
                }
            }

            for (int j = 0; j < CellCols; ++j)
            {
                ColWidth[j] = oldColWidth[j];
                SetVtcLW(j, VtcLW[0, j]);
            }
        }

        /// <summary>
        /// Tableを入りきるように分割する
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="col">列数</param>
        internal List<Table> SplitTable(int rows, Table myTable)
        {
            var _page = new List<Table>();

            var tmp1 = Clone();

            var roop_count = tmp1.Rows / rows + 1;

            var Rows = 0;

            for (int i = 0; i < roop_count; ++i)
            {
                if (tmp1.Rows <= 0)
                    break;

                var tmp2 = myTable.Clone();

                var r = tmp2.Rows;

                tmp2.ReDim(row: r + rows);

                var _r = myTable.Rows;

                if (i == 0)
                {
                    tmp2[0, 0] = tmp1[0, 0];
                }

                for (int ii = 0; ii < rows; ii++)
                {
                    for (int c = 0; c < tmp1.Columns; c++)
                    {
                        var range = tmp1[Rows + _r, c];
                        if (range == null)
                            continue;

                        tmp2[r, c] = range;
                        tmp2.AlignX[r, c] = tmp1.AlignX[Rows + _r, c];
                        tmp2.RowHeight[r] = tmp1.RowHeight[Rows + _r];

                        tmp1[r, c] = null;
                    }
                    r++;
                    Rows++;
                }
                if (tmp2.Rows <= _r)
                    _r = tmp2.Rows - 1;
                tmp2.RowHeight[_r] = printManager.LineSpacing2;

                _page.Add(tmp2);
                if (tmp1.Rows - Rows < rows)
                {
                    rows = tmp1.Rows - Rows - _r;
                }
            }
            return _page;
        }

    }
}
