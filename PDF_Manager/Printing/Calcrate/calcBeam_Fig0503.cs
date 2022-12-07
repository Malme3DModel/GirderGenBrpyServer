using PdfSharpCore.Drawing;
using Printing.Comon;

namespace Printing.Calcrate
{
    internal partial class calcBeam
    {
        private static void DrawFig0503(PdfDocument mc)
        {
            // 図の拡大率(図中のテキストのフォントサイズには影響なし)
            var scale = 1.0;

            // 元々の図のサイズと場所(単位はセンチメートル)
            var oldRect = new XRect(new XPoint(5.45, 8.11), new XPoint(14.86, 9.95));

            var matrix = new XMatrix();
            // センチメートルをポイントに換算
            matrix.ScaleAppend(XUnit.FromCentimeter(1));
            // 図の基準点を用紙の左上角から図の左上角に平行移動
            matrix.TranslateAppend(-XUnit.FromCentimeter(oldRect.Left), -XUnit.FromCentimeter(oldRect.Top));
            // 図の左上角を基準として図を拡大/縮小
            matrix.ScaleAppend(scale);
            // 図の左上角をmc.currentPosに平行移動
            matrix.TranslateAppend(mc.currentPos.X, mc.currentPos.Y);
            // 水平方向の中央寄せ
            matrix.TranslateAppend((mc.currentPage.Width - mc.Margine.Right - mc.currentPos.X - XUnit.FromCentimeter(oldRect.Width)) / 2, 0);

            var bkup = mc.xpen.Width;
            //mc.xpen.Width = 0.1;
            try
            {
                // 図本体の描画
                DrawFig0503a(mc, matrix);

                // 寸法線の描画
                DrawFig0503b(mc, matrix);

                // テキストの描画
                DrawFig0503c(mc, matrix);
            }
            finally
            {
                mc.xpen.Width = bkup;
            }

            // 描画後の図のサイズと場所(単位はポイント)
            var newRect = new XRect(matrix.Transform(oldRect.TopLeft), matrix.Transform(oldRect.BottomRight));

            // mc.currentPosを図の高さ分だけ下方に移動
            mc.addCurrentY(newRect.Height);
        }

        private static void DrawFig0503a(PdfDocument mc, XMatrix matrix)
        {
            #region (G1,G4)

            var p01 = matrix.Transform(new XPoint(5.45, 8.58));
            var p02 = matrix.Transform(new XPoint(9.19, 8.58));
            var p03 = matrix.Transform(new XPoint(8.68, 8.73));
            var p04 = matrix.Transform(new XPoint(7.83, 8.73));
            Shape.DrawPolygon(mc, new[] { p01, p02, p03, p04, });

            #endregion

            #region (G2,G3)

            var p05 = matrix.Transform(new XPoint(11.14, 8.60));
            var p06 = matrix.Transform(new XPoint(13.84, 8.60));
            var p07 = matrix.Transform(new XPoint(12.91, 8.87));
            var p08 = matrix.Transform(new XPoint(12.07, 8.87));
            Shape.DrawPolygon(mc, new[] { p05, p06, p07, p08, });

            #endregion
        }

