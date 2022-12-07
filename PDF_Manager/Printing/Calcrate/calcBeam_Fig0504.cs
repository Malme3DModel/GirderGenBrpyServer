using PdfSharpCore.Drawing;
using Printing.Comon;

namespace Printing.Calcrate
{
    internal partial class calcBeam
    {
        private static void DrawFig0504(PdfDocument mc)
        {
            // 図の拡大率(図中のテキストのフォントサイズには影響なし)
            var scale = 1.0;

            // 元々の(上側の)図のサイズと場所(単位はセンチメートル)
            // (Pg.24の図で採寸した値であることに注意)
            var oldRect1 = new XRect(new XPoint(5.21, 17.52), new XPoint(17.65, 24.43));
            // 元々の(下側の)図のサイズと場所(単位はセンチメートル)
            var oldRect2 = new XRect(new XPoint(4.04, 12.58), new XPoint(16.95, 23.60));
            // 上側と下側の図の水平方向の位置合わせ
            oldRect1.X = oldRect2.X;

            // 上側の図と下側の図の上下方向の位置の差
            var distance = XUnit.FromCentimeter(8.05);

            var matrix1 = new XMatrix();
            // センチメートルをポイントに換算
            matrix1.ScaleAppend(XUnit.FromCentimeter(1));
            // 上側の図の基準点を用紙の左上角から図の左上角に平行移動
            matrix1.TranslateAppend(-XUnit.FromCentimeter(oldRect1.Left), -XUnit.FromCentimeter(oldRect1.Top));
            // 上側の図の左上角を基準として図を拡大/縮小
            matrix1.ScaleAppend(scale);
            // 上側の図の左上角をmc.currentPosに平行移動
            matrix1.TranslateAppend(mc.currentPos.X, mc.currentPos.Y);
            // 水平方向の中央寄せ
            matrix1.TranslateAppend((mc.currentPage.Width - mc.Margine.Right - mc.currentPos.X - XUnit.FromCentimeter(oldRect1.Width)) / 2, 0);

            var matrix2 = new XMatrix();
            // センチメートルをポイントに換算
            matrix2.ScaleAppend(XUnit.FromCentimeter(1));
            // 下側の図の基準点を用紙の左上角から図の左上角に平行移動
            matrix2.TranslateAppend(-XUnit.FromCentimeter(oldRect2.Left), -XUnit.FromCentimeter(oldRect2.Top));
            // 下側の図の左上角を基準として図を拡大/縮小
            matrix2.ScaleAppend(scale);
            // 下側の図を下方に移動
            matrix2.TranslateAppend(0, distance * scale);
            // 下側の図の左上角をmc.currentPosに平行移動
            matrix2.TranslateAppend(mc.currentPos.X, mc.currentPos.Y);
            // 水平方向の中央寄せ
            matrix2.TranslateAppend((mc.currentPage.Width - mc.Margine.Right - mc.currentPos.X - XUnit.FromCentimeter(oldRect1.Width)) / 2, 0);

            // 用紙左端から図の中央(橋の横断面？の中央)までの距離
            var centerX = matrix1.Transform(new XPoint(11.45, 0)).X;

            var bkup = mc.xpen.Width;
            //mc.xpen.Width = 0.1;
            try
            {
                // 図本体の描画
                DrawFig0504a(mc, matrix1, matrix2, centerX);

                // 寸法線の描画
                DrawFig0504b(mc, matrix1, matrix2, centerX);

                // テキストの描画
                DrawFig0504c(mc, matrix1, matrix2);
            }
            finally
            {
                mc.xpen.Width = bkup;
            }

            // 描画後の(上側と下側を合わせた)図のサイズと場所(単位はポイント)
            var newRect = new XRect(matrix1.Transform(oldRect1.TopLeft), matrix2.Transform(oldRect2.BottomRight));

            // mc.currentPosを(上側と下側を合わせた)図の高さ分だけ下方に移動
            mc.addCurrentY(newRect.Height);
        }

