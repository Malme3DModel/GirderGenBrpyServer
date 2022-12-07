using PdfSharpCore.Drawing;
using Printing.Comon;

namespace Printing.Calcrate
{
    internal partial class calcBeam
    {
        private static void DrawTab0506(PdfDocument mc)
        {
            var bkup = mc.font_mic;
            mc.font_mic = new XFont("MS Mincho", 9, XFontStyle.Regular);

            var table = new Table(22, 6);

            table.ColMerge[0, 0] = 3;
            table.ColMerge[0, 4] = 1;
            table.ColMerge[2, 0] = 3;
            table.ColMerge[3, 1] = 2;
            table.ColMerge[5, 1] = 2;
            table.ColMerge[7, 0] = 3;
            table.ColMerge[16, 2] = 1;
            table.ColMerge[18, 2] = 1;
            table.ColMerge[20, 1] = 2;
            table.ColMerge[20, 4] = 1;
            table.ColMerge[21, 1] = 4;

            var hrules = new string[]
            {
                "━━━━━━",
                "　　　　──",
                "━━━━━━",
                "　─────",
                "　　　　　　",
                "　─────",
                "　　　　　　",
                "━━━━━━",
                "　─────",
                "　　　　　　",
                "　　　───",
                "　　　　　　",
                "　　────",
                "　　　　　　",
                "　　　───",
                "　　　　　　",
                "　─────",
                "　　　　　　",
                "　　────",
                "　　　　　　",
                "　─────",
                "━━━━━━",
            };
            for (var i = 0; i < hrules.Length; i++)
                for (var j = 0; j < hrules[i].Length; j++)
                    switch (hrules[i][j])
                    {
                        case '━':
                            table.HolLW[i, j] = 1.0;
                            break;
                        case '─':
                            table.HolLW[i, j] = 0.1;
                            break;
                        default:
                            table.HolLW[i, j] = 0;
                            break;
                    }

            var vrules = new string[]
            {
                "┃　　　┃　┃",
                "┃　　　┃│┃",
                "┃　　　┃│┃",
                "┃│　　┃│┃",
                "┃│　　┃│┃",
                "┃│　　┃│┃",
                "┃│　　┃│┃",
                "┃　　　┃│┃",
                "┃│││┃│┃",
                "┃│││┃│┃",
                "┃│││┃│┃",
                "┃│││┃│┃",
                "┃│││┃│┃",
                "┃│││┃│┃",
                "┃│││┃│┃",
                "┃│││┃│┃",
                "┃││　┃│┃",
                "┃││　┃│┃",
                "┃││　┃│┃",
                "┃││　┃│┃",
                "┃│　　┃　┃",
            };
            for (var i = 0; i < vrules.Length; i++)
                for (var j = 0; j < vrules[i].Length; j++)
                    switch (vrules[i][j])
                    {
                        case '┃':
                            table.VtcLW[i, j] = 1.0;
                            break;
                        case '│':
                            table.VtcLW[i, j] = 0.1;
                            break;
                        default:
                            table.VtcLW[i, j] = 0;
                            break;
                    }

            for (var i = 0; i < table.Rows; i++)
                for (var j = 0; j < table.Columns; j++)
                    table.AlignX[i, j] = "L";
            table.AlignX[0, 0] = table.AlignX[0, 4] = "C";
            table.AlignX[1, 4] = table.AlignX[1, 5] = "C";
            table.AlignX[12, 5] = "C";
            table.AlignX[20, 4] = "C";

            for (var i = 0; i < table.Rows; i++)
                for (var j = 0; j < table.Columns; j++)
                {
                    table.LeftCellPadding[i, j] = 4;
                    table.TopCellPadding[i, j] = 2;
                    table.RightCellPadding[i, j] = 4;
                    table.BottomCellPadding[i, j] = 2;
                }

            table[0, 0] = "構造，部材";
            table[0, 4] = "照査する限界状態";
            table[1, 0] = "   "; // 1列目の幅を決定するためのダミー
            table[1, 4] = "限界状態1";
            table[1, 5] = "限界状態3";
            table[2, 0] = "コンクリート系床版を有する鋼桁";
            table[2, 4] = "14.6";
            table[2, 5] = "14.7";
            table[3, 1] = "床版";
            table[3, 4] = "14.6.2";
            table[3, 5] = "14.7.2";
            table[4, 4] = "11章の各規程";
            table[4, 5] = "  →14.6.2";
            table[5, 1] = "鋼桁";
            table[5, 4] = "14.6.3";
            table[5, 5] = "14.7.3";
            table[6, 4] = "13章の各規程";
            table[6, 5] = "  →14.6.3";
            table[7, 0] = "鋼桁";
            table[7, 4] = "13.5";
            table[7, 5] = "13.6";
            table[8, 1] = "フランジ";
            table[8, 2] = "曲げモーメントに";
            table[8, 3] = "圧縮を受ける";
            table[8, 4] = "5.3.2";
            table[8, 5] = "5.4.2";
            table[9, 2] = "よる圧縮を受ける";
            table[9, 3] = "自由突出板";
            table[9, 4] = "  →5.4.2";
            table[10, 2] = "部材";
            table[10, 3] = "曲げモーメントによる";
            table[10, 4] = "5.3.6";
            table[10, 5] = "5.4.6";
            table[11, 3] = "圧縮を受ける部材";
            table[11, 4] = "  →5.4.6";
            table[12, 2] = "曲げモーメントに";
            table[12, 3] = "軸方向引張力を受ける";
            table[12, 4] = "5.3.5";
            table[12, 5] = "---";
            table[13, 2] = "よる引張を受ける";
            table[13, 3] = "部材(5.3.6より)";
            table[14, 2] = "部材";
            table[14, 3] = "曲げモーメントによる";
            table[14, 4] = "5.3.6";
            table[14, 5] = "5.4.6";
            table[15, 3] = "引張を受ける部材";
            table[15, 4] = "  →5.3.5，5.4.6";
            table[16, 1] = "腹板";
            table[16, 2] = "せん断を受ける部材";
            table[16, 4] = "5.3.7";
            table[16, 5] = "5.4.7";
            table[17, 4] = "  →5.4.7";
            table[18, 2] = "曲げモーメントとせん断力を受ける部材";
            table[18, 4] = "5.3.9";
            table[18, 5] = "5.4.9";
            table[19, 5] = "  →5.3.9";
            table[20, 1] = "相反部材";
            table[20, 4] = "5.1.3";
            table[21, 1] = "→番号: 条文内で参照されている節番号を示す(みなし規定)。";

            // 5列目と6列目の幅調整
            table.ColWidth[4] = table.ColWidth[5] = table.RightCellPadding[15, 4] + mc.MeasureString(table[15, 4]).Width + table.LeftCellPadding[15, 4];

            table.PrintTable(mc);

            mc.font_mic = bkup;
        }
    }
}
