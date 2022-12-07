using PdfSharpCore.Drawing;
using Printing.Comon;
using System;
using System.Linq;

namespace Printing.Calcrate
{
    internal partial class calcBeam
    {
        private static void DrawFig0505(PdfDocument mc)
        {
            // 図の拡大率(図中のテキストのフォントサイズには影響なし)
            var scale = 1.0;

            // 元々の図のサイズと場所(単位はセンチメートル)
            var oldRect = new XRect(new XPoint(3.73, 4.57), new XPoint(17.38, 19.85));

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

            var center = matrix.Transform(new XPoint(10.66, 10.89));

            var bkup = mc.xpen.Width;
            //mc.xpen.Width = 0.1;
            try
            {
                // 図本体の描画
                DrawFig0505a(mc, matrix, center);

                // 寸法線の描画
                DrawFig0505b(mc, matrix, center);

                // テキストの描画
                DrawFig0505c(mc, matrix);
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

        private static void DrawFig0505a(PdfDocument mc, XMatrix matrix, XPoint center)
        {
            var centerX = center.X;

            // 両側の縦線
            var p01 = matrix.Transform(new XPoint(4.93, 5.36));
            var p02 = matrix.Transform(new XPoint(4.93, 18.23));
            Shape.DrawLines(mc, new[] { p01, p02, });
            Shape.DrawLines(mc, new[] { p01, p02, }.YAxisSymmetry(centerX));

            // 両側の縦線の目盛り
            for (int i = 0; i <= 10; ++i)
            {
                var y = (16.02 - 5.76) * i / 10 + 5.76;
                var pa = matrix.Transform(new XPoint(4.83, y));
                var pb = matrix.Transform(new XPoint(5.03, y));
                Shape.DrawLines(mc, new[] { pa, pb, });
                Shape.DrawLines(mc, new[] { pa, pb, }.YAxisSymmetry(centerX));
            }

            // 中央の横線
            var p03 = matrix.Transform(new XPoint(4.93, 10.89));
            var p04 = matrix.Transform(new XPoint(10.66, 10.89));
            Shape.DrawLines(mc, new[] { p03, p04, });
            Shape.DrawLines(mc, new[] { p03, p04, }.YAxisSymmetry(centerX));

            // 3本の縦線
            var p05 = matrix.Transform(new XPoint(8.49, 7.82));
            var p06 = matrix.Transform(new XPoint(8.49, 13.96));
            Shape.DrawLines(mc, new[] { p05, p06 });
            Shape.DrawLines(mc, new[] { p05, p06 }.YAxisSymmetry(centerX));
            var p07 = matrix.Transform(new XPoint(10.66, 7.82));
            var p08 = matrix.Transform(new XPoint(10.66, 13.96));
            Shape.DrawLines(mc, new[] { p07, p08 });

            // 2本の縦線
            var p09 = matrix.Transform(new XPoint(8.49, 17.10));
            var p10 = matrix.Transform(new XPoint(8.49, 18.23));
            Shape.DrawLines(mc, new[] { p09, p10, });
            Shape.DrawLines(mc, new[] { p09, p10, }.YAxisSymmetry(centerX));

            // 斜線
            var p11 = matrix.Transform(new XPoint(4.93, 15.14));
            var p12 = matrix.Transform(new XPoint(10.66, 11.84));
            Shape.DrawLines(mc, new[] { p11, p12, });
            Shape.DrawLines(mc, new[] { p11, p12, }.PointSymmetry(center));

            // 二次曲線
            var ndivide = 20;
            var xAxisIntersection = matrix.Transform(new XPoint(4.93, 10.89));
            foreach (var localMinimumPoint in new[]
            {
                matrix.Transform(new XPoint(10.66, 11.16)),
                matrix.Transform(new XPoint(10.66, 12.44)),
                matrix.Transform(new XPoint(10.66, 12.90)),
            })
            {
                var a = -(localMinimumPoint.Y - xAxisIntersection.Y) / Math.Pow(xAxisIntersection.X - localMinimumPoint.X, 2);
                var b = localMinimumPoint.Y;
                Shape.DrawLines(mc, Enumerable.Range(0, ndivide + 1).Select(i =>
                {
                    var x = (localMinimumPoint.X - xAxisIntersection.X) * 2 / ndivide * i + xAxisIntersection.X;
                    var y = a * Math.Pow(x - localMinimumPoint.X, 2) + b;
                    return new XPoint(x, y);
                }).ToArray());
            }

            // 吹き出し
            foreach (var p in new[]
            {
                new XPoint(11.61, 11.15),
                new XPoint(13.02, 12.17),
                new XPoint(11.48, 12.84),
            })
            {
                var pa = matrix.Transform(p);
                var pb = matrix.Transform(p + new XVector(0.45, 0.56));
                var pc = matrix.Transform(p + new XVector(0.92, 0.56));
                Shape.DrawLines(mc, new[] { pa, pb, pc, }, startCap: LineCap.ArrowAnchor);
            }

            // ???
            var p13 = matrix.Transform(new XPoint(4.79, 18.27));
            var p14 = matrix.Transform(new XPoint(10.66, 18.27));
            Shape.DrawLines(mc, new XPoint[] { p13, p14, });
            Shape.DrawLines(mc, new XPoint[] { p13, p14, }.YAxisSymmetry(centerX));
            var p15 = matrix.Transform(new XPoint(4.79, 18.40));
            var p16 = matrix.Transform(new XPoint(10.66, 18.40));
            Shape.DrawLines(mc, new XPoint[] { p15, p16, });
            Shape.DrawLines(mc, new XPoint[] { p15, p16, }.YAxisSymmetry(centerX));
            var p17 = matrix.Transform(new XPoint(4.79, 18.86));
            var p18 = matrix.Transform(new XPoint(10.66, 18.86));
            Shape.DrawLines(mc, new XPoint[] { p17, p18, });
            Shape.DrawLines(mc, new XPoint[] { p17, p18, }.YAxisSymmetry(centerX));

            Shape.DrawLines(mc, new XPoint[] { p13, p17, });
            Shape.DrawLines(mc, new XPoint[] { p13, p17, }.YAxisSymmetry(centerX));
            var p19 = matrix.Transform(new XPoint(8.49, 18.27));
            var p20 = matrix.Transform(new XPoint(8.49, 18.86));
            Shape.DrawLines(mc, new[] { p19, p20, });
            Shape.DrawLines(mc, new[] { p19, p20, }.YAxisSymmetry(centerX));

            for (var i = 0; i < 25; ++i)
            {
                var x = (10.66 - 4.93) * 2 / 24 * i + 4.93;
                var pa = matrix.Transform(new XPoint(x, 18.27));
                var pb = matrix.Transform(new XPoint(x, 18.86));
                Shape.DrawLines(mc, new[] { pa, pb });
            }

            // S字
            foreach (var p in new[]
            {
                new XPoint(8.49, 18.27),
                new XPoint(8.49, 18.27).YAxisSymmetry(10.66), // 「XUnit.FromPoint(center.X).Centimeter」では描画位置が少しずれるので「10.66」を使用
            })
            {
                var deg = 90.0; // 円弧の中心角
                var c = 0.8; // 円弧の直線部分の比率
                var rad = deg / 180.0 * Math.PI;
                var h = (18.86 - 18.27) / 2 * c; // 円弧の直線部分の長さ
                var r = h / 2 / Math.Sin(rad / 2); // 円弧の半径

                var cx = p.X + r * Math.Cos(rad / 2);
                var cy = p.Y + h / c * (1 - c / 2);
                var pt0 = matrix.Transform(new XPoint(cx - r, cy - r));
                var pt1 = matrix.Transform(new XPoint(cx + r, cy + r));
                Shape.DrawArc(mc, pt0, pt1, 180 - deg / 2, deg);

                var cx2 = p.X - r * Math.Cos(rad / 2);
                var cy2 = p.Y + h / c * (1 + c / 2);
                var pt2 = matrix.Transform(new XPoint(cx2 - r, cy2 - r));
                var pt3 = matrix.Transform(new XPoint(cx2 + r, cy2 + r));
                Shape.DrawArc(mc, pt2, pt3, -deg / 2, deg);
            }

            // ???の支点
            var p21 = matrix.Transform(new XPoint(4.93, 18.86));
            var p22 = matrix.Transform(new XPoint(4.88, 18.96));
            var p23 = matrix.Transform(new XPoint(4.98, 18.96));
            Shape.DrawPolygon(mc, new[] { p21, p22, p23, });
            Shape.DrawPolygon(mc, new[] { p21, p22, p23, }.YAxisSymmetry(centerX));
        }

        private static void DrawFig0505b(PdfDocument mc, XMatrix matrix, XPoint center)
        {
            var centerX = center.X;

            // 断面長の寸法線
            var p01 = matrix.Transform(new XPoint(3.73, 17.10));
            var p02 = matrix.Transform(new XPoint(4.93, 17.10));
            var p03 = matrix.Transform(new XPoint(8.49, 17.10));
            var p04 = matrix.Transform(new XPoint(10.66, 17.10));
            Shape.DrawLines(mc, new[] { p01, p02, });
            Shape.DrawLines(mc, new[] { p02, p03, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p03, p04, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p02, p03, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p03, p04, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);

            // 配力鉄筋の寸法線
            var p05 = matrix.Transform(new XPoint(3.73, 17.43));
            var p06 = matrix.Transform(new XPoint(4.93, 17.43));
            var p07 = matrix.Transform(new XPoint(10.66, 17.43));
            Shape.DrawLines(mc, new[] { p05, p06, });
            Shape.DrawLines(mc, new[] { p06, p07, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p06, p07, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);

            // ???の寸法線
            var p08 = matrix.Transform(new XPoint(4.16, 18.27));
            var p09 = matrix.Transform(new XPoint(4.74, 18.27));
            Shape.DrawLines(mc, new[] { p08, p09, });
            var p10 = matrix.Transform(new XPoint(4.16, 18.86));
            var p11 = matrix.Transform(new XPoint(4.74, 18.86));
            Shape.DrawLines(mc, new[] { p10, p11, });
            var p12 = p08;
            var p13 = p10;
            Shape.DrawLines(mc, new[] { p12, p13, }, startCap: LineCap.ArrowAnchor, endCap: LineCap.ArrowAnchor);

            // 対傾構間隔と支間長の寸法線
            var p14 = matrix.Transform(new XPoint(4.93, 18.93));
            var p15 = matrix.Transform(new XPoint(4.93, 19.81));
            Shape.DrawLines(mc, new[] { p14, p15, });
            Shape.DrawLines(mc, new[] { p14, p15, }.YAxisSymmetry(centerX));

            var p16 = matrix.Transform(new XPoint(3.73, 19.46));
            var p17 = matrix.Transform(new XPoint(4.93, 19.46));
            var p18 = matrix.Transform(new XPoint(10.66, 19.46));
            Shape.DrawLines(mc, new[] { p16, p17, });
            Shape.DrawLines(mc, new[] { p17, p18, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p17, p18, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);

            var p19 = matrix.Transform(new XPoint(3.73, 19.81));
            var p20 = matrix.Transform(new XPoint(4.93, 19.81));
            var p21 = matrix.Transform(new XPoint(10.66, 19.81));
            Shape.DrawLines(mc, new[] { p19, p20, });
            Shape.DrawLines(mc, new[] { p20, p21, }, startCap: LineCap.ArrowAnchor);
            Shape.DrawLines(mc, new[] { p20, p21, }.YAxisSymmetry(centerX), startCap: LineCap.ArrowAnchor);
        }

        private static void DrawFig0505c(PdfDocument mc, XMatrix matrix)
        {
            var bkup = mc.currentPos;
            try
            {
                var fontL = new XFont("MS Mincho", 8.0, XFontStyle.Regular);

                foreach (var d in new[]
                {
                    new { txt = "G1 (G4)", pos = new XPoint(10.28, 4.91), angle = 0, },
    
                    new { txt = "[ kN・m ]", pos = new XPoint(4.02, 5.22), angle = 0, },
                    new { txt = "-10,000", pos = new XPoint(3.85, 5.79), angle = 0, },
                    new { txt = " -8,000", pos = new XPoint(3.85, 6.76), angle = 0, },
                    new { txt = " -6,000", pos = new XPoint(3.85, 7.78), angle = 0, },
                    new { txt = " -4,000", pos = new XPoint(3.85, 8.82), angle = 0, },
                    new { txt = " -2,000", pos = new XPoint(3.85, 9.84), angle = 0, },
                    new { txt = "      0", pos = new XPoint(3.85, 10.88), angle = 0, },
                    new { txt = "  2,000", pos = new XPoint(3.85, 11.88), angle = 0, },
                    new { txt = "  4,000", pos = new XPoint(3.85, 12.92), angle = 0, },
                    new { txt = "  6,000", pos = new XPoint(3.85, 13.94), angle = 0, },
                    new { txt = "  8,000", pos = new XPoint(3.85, 14.98), angle = 0, },
                    new { txt = " 10,000", pos = new XPoint(3.85, 15.98), angle = 0, },
                    new { txt = "S(R) = 837 kN", pos = new XPoint(5.35, 14.78), angle = -90, },
    
                    new { txt = "[ kN ]", pos = new XPoint(16.26, 5.22), angle = 0, },
                    new { txt = "-1,000", pos = new XPoint(16.56, 5.79), angle = 0, },
                    new { txt = "-800", pos = new XPoint(16.56, 6.76), angle = 0, },
                    new { txt = "-600", pos = new XPoint(16.56, 7.78), angle = 0, },
                    new { txt = "-400", pos = new XPoint(16.56, 8.82), angle = 0, },
                    new { txt = "-200", pos = new XPoint(16.56, 9.84), angle = 0, },
                    new { txt = "0", pos = new XPoint(16.56, 10.88), angle = 0, },
                    new { txt = "200", pos = new XPoint(16.56, 11.88), angle = 0, },
                    new { txt = "400", pos = new XPoint(16.56, 12.92), angle = 0, },
                    new { txt = "600", pos = new XPoint(16.56, 13.94), angle = 0, },
                    new { txt = "800", pos = new XPoint(16.56, 14.98), angle = 0, },
                    new { txt = "1,000", pos = new XPoint(16.56, 15.98), angle = 0, },
                    new { txt = "S(R) = 837 kN", pos = new XPoint(16.13, 8.43), angle = -90, },
    
                    new { txt = "Mv       = 3,278 kN・m", pos = new XPoint(8.43, 8.58), angle = -90, },
                    //new { txt = "Ms(MDb)  = 2,562 kN・m", pos = new XPoint(8.80, 8.58), angle = -90, },
                    //new { txt = "Mvd(MDa) =   542 kN・m", pos = new XPoint(9.10, 8.58), angle = -90, },
    
                    new { txt = "Mv       = 3,981 kN・m", pos = new XPoint(10.58, 8.58), angle = -90, },
                    //new { txt = "Ms(MDb)  = 3,008 kN・m", pos = new XPoint(10.96, 8.58), angle = -90, },
                    //new { txt = "Mvd(MDa) =   500 kN・m", pos = new XPoint(11.26, 8.58), angle = -90, },
    
                    new { txt = "Mv       = 3,278 kN・m", pos = new XPoint(12.75, 8.58), angle = -90, },
                    //new { txt = "Ms(MDb)  = 2,562 kN・m", pos = new XPoint(13.14, 8.58), angle = -90, },
                    //new { txt = "Mvd(MDa) =   542 kN・m", pos = new XPoint(13.44, 8.58), angle = -90, },
    
                    new { txt = "Mvd", pos = new XPoint(12.08, 11.66), angle = 0, },
                    //new { txt = "(MDa)", pos = new XPoint(12.01, 12.00), angle = 0, },
                    new { txt = "Ms", pos = new XPoint(13.55, 12.68), angle = 0, },
                    //new { txt = "(MDb)", pos = new XPoint(13.42, 13.02), angle = 0, },
                    new { txt = "Mv", pos = new XPoint(12.03, 13.34), angle = 0, },
    
                    //new { txt = "Ms(MDb) ：合成前死荷重曲げモーメント", pos = new XPoint(11.15, 15.26), angle = 0, },
                    //new { txt = "Mvd(MDa)：合成後死荷重曲げモーメント", pos = new XPoint(11.15, 15.66), angle = 0, },
                    new { txt = "Mv      ：合成後曲げモーメント", pos = new XPoint(11.15, 16.06), angle = 0, },
                    new { txt = "（注）特性値による断面力を示す", pos = new XPoint(11.15, 16.50), angle = 0, },
    
                    //new { txt = "断面長", pos = new XPoint(3.95, 17.06), angle = 0, },
                    new { txt = "10250", pos = new XPoint(6.35, 17.06), angle = 0, },
                    new { txt = "12500", pos = new XPoint(10.30, 17.06), angle = 0, },
                    new { txt = "10250", pos = new XPoint(14.22, 17.06), angle = 0, },
                    //new { txt = "配力鉄筋", pos = new XPoint(3.86, 17.37), angle = 0, },
                    //new { txt = "上段 D19@120 / 下段 D19@120", pos = new XPoint(9.08, 17.37), angle = 0, },
    
                    new { txt = "1700", pos = new XPoint(4.10, 18.83), angle = -90, },
                    new { txt = "Sec-2", pos = new XPoint(6.24, 17.98), angle = 0, },
                    new { txt = "J1", pos = new XPoint(8.18, 18.15), angle = 0, },
                    new { txt = "Sec-1", pos = new XPoint(10.26, 17.98), angle = 0, },
                    new { txt = "J2", pos = new XPoint(12.84, 18.15), angle = 0, },
                    new { txt = "Sec-2", pos = new XPoint(14.11, 17.98), angle = 0, },
    
                    //new { txt = "対傾構間隔", pos = new XPoint(3.74, 19.44), angle = 0, },
                    new { txt = "6  ×  5500 ＝ 33000", pos = new XPoint(9.45, 19.44), angle = 0, },
                    //new { txt = "支間長", pos = new XPoint(3.88, 19.78), angle = 0, },
                    new { txt = "33000", pos = new XPoint(10.28, 19.78), angle = 0, },
                })
                {
                    var p = matrix.Transform(d.pos);

                    mc.printText(p.X, p.Y, d.txt, d.angle, fontL);
                }

                // 下付き文字を含む文字列の描画

                var fontS = new XFont("MS Mincho", 6, XFontStyle.Regular);

                foreach (var d in new[]
                {
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("Ms(M", fontL);
                                context.PrtText("Db", fontS);
                                context.PrtText(")  = 2,562 kN・m", fontL);
                            }
                        }),
                        pos = new XPoint(8.80, 8.58), angle = -90,
                    },
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("Mvd(M", fontL);
                                context.PrtText("Da", fontS);
                                context.PrtText(") =   542 kN・m", fontL);
                            }
                        }),
                        pos = new XPoint(9.10, 8.58), angle = -90,
                    },
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("Ms(M", fontL);
                                context.PrtText("Db", fontS);
                                context.PrtText(")  = 3,008 kN・m", fontL);
                            }
                        }),
                        pos = new XPoint(10.96, 8.58), angle = -90,
                    },
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("Mvd(M", fontL);
                                context.PrtText("Da", fontS);
                                context.PrtText(") =   500 kN・m", fontL);
                            }
                        }),
                        pos = new XPoint(11.26, 8.58), angle = -90,
                    },
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("Ms(M", fontL);
                                context.PrtText("Db", fontS);
                                context.PrtText(")  = 2,562 kN・m", fontL);
                            }
                        }),
                        pos = new XPoint(13.14, 8.58), angle = -90,
                    },
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("Mvd(M", fontL);
                                context.PrtText("Da", fontS);
                                context.PrtText(") =   542 kN・m", fontL);
                            }
                        }),
                        pos = new XPoint(13.44, 8.58), angle = -90,
                    },
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("(M", fontL);
                                context.PrtText("Da", fontS);
                                context.PrtText(")", fontL);
                            }
                        }),
                        pos = new XPoint(12.01, 12.00), angle = 0,
                    },
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("(M", fontL);
                                context.PrtText("Db", fontS);
                                context.PrtText(")", fontL);
                            }
                        }),
                        pos = new XPoint(13.42, 13.02), angle = 0,
                    },
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("Ms(M", fontL);
                                context.PrtText("Db", fontS);
                                context.PrtText(") ：合成前死荷重曲げモーメント", fontL);
                            }
                        }),
                        pos = new XPoint(11.15, 15.26), angle = 0,
                    },
                    new
                    {
                        draw = new Action<PdfDocument, XPoint, int>((mc, pos, angle) =>
                        {
                            mc.currentPos = pos;
                            using (var context = new TextJoiningContext(mc, angle))
                            {
                                context.PrtText("Mvd(M", fontL);
                                context.PrtText("Da", fontS);
                                context.PrtText(")：合成後死荷重曲げモーメント", fontL);
                            }
                        }),
                        pos = new XPoint(11.15, 15.66), angle = 0,
                    },
                })
                {
                    var p = matrix.Transform(d.pos);

                    d.draw(mc, p, d.angle);
                }

                // 小さめのフォントによる描画

                var fontM = new XFont("MS Mincho", 6.5, XFontStyle.Regular);

                foreach (var d in new[]
                {
                    new { txt = "断面長", pos = new XPoint(3.95, 17.06), angle = 0, },
                    new { txt = "配力鉄筋", pos = new XPoint(3.86, 17.37), angle = 0, },
                    new { txt = "上段 D19@120 / 下段 D19@120", pos = new XPoint(9.08, 17.37), angle = 0, },
    
                    new { txt = "対傾構間隔", pos = new XPoint(3.74, 19.44), angle = 0, },
                    new { txt = "支間長", pos = new XPoint(3.88, 19.78), angle = 0, },
                })
                {
                    var p = matrix.Transform(d.pos);

                    mc.printText(p.X, p.Y, d.txt, d.angle, fontM);
                }
            }
            finally
            {
                mc.currentPos = bkup;
            }
        }
    }
}