        private static void DrawFig0504a(PdfDocument mc, XMatrix matrix1, XMatrix matrix2, double centerX)
        {
            // 図2と共通
            DrawFig0502a(mc, matrix1, centerX);

            // 両側の縦線
            var p01 = matrix2.Transform(new XPoint(6.19, 12.87));
            var p02 = matrix2.Transform(new XPoint(6.19, 23.60));
            Shape.DrawLines(mc, new[] { p01, p02, });
            Shape.DrawLines(mc, new[] { p01, p02, }.YAxisSymmetry(centerX));

            // 縦線間の横線
            foreach (var y in new[]
            {
                13.38,
                16.65,
                22.49,
            })
            {
                var pa = matrix2.Transform(new XPoint(6.19, y));
                var pb = matrix2.Transform(new XPoint(11.45, y));
                Shape.DrawLines(mc, new[] { pa, pb, });
                Shape.DrawLines(mc, new[] { pa, pb, }.YAxisSymmetry(centerX));
            }

            // 左縦線の左側にはみ出している横線
            foreach (var y in new[]
            {
                13.79,
                14.78,
                15.75,
                16.75,
                18.73,
                19.64,
                20.63,
                21.63,
                22.61,
                23.60,
            })
            {
                var pa = matrix2.Transform(new XPoint(4.33, y));
                var pb = matrix2.Transform(new XPoint(6.19, y));
                var pc = matrix2.Transform(new XPoint(11.45, y));
                Shape.DrawLines(mc, new[] { pa, pb, });
                Shape.DrawLines(mc, new[] { pb, pc, });
                Shape.DrawLines(mc, new[] { pb, pc, }.YAxisSymmetry(centerX));
            }

            // 塗装欄の構造物
            var p03 = matrix2.Transform(new XPoint(6.86, 18.73));
            var p04 = matrix2.Transform(new XPoint(6.86, 18.51));
            var p05 = matrix2.Transform(new XPoint(11.45, 18.51));
            Shape.DrawLines(mc, new[] { p03, p04, });
            Shape.DrawLines(mc, new[] { p03, p04, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p04, p05, });
            Shape.DrawLines(mc, new[] { p04, p05, }.YAxisSymmetry(centerX));

            // 活荷重欄の構造物
            var p06 = matrix2.Transform(new XPoint(6.86, 23.60));
            var p07 = matrix2.Transform(new XPoint(6.86, 23.19));
            var p08 = matrix2.Transform(new XPoint(11.45, 23.19));
            Shape.DrawLines(mc, new[] { p06, p07, });
            Shape.DrawLines(mc, new[] { p06, p07, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p07, p08, });
            Shape.DrawLines(mc, new[] { p07, p08, }.YAxisSymmetry(centerX));

            // ハンチ欄、鋼重欄、地覆欄、防護柵欄の下矢印
            foreach (var p in new[]
            {
                new
                {
                    S = matrix2.Transform(new XPoint(7.33, 14.35)),
                    T = matrix2.Transform(new XPoint(7.33, 14.78)),
                },
                new
                {
                    S = matrix2.Transform(new XPoint(10.09, 14.35)),
                    T = matrix2.Transform(new XPoint(10.09, 14.78)),
                },
                new
                {
                    S = matrix2.Transform(new XPoint(7.33, 15.32)),
                    T = matrix2.Transform(new XPoint(7.33, 15.75)),
                },
                new
                {
                    S = matrix2.Transform(new XPoint(10.09, 15.32)),
                    T = matrix2.Transform(new XPoint(10.09, 15.75)),
                },
                new
                {
                    S = matrix2.Transform(new XPoint(6.53, 19.21)),
                    T = matrix2.Transform(new XPoint(6.53, 19.64)),
                },
                new
                {
                    S = matrix2.Transform(new XPoint(6.46, 20.20)),
                    T = matrix2.Transform(new XPoint(6.46, 20.63)),
                },
            })
            {
                Shape.DrawLines(mc, new[] { p.S, p.T, }, endCap: LineCap.ArrowAnchor);
                Shape.DrawLines(mc, new[] { p.S, p.T, }.YAxisSymmetry(centerX), endCap: LineCap.ArrowAnchor);
            }

            // 添架物の下矢印
            var p09 = matrix2.Transform(new XPoint(11.45, 21.17));
            var p10 = matrix2.Transform(new XPoint(11.45, 21.63));
            Shape.DrawLines(mc, new[] { p09, p10, }, endCap: LineCap.ArrowAnchor);
        }

        private static void DrawFig0504b(PdfDocument mc, XMatrix matrix1, XMatrix matrix2, double centerX)
        {
            // 図2と共通
            DrawFig0502b(mc, matrix1, centerX);

            // 地覆欄と防護柵欄の寸法線
            foreach (var p in new[]
            {
                new[]
                {
                    matrix2.Transform(new XPoint(5.96, 19.04)),
                    matrix2.Transform(new XPoint(6.19, 19.04)),
                    matrix2.Transform(new XPoint(6.53, 19.04)),
                    matrix2.Transform(new XPoint(6.78, 19.04)),
                    matrix2.Transform(new XPoint(6.53, 19.16)),
                },
                new[]
                {
                    matrix2.Transform(new XPoint(5.96, 20.03)),
                    matrix2.Transform(new XPoint(6.19, 20.03)),
                    matrix2.Transform(new XPoint(6.46, 20.03)),
                    matrix2.Transform(new XPoint(6.71, 20.03)),
                    matrix2.Transform(new XPoint(6.46, 20.15)),
                },
            })
            {
                Shape.DrawLines(mc, new[] { p[0], p[1], }, endCap: LineCap.ArrowAnchor);
                Shape.DrawLines(mc, new[] { p[1], p[2], });
                Shape.DrawLines(mc, new[] { p[2], p[3], }, startCap: LineCap.ArrowAnchor);
                Shape.DrawLines(mc, new[] { p[2], p[4], });

                Shape.DrawLines(mc, new[] { p[0], p[1], }.YAxisSymmetry(centerX), endCap: LineCap.ArrowAnchor);
                Shape.DrawLines(mc, new[] { p[1], p[2], }.YAxisSymmetry(centerX));
                Shape.DrawLines(mc, new[] { p[2], p[3], }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);
                Shape.DrawLines(mc, new[] { p[2], p[4], }.YAxisSymmetry(centerX));
            }
        }

