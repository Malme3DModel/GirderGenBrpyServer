using PdfSharpCore.Drawing;
using Printing.Comon;
using System;

namespace Printing.Calcrate
{
    internal partial class calcBeam
    {
        private static void DrawFig0502(PdfDocument mc)
        {
            // 図の拡大率(図中のテキストのフォントサイズには影響なし)
            var scale = 1.0;

            // 元々の図のサイズと場所(単位はセンチメートル)
            var oldRect = new XRect(new XPoint(5.21, 17.52), new XPoint(17.65, 24.43));

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

            // 用紙左端から図の中央(橋の横断面？の中央)までの距離
            var centerX = matrix.Transform(new XPoint(11.45, 0)).X;

            var bkup = mc.xpen.Width;
            //mc.xpen.Width = 0.1;
            try
            {
                // 図本体の描画
                DrawFig0502a(mc, matrix, centerX);

                // 寸法線の描画
                DrawFig0502b(mc, matrix, centerX);

                // テキストの描画
                DrawFig0502c(mc, matrix);
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

        private static void DrawFig0502a(PdfDocument mc, XMatrix matrix, double centerX)
        {
            #region 左上

            var p01 = matrix.Transform(new XPoint(6.37, 19.86));
            var p02 = matrix.Transform(new XPoint(6.61, 19.86));
            var p03 = matrix.Transform(new XPoint(6.61, 19.96));
            var p04 = matrix.Transform(new XPoint(6.37, 19.96));

            var p05 = matrix.Transform(new XPoint(6.40, 19.96));
            var p06 = matrix.Transform(new XPoint(6.40, 20.76));

            var p07 = matrix.Transform(new XPoint(6.53, 19.96));
            var p08 = matrix.Transform(new XPoint(6.53, 20.76));

            var p09 = matrix.Transform(new XPoint(6.53, 20.15));
            var p10 = matrix.Transform(new XPoint(6.61, 20.15));
            var p11 = matrix.Transform(new XPoint(6.61, 20.26));
            var p12 = matrix.Transform(new XPoint(6.53, 20.26));

            var p13 = matrix.Transform(new XPoint(6.53, 20.44));
            var p14 = matrix.Transform(new XPoint(6.61, 20.44));
            var p15 = matrix.Transform(new XPoint(6.61, 20.55));
            var p16 = matrix.Transform(new XPoint(6.53, 20.55));

            var p17 = matrix.Transform(new XPoint(6.19, 20.76));
            var p18 = matrix.Transform(new XPoint(6.86, 20.76));
            var p19 = matrix.Transform(new XPoint(6.86, 21.11));
            var p20 = matrix.Transform(new XPoint(6.19, 21.11));

            var p60 = matrix.Transform(new XPoint(6.46, 19.91));
            var p61 = matrix.Transform(new XPoint(6.46, 20.76));

            Shape.DrawPolygon(mc, new[] { p01, p02, p03, p04, });
            Shape.DrawLines(mc, new[] { p05, p06, });
            Shape.DrawLines(mc, new[] { p07, p08, });
            Shape.DrawLines(mc, new[] { p09, p10, p11, p12, });
            Shape.DrawLines(mc, new[] { p13, p14, p15, p16, });
            Shape.DrawPolygon(mc, new[] { p17, p18, p19, p20, });
            Shape.DrawLines(mc, new[] { p60, p61, });

            #endregion

            #region 右上

            Shape.DrawPolygon(mc, new[] { p01, p02, p03, p04, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p05, p06, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p07, p08, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p09, p10, p11, p12, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p13, p14, p15, p16, }.YAxisSymmetry(centerX));
            Shape.DrawPolygon(mc, new[] { p17, p18, p19, p20, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p60, p61, }.YAxisSymmetry(centerX));

            #endregion

            #region 左中央

            var p21 = matrix.Transform(new XPoint(6.86, 21.03));
            var p22 = matrix.Transform(new XPoint(11.45, 20.93));

            var p23 = p20;
            var p24 = matrix.Transform(new XPoint(6.86, 21.11));
            var p25 = matrix.Transform(new XPoint(11.45, 21.03));

            var p26 = matrix.Transform(new XPoint(6.19, 21.36));
            var p27 = matrix.Transform(new XPoint(11.45, 21.24));

            Shape.DrawLines(mc, new[] { p21, p22, });
            Shape.DrawLines(mc, new[] { p24, p25, });
            Shape.DrawLines(mc, new[] { p26, p27, });
            Shape.DrawLines(mc, new[] { p23, p26, });

            #endregion

            #region 右中央

            Shape.DrawLines(mc, new[] { p21, p22, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p24, p25, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p26, p27, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p23, p26, }.YAxisSymmetry(centerX));

            #endregion

            #region 左下(G1)

            var p28 = matrix.Transform(new XPoint(6.19, 21.36));
            var p29 = matrix.Transform(new XPoint(7.16, 21.39));
            var p30 = matrix.Transform(new XPoint(7.50, 21.39));
            var p31 = matrix.Transform(new XPoint(7.59, 21.33));

            var p32 = p29;
            var p33 = matrix.Transform(new XPoint(7.16, 21.43));
            var p34 = matrix.Transform(new XPoint(7.50, 21.43));
            var p35 = p30;

            var p36 = matrix.Transform(new XPoint(7.32, 21.43));
            var p37 = matrix.Transform(new XPoint(7.32, 23.21));

            var p38 = matrix.Transform(new XPoint(7.34, 21.43));
            var p39 = matrix.Transform(new XPoint(7.34, 23.21));

            var p40 = matrix.Transform(new XPoint(7.01, 23.21));
            var p41 = matrix.Transform(new XPoint(7.66, 23.21));
            var p42 = matrix.Transform(new XPoint(7.66, 23.24));
            var p43 = matrix.Transform(new XPoint(7.01, 23.24));

            Shape.DrawLines(mc, new[] { p28, p29, p30, p31, });
            Shape.DrawLines(mc, new[] { p32, p33, p34, p35, });
            Shape.DrawLines(mc, new[] { p36, p37, });
            Shape.DrawLines(mc, new[] { p38, p39, });
            Shape.DrawPolygon(mc, new[] { p40, p41, p42, p43, });

            #endregion

            #region 右下(G4)

            Shape.DrawLines(mc, new[] { p28, p29, p30, p31, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p32, p33, p34, p35, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p36, p37, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p38, p39, }.YAxisSymmetry(centerX));
            Shape.DrawPolygon(mc, new[] { p40, p41, p42, p43, }.YAxisSymmetry(centerX));

            #endregion

            #region 左下(G2)

            var p44 = matrix.Transform(new XPoint(9.78, 21.28));
            var p45 = matrix.Transform(new XPoint(9.93, 21.39));
            var p46 = matrix.Transform(new XPoint(10.26, 21.39));
            var p47 = matrix.Transform(new XPoint(10.43, 21.26));

            var p48 = p45;
            var p49 = matrix.Transform(new XPoint(9.93, 21.43));
            var p50 = matrix.Transform(new XPoint(10.26, 21.43));
            var p51 = p46;

            var p52 = matrix.Transform(new XPoint(10.08, 21.43));
            var p53 = matrix.Transform(new XPoint(10.08, 23.21));

            var p54 = matrix.Transform(new XPoint(10.10, 21.43));
            var p55 = matrix.Transform(new XPoint(10.10, 23.21));

            var p56 = matrix.Transform(new XPoint(9.78, 23.21));
            var p57 = matrix.Transform(new XPoint(10.43, 23.21));
            var p58 = matrix.Transform(new XPoint(10.43, 23.24));
            var p59 = matrix.Transform(new XPoint(9.78, 23.24));

            Shape.DrawLines(mc, new[] { p44, p45, p46, p47, });
            Shape.DrawLines(mc, new[] { p48, p49, p50, p51, });
            Shape.DrawLines(mc, new[] { p52, p53, });
            Shape.DrawLines(mc, new[] { p54, p55, });
            Shape.DrawPolygon(mc, new[] { p56, p57, p58, p59, });

            #endregion

            #region 右下(G3)

            Shape.DrawLines(mc, new[] { p44, p45, p46, p47, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p48, p49, p50, p51, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p52, p53, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p54, p55, }.YAxisSymmetry(centerX));
            Shape.DrawPolygon(mc, new[] { p56, p57, p58, p59, }.YAxisSymmetry(centerX));

            #endregion
        }

        private static void DrawFig0502b(PdfDocument mc, XMatrix matrix, double centerX)
        {
            var p01 = matrix.Transform(new XPoint(6.19, 18.31));
            var p02 = matrix.Transform(new XPoint(11.45, 18.31));

            var p03 = matrix.Transform(new XPoint(6.19, 18.75));
            var p04 = matrix.Transform(new XPoint(6.86, 18.75));
            var p05 = matrix.Transform(new XPoint(11.45, 18.75));

            var p06 = matrix.Transform(new XPoint(5.91, 19.19));
            var p07 = matrix.Transform(new XPoint(6.19, 19.19));
            var p08 = matrix.Transform(new XPoint(6.46, 19.19));
            var p09 = matrix.Transform(new XPoint(6.86, 19.19));
            var p10 = matrix.Transform(new XPoint(7.14, 19.19));

            var p11 = p01;
            var p12 = matrix.Transform(new XPoint(6.19, 20.71));

            var p13 = p08;
            var p14 = matrix.Transform(new XPoint(6.46, 19.86));

            var p15 = p04;
            var p16 = matrix.Transform(new XPoint(6.86, 20.71));

            var p17 = matrix.Transform(new XPoint(7.46, 19.64));
            var p18 = matrix.Transform(new XPoint(7.46, 19.93));
            var p19 = matrix.Transform(new XPoint(7.46, 20.18));
            var p20 = matrix.Transform(new XPoint(7.46, 20.44));

            var p21 = matrix.Transform(new XPoint(6.93, 20.71));
            var p22 = matrix.Transform(new XPoint(7.46, 19.93));

            var p23 = matrix.Transform(new XPoint(6.93, 20.95));
            var p24 = matrix.Transform(new XPoint(7.46, 20.18));

            var p25 = matrix.Transform(new XPoint(5.47, 20.49));
            var p26 = matrix.Transform(new XPoint(5.47, 20.76));
            var p27 = matrix.Transform(new XPoint(5.47, 21.11));
            var p28 = matrix.Transform(new XPoint(5.47, 21.37));

            var p29 = p26;
            var p30 = matrix.Transform(new XPoint(6.14, 20.76));

            var p31 = p27;
            var p32 = matrix.Transform(new XPoint(6.14, 21.11));

            Shape.DrawLines(mc, new[] { p01, p02, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p03, p04, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p04, p05, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p06, p07, }, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p07, p08, }, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p08, p09, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p09, p10, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p11, p12, });
            Shape.DrawLines(mc, new[] { p13, p14, });
            Shape.DrawLines(mc, new[] { p15, p16, });
            Shape.DrawLines(mc, new[] { p17, p18, }, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p18, p19, });
            Shape.DrawLines(mc, new[] { p19, p20, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p21, p22, });
            Shape.DrawLines(mc, new[] { p23, p24, });
            Shape.DrawLines(mc, new[] { p25, p26, }, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p26, p27, });
            Shape.DrawLines(mc, new[] { p27, p28, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p29, p30, });
            Shape.DrawLines(mc, new[] { p31, p32, });

