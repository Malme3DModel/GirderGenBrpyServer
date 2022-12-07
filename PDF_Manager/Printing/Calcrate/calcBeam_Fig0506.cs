using PdfSharpCore.Drawing;
using Printing.Comon;
using System;

namespace Printing.Calcrate
{
    internal partial class calcBeam
    {
        private static void DrawFig0506(PdfDocument mc)
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
                DrawFig0506a(mc, matrix, center);

                // 寸法線の描画
                DrawFig0506b(mc, matrix, center);

                // テキストの描画
                DrawFig0506c(mc, matrix);
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

        private static void DrawFig0506a(PdfDocument mc, XMatrix matrix, XPoint center)
        {
            // 図5と共通
            DrawFig0505a(mc, matrix, center);
        }

        private static void DrawFig0506b(PdfDocument mc, XMatrix matrix, XPoint center)
        {
            // 図5と共通
            DrawFig0505b(mc, matrix, center);
        }

        private static void DrawFig0506c(PdfDocument mc, XMatrix matrix)
        {
            var bkup = mc.currentPos;
            try
            {
                var fontL = new XFont("MS Mincho", 8.0, XFontStyle.Regular);

                foreach (var d in new[]
                {
                    new { txt = "G2 (G3)", pos = new XPoint(10.28, 4.91), angle = 0, },
    
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
                    new { txt = "S(R) = 845 kN", pos = new XPoint(5.35, 14.78), angle = -90, },
    
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
                    new { txt = "S(R) = 845 kN", pos = new XPoint(16.13, 8.43), angle = -90, },
    
                    new { txt = "Mv       = 2,987 kN・m", pos = new XPoint(8.43, 8.58), angle = -90, },
                    //new { txt = "Ms(MDb)  = 2,427 kN・m", pos = new XPoint(8.80, 8.58), angle = -90, },
                    //new { txt = "Mvd(MDa) =   381 kN・m", pos = new XPoint(9.10, 8.58), angle = -90, },
    
                    new { txt = "Mv       = 3,360 kN・m", pos = new XPoint(10.58, 8.58), angle = -90, },
                    //new { txt = "Ms(MDb)  = 2,817 kN・m", pos = new XPoint(10.96, 8.58), angle = -90, },
                    //new { txt = "Mvd(MDa) =   577 kN・m", pos = new XPoint(11.26, 8.58), angle = -90, },
    
                    new { txt = "Mv       = 2,987 kN・m", pos = new XPoint(12.75, 8.58), angle = -90, },
                    //new { txt = "Ms(MDb)  = 2,427 kN・m", pos = new XPoint(13.14, 8.58), angle = -90, },
                    //new { txt = "Mvd(MDa) =   381 kN・m", pos = new XPoint(13.44, 8.58), angle = -90, },
    
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
                                context.PrtText(")  = 2,427 kN・m", fontL);
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
                                context.PrtText(") =   381 kN・m", fontL);
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
                                context.PrtText(")  = 2,817 kN・m", fontL);
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
                                context.PrtText(") =   577 kN・m", fontL);
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
                                context.PrtText(")  = 2,427 kN・m", fontL);
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
                                context.PrtText(") =   381 kN・m", fontL);
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
