using PdfSharpCore.Drawing;
using Printing.Comon;

namespace Printing.Calcrate
{
    internal partial class calcBeam
    {
        private static void DrawFig0507(PdfDocument mc)
        {
            // 図の拡大率(図中のテキストのフォントサイズには影響なし)
            var scale = 1.0;

            // 元々の図のサイズと場所(単位はセンチメートル)
            var oldRect = new XRect(new XPoint(5.43, 10.36), new XPoint(16.00, 14.44));

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
                DrawFig0507a(mc, matrix);

                // 寸法線の描画
                DrawFig0507b(mc, matrix);

                // テキストの描画
                DrawFig0507c(mc, matrix);
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

        private static void DrawFig0507a(PdfDocument mc, XMatrix matrix)
        {
            // 左側の上部
            var p01 = matrix.Transform(new XPoint(11.18, 11.01));
            var p02 = matrix.Transform(new XPoint(5.43, 11.01));
            var p03 = matrix.Transform(new XPoint(5.43, 11.24));
            var p04 = matrix.Transform(new XPoint(11.18, 11.24));
            Shape.DrawLines(mc, new[] { p01, p02, p03, p04, });
            var p05 = matrix.Transform(new XPoint(11.18, 10.83));
            var p06 = matrix.Transform(new XPoint(11.18, 11.66));
            Shape.DrawLines(mc, new[] { p05, p06, });
            // 左の脚柱
            DrawFig0507a_G1G4(mc, matrix);
            // 右の脚柱
            DrawFig0507a_G2G3(mc, matrix);

            // 右側
            var p07 = matrix.Transform(new XPoint(12.63, 11.58));
            var p08 = matrix.Transform(new XPoint(16.00, 11.58));
            Shape.DrawLines(mc, new[] { p07, p08, });
            var p09 = matrix.Transform(new XPoint(12.63, 12.05));
            var p10 = matrix.Transform(new XPoint(13.33, 12.05));
            var p11 = matrix.Transform(new XPoint(15.61, 12.05));
            var p12 = matrix.Transform(new XPoint(16.00, 12.05));
            Shape.DrawLines(mc, new[] { p09, p10, });
            Shape.DrawLines(mc, new[] { p10, p11, }, dashPattern: new[] { 5 / mc.xpen.Width, 3 / mc.xpen.Width, });
            Shape.DrawLines(mc, new[] { p11, p12, });
            var p13 = matrix.Transform(new XPoint(14.12, 12.31));
            var p14 = matrix.Transform(new XPoint(14.82, 12.31));
            var p15 = matrix.Transform(new XPoint(14.82, 12.36));
            var p16 = matrix.Transform(new XPoint(14.12, 12.36));
            Shape.DrawPolygon(mc, new[] { p13, p14, p15, p16, });
            var p17 = p10;
            var p18 = p13;
            var p19 = matrix.Transform(new XPoint(13.86, 12.05));
            Shape.DrawLines(mc, new[] { p17, p18, p19, });
            var p20 = p11;
            var p21 = p14;
            var p22 = matrix.Transform(new XPoint(15.08, 12.05));
            Shape.DrawLines(mc, new[] { p20, p21, p22, });
            var p23 = matrix.Transform(new XPoint(14.44, 12.36));
            var p24 = matrix.Transform(new XPoint(14.44, 12.73));
            Shape.DrawLines(mc, new[] { p23, p24, });
            var p25 = matrix.Transform(new XPoint(14.50, 12.36));
            var p26 = matrix.Transform(new XPoint(14.50, 12.73));
            Shape.DrawLines(mc, new[] { p25, p26, });

            // 45°
            var p27 = p19;
            var radius = XUnit.FromCentimeter(14.22 - 13.86);
            var p28 = new XPoint(p27.X - radius, p27.Y - radius);
            var p29 = new XPoint(p27.X + radius, p27.Y + radius);
            Shape.DrawArc(mc, p28, p29, 0, 45, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // 施工時勾配の吹き出し
            var p30 = matrix.Transform(new XPoint(11.26, 12.64));
            var p31 = matrix.Transform(new XPoint(13.42, 12.64));
            var p32 = matrix.Transform(new XPoint(13.71, 12.18));
            Shape.DrawLines(mc, new[] { p30, p31, p32, }, endCap: LineCap.ArrowAnchor);
        }
        private static void DrawFig0507a_G1G4(PdfDocument mc, XMatrix matrix)
        {
            var p01 = matrix.Transform(new XPoint(6.33, 11.24));
            var p02 = matrix.Transform(new XPoint(6.99, 11.24));
            var p03 = matrix.Transform(new XPoint(6.85, 11.36));
            var p04 = matrix.Transform(new XPoint(6.47, 11.36));
            Shape.DrawPolygon(mc, new[] { p01, p02, p03, p04, });

            var p05 = p04;
            var p06 = matrix.Transform(new XPoint(6.47, 11.40));
            var p07 = matrix.Transform(new XPoint(6.85, 11.40));
            var p08 = p03;
            Shape.DrawLines(mc, new[] { p05, p06, p07, p08, });

            var p09 = matrix.Transform(new XPoint(6.64, 11.40));
            var p10 = matrix.Transform(new XPoint(6.64, 13.21));
            Shape.DrawLines(mc, new[] { p09, p10, });

            var p11 = matrix.Transform(new XPoint(6.68, 11.40));
            var p12 = matrix.Transform(new XPoint(6.68, 13.21));
            Shape.DrawLines(mc, new[] { p11, p12, });

            var p13 = matrix.Transform(new XPoint(6.28, 13.21));
            var p14 = matrix.Transform(new XPoint(7.04, 13.21));
            var p15 = matrix.Transform(new XPoint(7.04, 13.25));
            var p16 = matrix.Transform(new XPoint(6.28, 13.25));
            Shape.DrawPolygon(mc, new[] { p13, p14, p15, p16, });
        }
        private static void DrawFig0507a_G2G3(PdfDocument mc, XMatrix matrix)
        {
            matrix = new XMatrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.OffsetX, matrix.OffsetY);
            matrix.TranslateAppend(XUnit.FromCentimeter(9.41 - 6.33), 0);

            DrawFig0507a_G1G4(mc, matrix);
        }

        private static void DrawFig0507b(PdfDocument mc, XMatrix matrix)
        {
            // 左の脚柱
            DrawFig0507b_G1G4(mc, matrix);
            // 右の脚柱
            DrawFig0507b_G2G3(mc, matrix);

            // b1
            var p01 = matrix.Transform(new XPoint(5.43, 11.30));
            var p02 = matrix.Transform(new XPoint(5.43, 14.09));
            Shape.DrawLines(mc, new[] { p01, p02, });
            var p03 = matrix.Transform(new XPoint(5.43, 12.36));
            var p04 = matrix.Transform(new XPoint(6.33, 12.36));
            Shape.DrawLines(mc, new[] { p03, p04, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // 2b2
            var p05 = matrix.Transform(new XPoint(6.99, 12.36));
            var p06 = matrix.Transform(new XPoint(9.41, 12.36));
            Shape.DrawLines(mc, new[] { p05, p06, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // 2b3
            var p07 = matrix.Transform(new XPoint(10.07, 12.36));
            var p08 = matrix.Transform(new XPoint(11.18, 12.36));
            Shape.DrawLines(mc, new[] { p07, p08, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // bottom
            var p09 = matrix.Transform(new XPoint(5.43, 14.02));
            var p10 = matrix.Transform(new XPoint(6.66, 14.02));
            var p11 = matrix.Transform(new XPoint(9.74, 14.02));
            var p12 = matrix.Transform(new XPoint(11.18, 14.02));
            Shape.DrawLines(mc, new[] { p09, p10, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p10, p11, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p11, p12, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // 右側中央
            var p13 = matrix.Transform(new XPoint(14.47, 11.30));
            var p14 = matrix.Transform(new XPoint(14.47, 12.73));
            Shape.DrawLines(mc, new[] { p13, p14, });

            // lambda
            var p15 = matrix.Transform(new XPoint(13.86, 11.01));
            var p16 = matrix.Transform(new XPoint(13.86, 12.01));
            Shape.DrawLines(mc, new[] { p15, p16, });
            var p17 = matrix.Transform(new XPoint(12.63, 11.07));
            var p18 = matrix.Transform(new XPoint(13.86, 11.07));
            Shape.DrawLines(mc, new[] { p17, p18, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // a
            var p19 = matrix.Transform(new XPoint(15.09, 11.01));
            var p20 = matrix.Transform(new XPoint(15.09, 12.01));
            Shape.DrawLines(mc, new[] { p19, p20, });
            var p21 = p18;
            var p22 = matrix.Transform(new XPoint(15.09, 11.07));
            Shape.DrawLines(mc, new[] { p21, p22, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // h
            var p23 = matrix.Transform(new XPoint(14.54, 12.04));
            var p24 = matrix.Transform(new XPoint(15.06, 11.76));
            Shape.DrawLines(mc, new[] { p23, p24, });
            var p25 = matrix.Transform(new XPoint(14.54, 12.28));
            var p26 = matrix.Transform(new XPoint(15.06, 12.00));
            Shape.DrawLines(mc, new[] { p25, p26, });
            var p27 = matrix.Transform(new XPoint(15.00, 11.59));
            var p28 = matrix.Transform(new XPoint(15.00, 11.79));
            var p29 = matrix.Transform(new XPoint(15.00, 12.03));
            var p30 = matrix.Transform(new XPoint(15.00, 12.23));
            Shape.DrawLines(mc, new[] { p27, p28, }, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p28, p29, });
            Shape.DrawLines(mc, new[] { p29, p30, }, startCap: LineCap.ArrowAnchor);

            // Bf
            var p31 = matrix.Transform(new XPoint(14.12, 12.44));
            var p32 = matrix.Transform(new XPoint(14.12, 13.34));
            Shape.DrawLines(mc, new[] { p31, p32, });
            var p33 = matrix.Transform(new XPoint(14.82, 12.44));
            var p34 = matrix.Transform(new XPoint(14.82, 13.34));
            Shape.DrawLines(mc, new[] { p33, p34, });
            var p35 = matrix.Transform(new XPoint(14.12, 13.27));
            var p36 = matrix.Transform(new XPoint(14.82, 13.27));
            Shape.DrawLines(mc, new[] { p35, p36, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);
        }
        private static void DrawFig0507b_G1G4(PdfDocument mc, XMatrix matrix)
        {
            // a1
            var p01 = matrix.Transform(new XPoint(6.33, 10.56));
            var p02 = matrix.Transform(new XPoint(6.33, 11.18));
            Shape.DrawLines(mc, new[] { p01, p02, });
            var p03 = matrix.Transform(new XPoint(6.99, 10.56));
            var p04 = matrix.Transform(new XPoint(6.99, 11.18));
            Shape.DrawLines(mc, new[] { p03, p04, });
            var p05 = matrix.Transform(new XPoint(6.33, 10.59));
            var p06 = matrix.Transform(new XPoint(6.99, 10.59));
            Shape.DrawLines(mc, new[] { p05, p06, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // b1
            var p07 = matrix.Transform(new XPoint(6.74, 11.21));
            var p08 = matrix.Transform(new XPoint(7.91, 10.59));
            Shape.DrawLines(mc, new[] { p07, p08, });
            var p09 = matrix.Transform(new XPoint(6.74, 11.33));
            var p10 = matrix.Transform(new XPoint(7.91, 10.71));
            Shape.DrawLines(mc, new[] { p09, p10, });
            var p11 = matrix.Transform(new XPoint(7.83, 10.44));
            var p12 = matrix.Transform(new XPoint(7.83, 10.64));
            var p13 = matrix.Transform(new XPoint(7.83, 10.76));
            var p14 = matrix.Transform(new XPoint(7.83, 10.96));
            Shape.DrawLines(mc, new[] { p11, p12, }, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p12, p13, });
            Shape.DrawLines(mc, new[] { p13, p14, }, startCap: LineCap.ArrowAnchor);

            // lambda1
            var p15 = matrix.Transform(new XPoint(5.73, 11.32));
            var p16 = matrix.Transform(new XPoint(5.73, 11.99));
            Shape.DrawLines(mc, new[] { p15, p16, });
            var p17 = matrix.Transform(new XPoint(6.33, 11.32));
            var p18 = matrix.Transform(new XPoint(6.33, 12.44));
            Shape.DrawLines(mc, new[] { p17, p18, });
            var p19 = matrix.Transform(new XPoint(5.73, 11.93));
            var p20 = matrix.Transform(new XPoint(6.33, 11.93));
            Shape.DrawLines(mc, new[] { p19, p20, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // lambda2
            var p21 = matrix.Transform(new XPoint(6.99, 11.32));
            var p22 = matrix.Transform(new XPoint(6.99, 12.44));
            Shape.DrawLines(mc, new[] { p21, p22 });
            var p23 = matrix.Transform(new XPoint(7.56, 11.32));
            var p24 = matrix.Transform(new XPoint(7.56, 11.99));
            Shape.DrawLines(mc, new[] { p23, p24, });
            var p25 = matrix.Transform(new XPoint(6.99, 11.93));
            var p26 = matrix.Transform(new XPoint(7.56, 11.93));
            Shape.DrawLines(mc, new[] { p25, p26, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // center
            var p27 = matrix.Transform(new XPoint(6.66, 10.69));
            var p28 = matrix.Transform(new XPoint(6.66, 11.41));
            Shape.DrawLines(mc, new[] { p27, p28, });
            var p29 = matrix.Transform(new XPoint(6.66, 13.32));
            var p30 = matrix.Transform(new XPoint(6.66, 14.09));
            Shape.DrawLines(mc, new[] { p29, p30, });
        }
        private static void DrawFig0507b_G2G3(PdfDocument mc, XMatrix matrix)
        {
            matrix = new XMatrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.OffsetX, matrix.OffsetY);
            matrix.TranslateAppend(XUnit.FromCentimeter(9.41 - 6.33), 0);

            DrawFig0507b_G1G4(mc, matrix);
        }

        private static void DrawFig0507c(PdfDocument mc, XMatrix matrix)
        {
            var bkup = mc.currentPos;
            try
            {
                var font = new XFont("MS Mincho", 8, XFontStyle.Regular);

                foreach (var d in new[]
                {
                    new { txt = "a1", pos = new XPoint(6.55, 10.57), angle = 0.0, },
                    new { txt = "a2", pos = new XPoint(9.60, 10.57), angle = 0.0, },
                    new { txt = "h1", pos = new XPoint(7.76, 10.80), angle = -90.0, },
                    new { txt = "h2", pos = new XPoint(10.83, 10.80), angle = -90.0, },
                    new { txt = "λ1", pos = new XPoint(5.86, 11.85), angle = 0.0, },
                    new { txt = "λ2", pos = new XPoint(7.10, 11.85), angle = 0.0, },
                    new { txt = "λ2", pos = new XPoint(8.92, 11.85), angle = 0.0, },
                    new { txt = "λ3", pos = new XPoint(10.15, 11.85), angle = 0.0, },
                    new { txt = "b1", pos = new XPoint(5.75, 12.31), angle = 0.0, },
                    new { txt = "2b2", pos = new XPoint(7.96, 12.31), angle = 0.0, },
                    new { txt = "2b3", pos = new XPoint(10.39, 12.31), angle = 0.0, },
                    new { txt = "1025", pos = new XPoint(5.75, 13.96), angle = 0.0, },
                    new { txt = "2550", pos = new XPoint(7.87, 13.96), angle = 0.0, },
                    new { txt = "2550", pos = new XPoint(10.16, 13.96), angle = 0.0, },
                    new { txt = "G1(G4)", pos = new XPoint(6.42, 14.44), angle = 0.0, },
                    new { txt = "G2(G3)", pos = new XPoint(9.48, 14.44), angle = 0.0, },
    
                    new { txt = "λ", pos = new XPoint(13.14, 11.01), angle = 0.0, },
                    new { txt = "a", pos = new XPoint(14.43, 11.01), angle = 0.0, },
                    new { txt = "45°", pos = new XPoint(14.10, 12.30), angle = -67.5, },
                    new { txt = "h", pos = new XPoint(14.94, 11.98), angle = -90.0, },
                    new { txt = "施工時勾配1:3", pos = new XPoint(11.36, 12.60), angle = 0.0, },
                    new { txt = "Bf", pos = new XPoint(14.34, 13.21), angle = 0.0, },
                })
                {
                    var p = matrix.Transform(d.pos);

                    mc.printText(p.X, p.Y, d.txt, d.angle, font);
                }
            }
            finally
            {
                mc.currentPos = bkup;
            }
        }
    }
}