        private static void DrawFig0503b(PdfDocument mc, XMatrix matrix)
        {
            #region (G1,G4)

            var p01 = matrix.Transform(new XPoint(9.26, 8.58));
            var p02 = matrix.Transform(new XPoint(10.19, 8.58));
            Shape.DrawLines(mc, new[] { p01, p02, });

            var p03 = matrix.Transform(new XPoint(8.77, 8.73));
            var p04 = matrix.Transform(new XPoint(10.19, 8.73));
            Shape.DrawLines(mc, new[] { p03, p04, });

            var p05 = matrix.Transform(new XPoint(10.1, 8.38));
            var p06 = matrix.Transform(new XPoint(10.1, 8.58));
            Shape.DrawLines(mc, new[] { p05, p06, }, endCap: LineCap.ArrowAnchor);
            var p07 = p06;
            var p08 = matrix.Transform(new XPoint(10.1, 8.73));
            Shape.DrawLines(mc, new[] { p07, p08, });
            var p09 = p08;
            var p10 = matrix.Transform(new XPoint(10.1, 8.95));
            Shape.DrawLines(mc, new[] { p09, p10, }, startCap: LineCap.ArrowAnchor);

            var p11 = matrix.Transform(new XPoint(5.45, 8.64));
            var p12 = matrix.Transform(new XPoint(5.45, 9.95));
            Shape.DrawLines(mc, new[] { p11, p12, });

            var p13 = matrix.Transform(new XPoint(7.83, 8.82));
            var p14 = matrix.Transform(new XPoint(7.83, 9.55));
            Shape.DrawLines(mc, new[] { p13, p14, });

            var p15 = matrix.Transform(new XPoint(8.68, 8.82));
            var p16 = matrix.Transform(new XPoint(8.68, 9.55));
            Shape.DrawLines(mc, new[] { p15, p16, });

            var p17 = matrix.Transform(new XPoint(9.19, 8.64));
            var p18 = matrix.Transform(new XPoint(9.19, 9.95));
            Shape.DrawLines(mc, new[] { p17, p18, });

            var p19 = matrix.Transform(new XPoint(5.45, 9.47));
            var p20 = matrix.Transform(new XPoint(7.83, 9.47));
            Shape.DrawLines(mc, new[] { p19, p20, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            var p21 = p20;
            var p22 = matrix.Transform(new XPoint(8.68, 9.47));
            Shape.DrawLines(mc, new[] { p21, p22, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            var p23 = p22;
            var p24 = matrix.Transform(new XPoint(9.19, 9.47));
            Shape.DrawLines(mc, new[] { p23, p24, });

            var p25 = p24;
            var p26 = matrix.Transform(new XPoint(9.42, 9.47));
            Shape.DrawLines(mc, new[] { p25, p26, }, startCap: LineCap.ArrowAnchor);

            var p27 = matrix.Transform(new XPoint(5.45, 9.86));
            var p28 = matrix.Transform(new XPoint(9.19, 9.86));
            Shape.DrawLines(mc, new[] { p27, p28, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            #endregion

            #region (G2,G3)

            var p29 = matrix.Transform(new XPoint(13.93, 8.60));
            var p30 = matrix.Transform(new XPoint(14.86, 8.60));
            Shape.DrawLines(mc, new[] { p29, p30, });

            var p31 = matrix.Transform(new XPoint(12.99, 8.87));
            var p32 = matrix.Transform(new XPoint(14.86, 8.87));
            Shape.DrawLines(mc, new[] { p31, p32, });

            var p33 = matrix.Transform(new XPoint(14.78, 8.38));
            var p34 = matrix.Transform(new XPoint(14.78, 8.60));
            Shape.DrawLines(mc, new[] { p33, p34, }, endCap: LineCap.ArrowAnchor);
            var p35 = p34;
            var p36 = matrix.Transform(new XPoint(14.78, 8.87));
            Shape.DrawLines(mc, new[] { p35, p36, });
            var p37 = p36;
            var p38 = matrix.Transform(new XPoint(14.78, 9.11));
            Shape.DrawLines(mc, new[] { p37, p38, }, startCap: LineCap.ArrowAnchor);

            var p39 = matrix.Transform(new XPoint(11.14, 8.68));
            var p40 = matrix.Transform(new XPoint(11.14, 9.95));
            Shape.DrawLines(mc, new[] { p39, p40, });

            var p41 = matrix.Transform(new XPoint(12.07, 8.94));
            var p42 = matrix.Transform(new XPoint(12.07, 9.57));
            Shape.DrawLines(mc, new[] { p41, p42, });

            var p43 = matrix.Transform(new XPoint(12.91, 8.94));
            var p44 = matrix.Transform(new XPoint(12.91, 9.57));
            Shape.DrawLines(mc, new[] { p43, p44, });

            var p45 = matrix.Transform(new XPoint(13.84, 8.68));
            var p46 = matrix.Transform(new XPoint(13.84, 9.95));
            Shape.DrawLines(mc, new[] { p45, p46, });

            var p47 = matrix.Transform(new XPoint(11.14, 9.48));
            var p48 = matrix.Transform(new XPoint(12.07, 9.48));
            Shape.DrawLines(mc, new[] { p47, p48, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            var p49 = p48;
            var p50 = matrix.Transform(new XPoint(12.91, 9.48));
            Shape.DrawLines(mc, new[] { p49, p50, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            var p51 = p50;
            var p52 = matrix.Transform(new XPoint(13.84, 9.48));
            Shape.DrawLines(mc, new[] { p51, p52, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            var p53 = matrix.Transform(new XPoint(11.14, 9.88));
            var p54 = matrix.Transform(new XPoint(13.84, 9.88));
            Shape.DrawLines(mc, new[] { p53, p54, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            #endregion
        }

        private static void DrawFig0503c(PdfDocument mc, XMatrix matrix)
        {
            var bkup = mc.currentPos;
            try
            {
                var font = new XFont("MS Mincho", 8, XFontStyle.Regular);

                #region (G1,G4)

                foreach (var d in new[]
                {
                    new { txt = "(G1,G4)", pos = new XPoint(6.9, 8.5), angle = 0, },
                    new { txt = "870", pos = new XPoint(6.4, 9.45), angle = 0, },
                    new { txt = "310", pos = new XPoint(8.0, 9.45), angle = 0, },
                    new { txt = "180", pos = new XPoint(8.7, 9.45), angle = 0, },
                    new { txt = "1360", pos = new XPoint(7.0, 9.88), angle = 0, },
                    new { txt = "60", pos = new XPoint(10.0, 8.95), angle = -90, },
                })
                {
                    var p = matrix.Transform(d.pos);

                    mc.printText(p.X, p.Y, d.txt, d.angle, font);
                }

                #endregion

                #region (G2,G3)

                foreach (var d in new[]
                {
                    new { txt = "(G2,G3)", pos = new XPoint(12.17, 8.5), angle = 0, },
                    new { txt = "333", pos = new XPoint(11.37, 9.45), angle = 0, },
                    new { txt = "310", pos = new XPoint(12.26, 9.45), angle = 0, },
                    new { txt = "333", pos = new XPoint(13.13, 9.45), angle = 0, },
                    new { txt = "976", pos = new XPoint(12.26, 9.88), angle = 0, },
                    new { txt = "111", pos = new XPoint(14.72, 9.00), angle = -90, },
                })
                {
                    var p = matrix.Transform(d.pos);

                    mc.printText(p.X, p.Y, d.txt, d.angle, font);
                }

                #endregion
            }
            finally
            {
                mc.currentPos = bkup;
            }
        }
    }
}
