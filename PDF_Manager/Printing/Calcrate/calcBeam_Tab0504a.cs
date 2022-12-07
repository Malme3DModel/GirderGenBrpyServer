using PdfSharpCore.Drawing;
using Printing.Comon;

namespace Printing.Calcrate
{
    internal partial class calcBeam
    {
        private static void DrawTab0504a(PdfDocument mc)
        {
            var bkup = mc.font_mic;
            mc.font_mic = new XFont("MS Mincho", 9, XFontStyle.Regular);

            var table = new Table(11, 5);

            for (var i = 0; i < table.Rows + 1; ++i)
                for (var j = 0; j < table.Columns; ++j)
                    table.HolLW[i, j] = 0.1;
            table.HolLW[2, 0] = table.HolLW[3, 0] = 0;
            table.HolLW[5, 0] = table.HolLW[6, 0] = 0;
            table.HolLW[8, 0] = table.HolLW[9, 0] = table.HolLW[10, 0] = 0;

            for (var i = 0; i < table.Rows; ++i)
                for (var j = 0; j < table.Columns + 1; ++j)
                    table.VtcLW[i, j] = 0.1;
            table.VtcLW[0, 1] = 0;

            table.ColMerge[0, 0] = 1;
            table.RowMerge[1, 0] = 2;
            table.RowMerge[4, 0] = 2;
            table.RowMerge[7, 0] = 3;

            table.AlignY[1, 0] = "C";
            table.AlignY[4, 0] = "C";
            table.AlignY[7, 0] = "C";

            for (var i = 0; i < table.Rows; ++i)
                for (var j = 0; j < table.Columns; ++j)
                {
                    table.LeftCellPadding[i, j] = 4;
                    table.TopCellPadding[i, j] = 2;
                    table.RightCellPadding[i, j] = 4;
                    table.BottomCellPadding[i, j] = 2;
                }

            table[0, 0] = "断 面";
            table[0, 1] = "";
            table[0, 2] = "Sec-2";
            table[0, 3] = "Sec-1";
            table[0, 4] = "Sec-2";
            table[1, 0] = "U-Flg.PL";
            table[1, 1] = "b × t (mm)";
            table[1, 2] = " 310   ×     24";
            table[1, 3] = " 310   ×     29";
            table[1, 4] = " 310   ×     24";
            table[2, 0] = "";
            table[2, 1] = "σ (N/mm\u00B2)";
            table[2, 2] = "-267   ≦    271";
            table[2, 3] = "-267   ≦    271";
            table[2, 4] = "-267   ≦    271";
            table[3, 0] = "";
            table[3, 1] = "決定ケース";
            table[3, 2] = "組合せ①【鋼+鉄筋】";
            table[3, 3] = "組合せ①【鋼+鉄筋】";
            table[3, 4] = "組合せ①【鋼+鉄筋】";
            table[4, 0] = "Web.PL";
            table[4, 1] = "b × t (mm)";
            table[4, 2] = "1,676  ×     9 ";
            table[4, 3] = "1,671  ×     9 ";
            table[4, 4] = "1,676  ×     9 ";
            table[5, 0] = "";
            table[5, 1] = "τ (N/mm\u00B2)";
            table[5, 2] = "  63   ≦    156";
            table[5, 3] = "  14   ≦    156";
            table[5, 4] = "  63   ≦    156";
            table[6, 0] = "";
            table[6, 1] = "［道示Ⅱ］式5.3.2";
            table[6, 2] = " 0.93  ≦    1.2";
            table[6, 3] = " 0.92  ≦    1.2";
            table[6, 4] = " 0.93  ≦    1.2";
            table[7, 0] = "L-Flg.PL";
            table[7, 1] = "b × t (mm)";
            table[7, 2] = " 550   ×     26";
            table[7, 3] = " 550   ×     26";
            table[7, 4] = " 550   ×     26";
            table[8, 0] = "";
            table[8, 1] = "σ (N/mm\u00B2)";
            table[8, 2] = " 226   ≦    271";
            table[8, 3] = " 267   ≦    271";
            table[8, 4] = " 226   ≦    271";
            table[9, 0] = "";
            table[9, 1] = "決定ケース";
            table[9, 2] = "組合せ②【合成】";
            table[9, 3] = "組合せ②【合成】";
            table[9, 4] = "組合せ②【合成】";
            table[10, 0] = "";
            table[10, 1] = "孔引き後σn(N/mm\u00B2)";
            table[10, 2] = " 267   ≦    271";
            table[10, 3] = "－";
            table[10, 4] = " 267   ≦    271";
            table.PrintTable(mc);

            mc.font_mic = bkup;
        }
    }
}