        private static void DrawFig0504c(PdfDocument mc, XMatrix matrix1, XMatrix matrix2)
        {
            var bkup = mc.currentPos;
            try
            {
                // 図2と共通
                DrawFig0502c(mc, matrix1);

                var font = new XFont("MS Mincho", 8, XFontStyle.Regular);

                foreach (var d in new[]
                {
                    new { str = "合成前死荷重", p = new XPoint(4.04, 12.86), deg = 0.0, },
                    new { str = "床   版", p = new XPoint(4.94, 13.40), deg = 0.0, },
                    new { str = "5.39kN/m\u00B2", p = new XPoint(10.88, 13.37), deg = 0.0, },
                    new { str = "ハ ン チ", p = new XPoint(4.94, 14.35), deg = 0.0, },
                    new { str = "1.23kN/m", p = new XPoint(6.73, 14.25), deg = 0.0, },
                    new { str = "1.75kN/m", p = new XPoint(9.50, 14.25), deg = 0.0, },
                    new { str = "1.75kN/m", p = new XPoint(12.24, 14.25), deg = 0.0, },
                    new { str = "1.23kN/m", p = new XPoint(14.99, 14.25), deg = 0.0, },
                    new { str = "鋼   重", p = new XPoint(4.94, 15.36), deg = 0.0, },
                    new { str = "4.41kN/m", p = new XPoint(6.73, 15.22), deg = 0.0, },
                    new { str = "4.41kN/m", p = new XPoint(9.50, 15.22), deg = 0.0, },
                    new { str = "4.41kN/m", p = new XPoint(12.24, 15.22), deg = 0.0, },
                    new { str = "4.41kN/m", p = new XPoint(14.99, 15.22), deg = 0.0, },
                    new { str = "型   枠", p = new XPoint(4.94, 16.35), deg = 0.0, },
                    new { str = "1.00kN/m\u00B2", p = new XPoint(10.88, 16.52), deg = 0.0, },
                    new { str = "合成後死荷重", p = new XPoint(4.04, 17.80), deg = 0.0, },
                    new { str = "舗   装", p = new XPoint(4.94, 18.34), deg = 0.0, },
                    new { str = "1.69kN/m\u00B2", p = new XPoint(10.88, 18.40), deg = 0.0, },
                    new { str = "地   覆", p = new XPoint(4.94, 19.25), deg = 0.0, },
                    new { str = "300", p = new XPoint(6.12, 19.00), deg = 0.0, },
                    new { str = "4.78kN/m", p = new XPoint(6.64, 19.47), deg = 0.0, },
                    new { str = "4.78kN/m", p = new XPoint(15.09, 19.47), deg = 0.0, },
                    new { str = "300", p = new XPoint(16.35, 19.00), deg = 0.0, },
                    new { str = "防護柵", p = new XPoint(4.94, 20.24), deg = 0.0, },
                    new { str = "250", p = new XPoint(6.12, 20.00), deg = 0.0, },
                    new { str = "0.50kN/m", p = new XPoint(6.61, 20.45), deg = 0.0, },
                    new { str = "0.50kN/m", p = new XPoint(15.16, 20.45), deg = 0.0, },
                    new { str = "250", p = new XPoint(16.35, 20.00), deg = 0.0, },
                    new { str = "添架物", p = new XPoint(4.94, 21.23), deg = 0.0, },
                    new { str = "0.60kN/m", p = new XPoint(10.88, 21.06), deg = 0.0, },
                    new { str = "型枠(撤去)", p = new XPoint(4.75, 22.23), deg = 0.0, },
                    new { str = "―1.00kN/m\u00B2", p = new XPoint(10.88, 22.38), deg = 0.0, },
                    new { str = "活荷重", p = new XPoint(4.94, 23.20), deg = 0.0, },
                    new { str = "B活荷重", p = new XPoint(10.88, 23.09), deg = 0.0, },
                })
                {
                    var p = matrix2.Transform(d.p);

                    mc.printText(p.X, p.Y, d.str, d.deg, font);
                }
            }
            finally
            {
                mc.currentPos = bkup;
            }
        }
    }
}