            Shape.DrawLines(mc, new[] { p01, p02, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p03, p04, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p04, p05, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p06, p07, }.YAxisSymmetry(centerX), endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p07, p08, }.YAxisSymmetry(centerX), endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p08, p09, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p09, p10, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p11, p12, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p13, p14, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p15, p16, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p17, p18, }.YAxisSymmetry(centerX), endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p18, p19, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p19, p20, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p21, p22, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p23, p24, }.YAxisSymmetry(centerX));

            // アスファルト舗装厚の吹き出し
            var p33 = matrix.Transform(new XPoint(8.04, 19.59));
            var p34 = matrix.Transform(new XPoint(11.60, 19.59));
//            var p35 = p33;
            var p36 = matrix.Transform(new XPoint(8.04, 21.00));
            Shape.DrawLines(mc, new[] { p34, p33, p36, }, endCap: LineCap.ArrowAnchor);

            // 鉄筋コンクリート床版厚の吹き出し
            var p37 = matrix.Transform(new XPoint(8.24, 20.11));
            var p38 = matrix.Transform(new XPoint(12.48, 20.11));
///            var p39 = p37;
            var p40 = matrix.Transform(new XPoint(8.24, 21.28));
            Shape.DrawLines(mc, new[] { p38, p37, p40, }, endCap: LineCap.ArrowAnchor);

            // 2%傾斜
            var p41 = matrix.Transform(new XPoint(8.84, 20.82));
            var p42 = matrix.Transform(new XPoint(10.31, 20.79));
            Shape.DrawLines(mc, new[] { p41, p42, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p41, p42, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);

            var p43 = matrix.Transform(new XPoint(6.83, 21.34));
            var p44 = matrix.Transform(new XPoint(6.83, 21.63));
            var p45 = matrix.Transform(new XPoint(6.83, 21.69));
            var p46 = matrix.Transform(new XPoint(6.83, 21.95));

            var p47 = p44;
            var p48 = matrix.Transform(new XPoint(7.28, 21.36));
            var p49 = p45;
            var p50 = matrix.Transform(new XPoint(7.28, 21.42));

            Shape.DrawLines(mc, new[] { p43, p44, }, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p44, p45, });
            Shape.DrawLines(mc, new[] { p45, p46, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p47, p48, });
            Shape.DrawLines(mc, new[] { p49, p50, });

            var p51 = matrix.Transform(new XPoint(9.70, 21.32));
            var p52 = matrix.Transform(new XPoint(9.70, 21.56));
            var p53 = matrix.Transform(new XPoint(9.70, 21.67));
            var p54 = matrix.Transform(new XPoint(9.70, 21.95));

            var p55 = p52;
            var p56 = matrix.Transform(new XPoint(10.04, 21.30));
            var p57 = p53;
            var p58 = matrix.Transform(new XPoint(10.02, 21.43));

            Shape.DrawLines(mc, new[] { p51, p52, }, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p52, p53, });
            Shape.DrawLines(mc, new[] { p53, p54, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p55, p56, });
            Shape.DrawLines(mc, new[] { p57, p58, });

            var p59 = matrix.Transform(new XPoint(6.19, 21.40));
            var p60 = matrix.Transform(new XPoint(6.19, 24.37));

            var p61 = matrix.Transform(new XPoint(7.33, 23.30));
            var p62 = matrix.Transform(new XPoint(7.33, 24.37));

            var p63 = p60;
            var p64 = p62;

            var p65 = p62;
            var p66 = matrix.Transform(new XPoint(11.45, 24.37));

            Shape.DrawLines(mc, new[] { p59, p60, });
            Shape.DrawLines(mc, new[] { p61, p62, });
            Shape.DrawLines(mc, new[] { p63, p64, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p65, p66, }, startCap: LineCap.ArrowAnchor);

            Shape.DrawLines(mc, new[] { p59, p60, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p61, p62, }.YAxisSymmetry(centerX));
            Shape.DrawLines(mc, new[] { p63, p64, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p65, p66, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);

            var p67 = matrix.Transform(new XPoint(16.31, 21.39));
            var p68 = matrix.Transform(new XPoint(17.52, 21.39));

            var p69 = matrix.Transform(new XPoint(15.95, 23.21));
            var p70 = matrix.Transform(new XPoint(17.52, 23.21));

            var p71 = p68;
            var p72 = p70;

            Shape.DrawLines(mc, new[] { p67, p68, });
            Shape.DrawLines(mc, new[] { p69, p70, });
            Shape.DrawLines(mc, new[] { p71, p72, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // 中心線
            var p73 = matrix.Transform(new XPoint(11.45, 17.84));
            var p74 = matrix.Transform(new XPoint(11.45, 23.34));
            var line_len1 = 5 / mc.xpen.Width;
            var empty_len1 = 0.5 / mc.xpen.Width;
            var line_len2 = 0.5 / mc.xpen.Width;
            var empty_len2 = 0.5 / mc.xpen.Width;
            Shape.DrawLines(mc, new[] { p73, p74, }, dashPattern: new[] { line_len1, empty_len1, line_len2, empty_len2, });

            // 添架物の横線
            var p75 = matrix.Transform(new XPoint(11.33, 22.07));
            var p76 = matrix.Transform(new XPoint(11.57, 22.07));
            Shape.DrawLines(mc, new[] { p75, p76, });

            // 添架物の円
            var p80 = matrix.Transform(new XPoint(11.45, 22.07));
            const double radius = 2;
            Shape.Drawcircle(mc, p80 - new XVector(radius, radius), new XSize(radius * 2, radius * 2));

            // 添架物の吹き出し
            var p77 = matrix.Transform(new XPoint(10.27, 22.44));
            var p78 = matrix.Transform(new XPoint(11.17, 22.44));
            var p79 = matrix.Transform(new XPoint(11.45, 22.07));
            Shape.DrawLines(mc, new[] { p77, p78, p79, }, endCap: LineCap.ArrowAnchor);
        }

        private static void DrawFig0502c(PdfDocument mc, XMatrix matrix)
        {
            var bkup = mc.currentPos;
            try
            {
                var font = new XFont("MS Mincho", 8, XFontStyle.Regular);

                foreach (var d in new[]
                {
                    new { str = "\u2104", p = new XPoint(11.36, 17.81), deg = 0.0, }, // CL
                    new { str = "9700", p = new XPoint(11.14, 18.25), deg = 0.0, },
                    new { str = "8500", p = new XPoint(11.14, 18.70), deg = 0.0, },
                    new { str = "600", p = new XPoint(6.30, 18.70), deg = 0.0, },
                    new { str = "250", p = new XPoint(5.90, 19.15), deg = 0.0, },
                    new { str = "350", p = new XPoint(6.43, 19.15), deg = 0.0, },
                    new { str = "600", p = new XPoint(16.15, 18.70), deg = 0.0, },
                    new { str = "350", p = new XPoint(16.00, 19.15), deg = 0.0, },
                    new { str = "250", p = new XPoint(16.55, 19.15), deg = 0.0, },
                    new { str = "アスファルト舗装厚75mm", p = new XPoint(8.20, 19.53), deg = 0.0, },
                    new { str = "鉄筋コンクリート床版厚220mm", p = new XPoint(8.39, 20.05), deg = 0.0, },
                    new { str = "2%直線", p = new XPoint(9.15, 20.75), deg = -Math.Atan(0.02) * 180 / Math.PI, },
                    new { str = "2%直線", p = new XPoint(12.66, 20.73), deg = Math.Atan(0.02) * 180 / Math.PI, },
                    new { str = "325", p = new XPoint(5.44, 21.14), deg = -90.0, },
                    new { str = "250", p = new XPoint(7.43, 20.25), deg = -90.0, },
                    new { str = "250", p = new XPoint(15.42, 20.29), deg = -90.0, },
                    new { str = "60", p = new XPoint(6.77, 21.78), deg = -90.0, },
                    new { str = "111", p = new XPoint(9.56, 21.78), deg = -90.0, },
                    new { str = "添架物", p = new XPoint(10.36, 22.43), deg = 0.0, },
                    new { str = "1700", p = new XPoint(17.48, 22.55), deg = -90.0, },
                    new { str = "G1", p = new XPoint(7.16, 23.67), deg = 0.0, },
                    new { str = "G2", p = new XPoint(9.95, 23.67), deg = 0.0, },
                    new { str = "G3", p = new XPoint(12.70, 23.67), deg = 0.0, },
                    new { str = "G4", p = new XPoint(15.43, 23.67), deg = 0.0, },
                    new { str = "1025", p = new XPoint(6.47, 24.31), deg = 0.0, },
                    new { str = "3×2550＝7650", p = new XPoint(10.52, 24.31), deg = 0.0, },
                    new { str = "1025", p = new XPoint(15.85, 24.31), deg = 0.0, },
                })
                {
                    var p = matrix.Transform(d.p);

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
