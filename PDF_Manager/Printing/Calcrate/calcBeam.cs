using PdfSharpCore.Drawing;
using Printing.Comon;
using SixLabors.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing.Calcrate
{
    internal partial class calcBeam
    {
        internal static void printPDF(PdfDocument mc, GirderData.GirderData data)
        {
            #region Pg.24

            Text.PrtText(mc, "5.　主桁の設計");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(printManager.LineSpacing3);
            Text.PrtText(mc, "5.1　設計方針");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(printManager.LineSpacing3);
            Text.PrtText(mc, "（1） 主桁の抵抗断面に床版コンクリートを見込む設計とし,床版コンクリートと鋼桁のずれ");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(printManager.LineSpacing4);
            Text.PrtText(mc, "止めには,頭付きスタッドを用いることとする。");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(-printManager.LineSpacing4);
            Text.PrtText(mc, "（2） 鉛直方向の作用により主桁に生じる断面力については,上部構造を下図のような支間中央");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(printManager.LineSpacing4);
            Text.PrtText(mc, "に1本の荷重分配横桁を有する格子構造にモデル化し,変形法により算出する。主桁の耐荷");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "性能の照査方針については5.6.1に示す。");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(-printManager.LineSpacing4);
            Text.PrtText(mc, "（3） 風荷重及び地震の影響などの横荷重については,全て床版で抵抗させるものとして設計");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(printManager.LineSpacing4);
            Text.PrtText(mc, "する。横荷重に対するモデル化及び照査方針については5.7.1に示す。");
            mc.addCurrentX(-(printManager.LineSpacing3 * 2.0 + printManager.LineSpacing4));

            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(printManager.LineSpacing3 * 20.0);
            Text.PrtText(mc, "主桁");
            mc.addCurrentX(-printManager.LineSpacing3 * 20.0);
            XPoint _pta1 = new XPoint(mc.currentPos.X + 203.5, mc.currentPos.Y - 5.5);
            XPoint _pta2 = new XPoint(mc.currentPos.X + 181.5, mc.currentPos.Y + printManager.LineSpacing2 * 2.0);
            Shape.DrawLine(mc, _pta1, _pta2);
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);



            double startpoint = 115.5;
            double length = 302.5;
            double endpoint = length + startpoint;
            int amount = data.amount_V;
            int amount2 = data.amount_H;
            int amount3 = data.amount_C;
            string TFc = data.crossbeam;
            double interval = 30;
            double interval2 = length / (amount2 - 1);
            double dy = 0;
            double dx = interval2;
            double trisize = 11;
            double trix = trisize / 2.0;
            double triliney = 3;
            double dushline = 8.0;
            double dushspace = 3.0;
            double dushamount = Math.Floor(interval * (amount - 1) / (dushline + dushspace));
            double measurement = 73;
            double measurement2 = 55;
            double measurement3 = 45;
            double dmea = 5;
            double arrowH = 3.0;
            double arrowW = 5.0;

            /// 主桁の線を描写
            for (int i = 0; i < amount; i++)
            {
                XPoint _pt1 = new XPoint(mc.currentPos.X + startpoint, mc.currentPos.Y + dy);
                XPoint _pt2 = new XPoint(mc.currentPos.X + endpoint, mc.currentPos.Y + dy);
                Shape.DrawLine(mc, _pt1, _pt2);
                dy += interval;
            }

            /// 対傾構を描写
            for (int i = 0; i < amount2 - 2; i++)
            {
                dy = 0.0;
                for (int j = 0; j < dushamount; j++)
                {
                    XPoint _pt1 = new XPoint(mc.currentPos.X + startpoint + dx, mc.currentPos.Y + dy);
                    XPoint _pt2 = new XPoint(mc.currentPos.X + startpoint + dx, mc.currentPos.Y + dy + dushline);
                    Shape.DrawLine(mc, _pt1, _pt2);
                    dy += dushline + dushspace;
                }
                XPoint _pt3 = new XPoint(mc.currentPos.X + startpoint + dx, mc.currentPos.Y + dy);
                XPoint _pt4 = new XPoint(mc.currentPos.X + startpoint + dx, mc.currentPos.Y + interval * (amount - 1));
                Shape.DrawLine(mc, _pt3, _pt4);
                dx += interval2;
            }

            /// 左側の支承を描写
            dy = 0.0;
            for (int i = 0; i < amount; i++)
            {
                XPoint _pt1 = new XPoint(mc.currentPos.X + startpoint, mc.currentPos.Y + dy);
                XPoint _pt2 = new XPoint(mc.currentPos.X + startpoint - trix, mc.currentPos.Y + dy + trisize);
                XPoint _pt3 = new XPoint(mc.currentPos.X + startpoint + trix, mc.currentPos.Y + dy + trisize);
                Shape.DrawLine(mc, _pt1, _pt2);
                Shape.DrawLine(mc, _pt1, _pt3);
                Shape.DrawLine(mc, _pt2, _pt3);
                dy += interval;
            }

            /// 右側の支承を描写
            dy = 0.0;
            for (int i = 0; i < amount; i++)
            {
                XPoint _pt1 = new XPoint(mc.currentPos.X + endpoint, mc.currentPos.Y + dy);
                XPoint _pt2 = new XPoint(mc.currentPos.X + endpoint - trix, mc.currentPos.Y + dy + trisize);
                XPoint _pt3 = new XPoint(mc.currentPos.X + endpoint + trix, mc.currentPos.Y + dy + trisize);
                XPoint _pt4 = new XPoint(mc.currentPos.X + endpoint - trix, mc.currentPos.Y + dy + trisize + triliney);
                XPoint _pt5 = new XPoint(mc.currentPos.X + endpoint + trix, mc.currentPos.Y + dy + trisize + triliney);
                Shape.DrawLine(mc, _pt1, _pt2);
                Shape.DrawLine(mc, _pt1, _pt3);
                Shape.DrawLine(mc, _pt2, _pt3);
                Shape.DrawLine(mc, _pt4, _pt5);
                dy += interval;
            }

            /// 荷重分配横桁を描写
            double m = (amount2 - 1) / (amount3 + 1);
            Math.Floor(m);
            for (int i = 0; i < amount3; i++)
            {
                XPoint _ptm1 = new XPoint(mc.currentPos.X + startpoint + interval2 * m, mc.currentPos.Y);
                XPoint _ptm2 = new XPoint(mc.currentPos.X + startpoint + interval2 * m, mc.currentPos.Y + (amount - 1) * interval);
                Shape.DrawLine(mc, _ptm1, _ptm2);
                m += m;
            }

            /// 端横桁を描写
            XPoint _pts1 = new XPoint(mc.currentPos.X + startpoint, mc.currentPos.Y);
            XPoint _pts2 = new XPoint(mc.currentPos.X + startpoint, mc.currentPos.Y + (amount - 1) * interval);
            XPoint _pte1 = new XPoint(mc.currentPos.X + endpoint, mc.currentPos.Y);
            XPoint _pte2 = new XPoint(mc.currentPos.X + endpoint, mc.currentPos.Y + (amount - 1) * interval);
            Shape.DrawLine(mc, _pts1, _pts2);
            Shape.DrawLine(mc, _pte1, _pte2);

            /// 右側寸法線を作成
            dx = 5.5;
            XPoint _ptL1 = new XPoint(mc.currentPos.X + endpoint + dx, mc.currentPos.Y);
            XPoint _ptL2 = new XPoint(mc.currentPos.X + endpoint + measurement3 + dx, mc.currentPos.Y);
            XPoint _ptL3 = new XPoint(mc.currentPos.X + endpoint + dx, mc.currentPos.Y + (amount - 1) * interval);
            XPoint _ptL4 = new XPoint(mc.currentPos.X + endpoint + measurement3 + dx, mc.currentPos.Y + (amount - 1) * interval);
            XPoint _ptL5 = new XPoint(mc.currentPos.X + endpoint + measurement3 + dx - dmea, mc.currentPos.Y);
            XPoint _ptL6 = new XPoint(mc.currentPos.X + endpoint + measurement3 + dx - dmea, mc.currentPos.Y + (amount - 1) * interval);
            XPoint _ptL7 = new XPoint(mc.currentPos.X + endpoint + measurement3 + dx - dmea - arrowH, mc.currentPos.Y + arrowW);
            XPoint _ptL8 = new XPoint(mc.currentPos.X + endpoint + measurement3 + dx - dmea + arrowH, mc.currentPos.Y + arrowW);
            XPoint _ptL9 = new XPoint(mc.currentPos.X + endpoint + measurement3 + dx - dmea - arrowH, mc.currentPos.Y - arrowW + (amount - 1) * interval);
            XPoint _ptL10 = new XPoint(mc.currentPos.X + endpoint + measurement3 + dx - dmea + arrowH, mc.currentPos.Y - arrowW + (amount - 1) * interval);
            Shape.DrawLine(mc, _ptL1, _ptL2);
            Shape.DrawLine(mc, _ptL3, _ptL4);
            Shape.DrawLine(mc, _ptL5, _ptL6);
            Shape.DrawLine(mc, _ptL5, _ptL7);
            Shape.DrawLine(mc, _ptL5, _ptL8);
            Shape.DrawLine(mc, _ptL6, _ptL9);
            Shape.DrawLine(mc, _ptL6, _ptL10);

            mc.addCurrentX(printManager.LineSpacing3 * 8.0);
            Text.PrtText(mc, "G1");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "G2");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "G3");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "G4");
            mc.addCurrentX(-printManager.LineSpacing3 * 8.0);

            /// 下側寸法線を作成
            XPoint _ptM1 = new XPoint(mc.currentPos.X + startpoint, mc.currentPos.Y);
            XPoint _ptM2 = new XPoint(mc.currentPos.X + startpoint, mc.currentPos.Y + measurement);
            XPoint _ptM3 = new XPoint(mc.currentPos.X + startpoint + (length / 2.0), mc.currentPos.Y);
            XPoint _ptM4 = new XPoint(mc.currentPos.X + startpoint + (length / 2.0), mc.currentPos.Y + measurement2);
            XPoint _ptM5 = new XPoint(mc.currentPos.X + endpoint, mc.currentPos.Y);
            XPoint _ptM6 = new XPoint(mc.currentPos.X + endpoint, mc.currentPos.Y + measurement);
            XPoint _ptM7 = new XPoint(mc.currentPos.X + startpoint, mc.currentPos.Y + measurement - dmea);
            XPoint _ptM8 = new XPoint(mc.currentPos.X + endpoint, mc.currentPos.Y + measurement - dmea);
            XPoint _ptM9 = new XPoint(mc.currentPos.X + startpoint, mc.currentPos.Y + measurement2 - dmea);
            XPoint _ptM10 = new XPoint(mc.currentPos.X + endpoint, mc.currentPos.Y + measurement2 - dmea);
            XPoint _ptM11 = new XPoint(mc.currentPos.X + startpoint, mc.currentPos.Y + measurement);
            XPoint _ptM12 = new XPoint(mc.currentPos.X + startpoint + arrowW, mc.currentPos.Y + measurement - dmea - arrowH);
            XPoint _ptM13 = new XPoint(mc.currentPos.X + startpoint + arrowW, mc.currentPos.Y + measurement - dmea + arrowH);
            XPoint _ptM14 = new XPoint(mc.currentPos.X + endpoint - arrowW, mc.currentPos.Y + measurement - dmea - arrowH);
            XPoint _ptM15 = new XPoint(mc.currentPos.X + endpoint - arrowW, mc.currentPos.Y + measurement - dmea + arrowH);
            XPoint _ptM16 = new XPoint(mc.currentPos.X + startpoint + arrowW, mc.currentPos.Y + measurement2 - dmea - arrowH);
            XPoint _ptM17 = new XPoint(mc.currentPos.X + startpoint + arrowW, mc.currentPos.Y + measurement2 - dmea + arrowH);
            XPoint _ptM18 = new XPoint(mc.currentPos.X + startpoint + (length / 2.0), mc.currentPos.Y + measurement2 - dmea);
            XPoint _ptM19 = new XPoint(mc.currentPos.X + startpoint + (length / 2.0) - arrowW, mc.currentPos.Y + measurement2 - dmea - arrowH);
            XPoint _ptM20 = new XPoint(mc.currentPos.X + startpoint + (length / 2.0) - arrowW, mc.currentPos.Y + measurement2 - dmea + arrowH);
            XPoint _ptM21 = new XPoint(mc.currentPos.X + startpoint + (length / 2.0) + arrowW, mc.currentPos.Y + measurement2 - dmea - arrowH);
            XPoint _ptM22 = new XPoint(mc.currentPos.X + startpoint + (length / 2.0) + arrowW, mc.currentPos.Y + measurement2 - dmea + arrowH);
            XPoint _ptM23 = new XPoint(mc.currentPos.X + endpoint - arrowW, mc.currentPos.Y + measurement2 - dmea - arrowH);
            XPoint _ptM24 = new XPoint(mc.currentPos.X + endpoint - arrowW, mc.currentPos.Y + measurement2 - dmea + arrowH);
            Shape.DrawLine(mc, _ptM1, _ptM2);
            Shape.DrawLine(mc, _ptM3, _ptM4);
            Shape.DrawLine(mc, _ptM5, _ptM6);
            Shape.DrawLine(mc, _ptM7, _ptM8);
            Shape.DrawLine(mc, _ptM9, _ptM10);
            Shape.DrawLine(mc, _ptM7, _ptM12);
            Shape.DrawLine(mc, _ptM7, _ptM13);
            Shape.DrawLine(mc, _ptM8, _ptM14);
            Shape.DrawLine(mc, _ptM8, _ptM15);
            Shape.DrawLine(mc, _ptM9, _ptM16);
            Shape.DrawLine(mc, _ptM9, _ptM17);
            Shape.DrawLine(mc, _ptM18, _ptM19);
            Shape.DrawLine(mc, _ptM18, _ptM20);
            Shape.DrawLine(mc, _ptM18, _ptM21);
            Shape.DrawLine(mc, _ptM18, _ptM22);
            Shape.DrawLine(mc, _ptM10, _ptM23);
            Shape.DrawLine(mc, _ptM10, _ptM24);

            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(printManager.LineSpacing3 * 30.5);
            Text.PrtText(mc, "荷重分配横げた");
            mc.addCurrentX(-printManager.LineSpacing3 * 30.5);
            XPoint _pta3 = new XPoint(mc.currentPos.X + startpoint + (length / 2.0) + 65, mc.currentPos.Y - 5.5);
            double s = (amount2 - 1) / (amount3 + 1);
            Math.Floor(s);
            for (int i = 0; i < amount3; i++)
            {
                XPoint _pta4 = new XPoint(mc.currentPos.X + startpoint + interval2 * s, mc.currentPos.Y - printManager.LineSpacing2 * 2.0);
                Shape.DrawLine(mc, _pta3, _pta4);
                s += s;
            }
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(printManager.LineSpacing3 * 14.0);
            Text.PrtText(mc, "3x5500=16500");
            mc.addCurrentX(printManager.LineSpacing3 * 14.0);
            Text.PrtText(mc, "3x5500=16500");
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentX(-printManager.LineSpacing3 * 5.0);
            Text.PrtText(mc, "33000");

            var _x = printManager.LineSpacing3 * 46.0;
            var _y = printManager.LineSpacing2 * (16.0 + (amount - 3));
            mc.printText(_x, _y, "3x2550=7650");


            // -----------------------------------------------------------------------

            mc.addCurrentY(printManager.LineSpacing1 * 7);

            mc.setCurrentX(printManager.SubsectionIndent);
            Text.PrtText(mc, "5.2　荷重");
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.SubsubsectionIndent);
            Text.PrtText(mc, "5.2.1　横断面形状");
            mc.addCurrentY(printManager.LineSpacing2);

            DrawFig0502(mc);

            #endregion

            mc.NewPage();

            #region Pg.25

            mc.setCurrentX(printManager.SubsubsectionIndent);
            Text.PrtText(mc, "5.2.2　合成前死荷重");
            mc.setCurrentX(mc.currentPageSize.Width - mc.Margine.Left - mc.Margine.Right);
            Text.PrtText(mc, "［道示Ⅰ］表-8-1.1", align: XStringFormats.BottomRight);
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "床　版                     0.220 × 24.5                    ＝　5.39 kN/m\u00B2");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "ハンチ（G1，4）            1/2×(0.310+1.360)×0.060×24.5  ＝　1.23 kN/m");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "　〃　（G2，3）            1/2×(0.310+0.976)×0.111×24.5  ＝　1.75 kN/m");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "鋼　重（主桁1本当たり）                                     ＝　4.41 kN/m");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "型　枠                                                      ＝　1.00 kN/m\u00B2");
            mc.addCurrentY(printManager.LineSpacing2);

            DrawFig0503(mc);
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.SubsubsectionIndent);
            Text.PrtText(mc, "5.2.3　合成後死荷重");
            mc.setCurrentX(mc.currentPageSize.Width - mc.Margine.Left - mc.Margine.Right);
            Text.PrtText(mc, "［道示Ⅰ］表-8-1.1", align: XStringFormats.BottomRight);
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "舗　装                 0.075 × 22.5           ＝　1.69 kN/m\u00B2");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "地　覆（右，左とも）   0.325 × 0.600 × 24.5  ＝　4.78 kN/m");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "防護柵（右，左とも）                           ＝　0.50 kN/m");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "添架物                                         ＝　0.60 kN/m");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "型　枠（撤去）                                 ＝ -1.00 kN/m\u00B2");
            mc.addCurrentY(printManager.LineSpacing2);

            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.SubsubsectionIndent);
            Text.PrtText(mc, "5.2.4　活 荷 重");
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "等分布荷重P\u2081    曲げモーメントに対して    10 kN/m\u00B2");
            mc.setCurrentX(mc.currentPageSize.Width - mc.Margine.Left - mc.Margine.Right);
            Text.PrtText(mc, "［道示Ⅰ］表-8-2.2", align: XStringFormats.BottomRight);
            mc.addCurrentY(printManager.LineSpacing2);
            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "                せん断力に対して          12 kN/m\u00B2");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "等分布荷重P\u2082      3.5 kN/m\u00B2");
            mc.addCurrentY(printManager.LineSpacing3 * 2);
            using (var context = new TextJoiningContext(mc))
            {
                context.PrtText("衝撃係数        i ＝ ");
                context.PrtFraction("20", "50 + L");
                context.PrtText(" ＝ ");
                context.PrtFraction("20", "50 + 33.0");
                context.PrtText(" - 0.241");
            }
            mc.setCurrentX(mc.currentPageSize.Width - mc.Margine.Left - mc.Margine.Right);
            Text.PrtText(mc, "［道示Ⅰ］表-8-3.2", align: XStringFormats.BottomRight);
            mc.addCurrentY(printManager.LineSpacing3 * 2);
            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "                          L ＝ 支間長    (m)");

            #endregion

            mc.NewPage();

            #region Pg.26

            mc.setCurrentX(printManager.SubsectionIndent);
            Text.PrtText(mc, "5.3　荷重強度");
            mc.addCurrentY(printManager.LineSpacing2);

            DrawFig0504(mc);

            #endregion

            mc.NewPage();

            #region Pg.27

            mc.setCurrentX(printManager.SubsectionIndent);
            Text.PrtText(mc, "5.4　断面力");
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.SubsubsectionIndent);
            Text.PrtText(mc, "5.4.1　断面力及び断面構成図");
            mc.addCurrentY(printManager.LineSpacing2);

            DrawFig0505(mc);
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent + printManager.LineSpacing3 * 2);
            DrawTab0504a(mc);

            #endregion

            mc.NewPage();

            #region Pg.28

            DrawFig0506(mc);
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent + printManager.LineSpacing3 * 2);
            DrawTab0504b(mc);

            #endregion

            mc.NewPage();

            #region Pg.29

            mc.setCurrentX(printManager.SubsubsectionIndent);
            Text.PrtText(mc, "5.4.2　支点反力");
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent);
            mc.Nup(mc =>
            {
                var topPos = mc.currentPos;
                Text.PrtUnderlinedText(mc, "G1（G4）桁");
                mc.addCurrentY(printManager.LineSpacing2);
                using (var context = new TextJoiningContext(mc))
                {
                    var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                    context.PrtText("合 成 前    R");
                    context.PrtText("s", sf);
                    context.PrtText("(D");
                    context.PrtText("b", sf);
                    context.PrtText(")   ＝ S");
                    context.PrtText("s", sf);
                    context.PrtText(" ＝   361 kN");
                }
                mc.addCurrentY(printManager.LineSpacing2);
                using (var context = new TextJoiningContext(mc))
                {
                    var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                    context.PrtText("合 成 後    R");
                    context.PrtText("v", sf);
                    context.PrtText("       ＝ S");
                    context.PrtText("v", sf);
                    context.PrtText(" ＝   476 kN");
                }
                mc.addCurrentY(printManager.LineSpacing2);
                using (var context = new TextJoiningContext(mc))
                {
                    var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                    context.PrtText("                ( S");
                    context.PrtText("vd", sf);
                    context.PrtText("(Da)  ＝    90 kN )");
                }
                mc.addCurrentY(printManager.LineSpacing2);
                using (var context = new TextJoiningContext(mc))
                {
                    var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                    context.PrtUnderlinedText("                ( S");
                    context.PrtUnderlinedText("vl", sf);
                    context.PrtUnderlinedText("      ＝   386 kN )");
                }
                mc.addCurrentY(printManager.LineSpacing2);
                Text.PrtText(mc, " 合 計               R(S) ＝   837 kN");
                //mc.addCurrentY(printManager.LineSpacing2);
                return new XSize(0, mc.currentPos.Y - topPos.Y); // Widthが参照されることはないのでダミーとして0を設定
            }, mc =>
            {
                var topPos = mc.currentPos;
                Text.PrtUnderlinedText(mc, "G2（G3）桁");
                mc.addCurrentY(printManager.LineSpacing2);
                using (var context = new TextJoiningContext(mc))
                {
                    var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                    context.PrtText("合 成 前    R");
                    context.PrtText("s", sf);
                    context.PrtText("(D");
                    context.PrtText("b", sf);
                    context.PrtText(")   ＝ S");
                    context.PrtText("s", sf);
                    context.PrtText(" ＝   345 kN");
                }
                mc.addCurrentY(printManager.LineSpacing2);
                using (var context = new TextJoiningContext(mc))
                {
                    var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                    context.PrtText("合 成 後    R");
                    context.PrtText("v", sf);
                    context.PrtText("       ＝ S");
                    context.PrtText("v", sf);
                    context.PrtText(" ＝   500 kN");
                }
                mc.addCurrentY(printManager.LineSpacing2);
                using (var context = new TextJoiningContext(mc))
                {
                    var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                    context.PrtText("                ( S");
                    context.PrtText("vd", sf);
                    context.PrtText("(Da)  ＝    41 kN )");
                }
                mc.addCurrentY(printManager.LineSpacing2);
                using (var context = new TextJoiningContext(mc))
                {
                    var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                    context.PrtUnderlinedText("                ( S");
                    context.PrtUnderlinedText("vl", sf);
                    context.PrtUnderlinedText("      ＝   459 kN )");
                }
                mc.addCurrentY(printManager.LineSpacing2);
                Text.PrtText(mc, " 合 計               R(S) ＝   845 kN");
                mc.addCurrentY(printManager.LineSpacing2);
                Text.PrtText(mc, "注）特性値による支点反力を示す。");
                //mc.addCurrentY(printManager.LineSpacing2);
                return new XSize(0, mc.currentPos.Y - topPos.Y); // Widthが参照されることはないのでダミーとして0を設定
            });
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.SubsectionIndent);
            Text.PrtText(mc, "5.5　床版の有効幅");
            mc.setCurrentX(mc.currentPageSize.Width - mc.Margine.Left - mc.Margine.Right);
            Text.PrtText(mc, "［道示Ⅱ］14-3.4", align: XStringFormats.BottomRight);
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "　床版の有効幅は，鋼桁と床版との合成断面に参入できる床版の幅であり，下図のλとaとを");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "合わせた長さである。aを求める場合の水平に対するハンチの傾斜は45°とする。");
            mc.addCurrentY(printManager.LineSpacing2);

            DrawFig0507(mc);
            mc.addCurrentY(printManager.LineSpacing2);
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "上フランジ幅を仮定しハンチ部の長さaを求める。");
            mc.addCurrentY(printManager.LineSpacing2);
            using (var context = new TextJoiningContext(mc))
            {
                var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                context.PrtText("  a\u2081 ＝ B");
                context.PrtText("f", sf);
                context.PrtText(" ＋ 2 × h\u2081 ＝ 310 ＋ 2 × 60   ＝   430 mm");
            }
            mc.addCurrentY(printManager.LineSpacing2);
            using (var context = new TextJoiningContext(mc))
            {
                var sf = new XFont(mc.font_mic.Name, mc.font_mic.Height / 1.5, mc.font_mic.Style);
                context.PrtText("  a\u2082 ＝ B");
                context.PrtText("f", sf);
                context.PrtText(" ＋ 2 × h\u2082 ＝ 310 ＋ 2 × 111  ＝   532 mm");
            }
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "したがって，bは以下のように求められる。");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "  b\u2081 ＝  1,025   -   430  ／  2 ＝   810 mm");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "  b\u2082 ＝  (  2,550   -   430  ／  2 -  532  ／ 2 )   ／  2 ＝   1,035 mm");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "  b\u2083 ＝  (  2,550   -   532  )  ／  2 ＝   1,009 mm");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "片側有効幅λは次式より求める。");
            mc.addCurrentY(printManager.LineSpacing3 * 2);
            using (var context = new TextJoiningContext(mc))
            {
                context.PrtText("  λ = b                    ( ");
                context.PrtFraction("b", "ℓ");
                context.PrtText(" ≦ 0.05 )");
            }
            mc.setCurrentX(mc.currentPageSize.Width - mc.Margine.Left - mc.Margine.Right);
            Text.PrtText(mc, "［道示Ⅱ］式(13.3.1)", align: XStringFormats.BottomRight);
            mc.addCurrentY(printManager.LineSpacing2 + printManager.LineSpacing3);
            mc.setCurrentX(printManager.ParagraphIndent);
            using (var context = new TextJoiningContext(mc))
            {
                context.PrtText("     = { 1.1 - 2 ");
                context.PrtFraction("b", "ℓ");
                context.PrtText(" } b    ( 0.05 ＜ ");
                context.PrtFraction("b", "ℓ");
                context.PrtText(" ＜ 0.30 )");
            }
            mc.addCurrentY(printManager.LineSpacing2 + printManager.LineSpacing3);
            using (var context = new TextJoiningContext(mc))
            {
                context.PrtText("     = 0.15ℓ                ( 0.30 ≦ ");
                context.PrtFraction("b", "ℓ");
                context.PrtText(" )");
            }
            mc.addCurrentY(printManager.LineSpacing3 * 2);
            Text.PrtText(mc, "  ここで");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "    ℓ：等価支間長       本橋は単純桁のため支間に等しく");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "                        ℓ  ＝  33,000  mmとする。");
            mc.addCurrentY(printManager.LineSpacing2);

            #endregion

            mc.NewPage();

            #region Pg.30

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "G1（G4）桁の有効幅 B\u2081（B\u2084）");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "  b\u2081/ℓ  ＝   810   ／  33,000   ＝   0.025   ≦  0.05");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "    λ\u2081  ＝  b\u2081  ＝   810 mm");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "  b\u2082/ℓ  ＝ 1,035   ／  33,000   ＝   0.031   ≦  0.05");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "    λ\u2082  ＝  b\u2082  ＝ 1,035 mm");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "  したがって");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "    B\u2081（ B\u2084 ）＝ λ\u2081 ＋ a\u2081 ＋ λ\u2082");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "              ＝ 810 ＋ 430 ＋ 1,035");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "              ＝ 2,275 mm");
            mc.addCurrentY(printManager.LineSpacing2);

            Text.PrtText(mc, "G2（G3）桁の有効幅 B\u2082（B\u2083）");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "  b\u2083/ℓ  ＝ 1,009   ／  33,000   ＝   0.031   ≦  0.05");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "    λ\u2083  ＝  b\u2083  ＝ 1,009 mm");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "  したがって");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "    B\u2082（ B\u2083 ）＝ λ\u2082 ＋ a\u2082 ＋ λ\u2083");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "              ＝ 1,035 ＋ 532 ＋ 1,009");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "              ＝ 2,576 mm");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "床版は全幅が有効である。");

            #endregion

            mc.NewPage();

            #region Pg.31

            mc.setCurrentX(printManager.SubsectionIndent);
            Text.PrtText(mc, "5.6　耐荷性能の照査");
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "ここでは，鉛直方向の作用に対する主桁の耐荷性能について照査する。");
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.SubsubsectionIndent);
            Text.PrtText(mc, "5.6.1　設計方針");
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "(1)　主桁は，コンクリート系床版を有する鋼桁として設計し、構成する部材に作用す");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "   る応力度が限界状態1と限界状態3の制限値を超えないことを照査する。");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "(2)　床版コンクリートを主桁の抵抗断面に見込んだ設計とするが，床版コンクリート");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "   に引張応力が作用し，［道示Ⅱ］表-14.6.2に示される制限値を超える場合は，抵抗");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "   断面に床版コンクリートを考慮しない。このとき，配力鉄筋の鉄筋量は［道示Ⅱ］");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "   14.3.3(3) 1)および2)を満たすように決定する。");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "(3)　上下フランジの最小板厚は［道示Ⅱ］5.2.1より8mm以上とする。");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "(4)　腹板の最小板厚は［道示Ⅱ］5.2.1より8mmとする。また，水平補剛材は1段とし");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "   腹板厚は［道示Ⅱ］表13.4.1に示される値以上とする。");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "(5)　相反応力についてはここで示した例では影響が小さいため照査結果を省略する。");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "(6)　施工時の耐荷性能を照査するため，以下の荷重組合せと部分係数を設定する。");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "             1.05 D + 1.05 ER                 ［道示Ⅰ］3.3解説");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "   ここで死荷重は床版コンクリートが硬化する前の鋼桁と床版の荷重であり，施工時");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "   荷重は型枠の荷重である。これらを合わせて合成前死荷重Dbと表記する。");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "(7)　床版コンクリートが硬化した後の死荷重(舗装，地覆など)と型枠撤去時の荷重は");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "   合成後死荷重Daと表記する。合成前死荷重Dbと合成後死荷重Daを合計したものが");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "   完成系の死荷重となる。");

            #endregion

            mc.NewPage();

            #region Pg.32

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "構造，部材の照査に関する［道示Ⅱ］の記載を以下の表に示す。");
            mc.addCurrentY(printManager.LineSpacing2);

            DrawTab0506(mc);
            mc.addCurrentY(printManager.LineSpacing2);

            mc.setCurrentX(printManager.ParagraphIndent);
            Text.PrtText(mc, "  以降では，G1(G4)桁のSec-1(支間中央)について断面決定までの計算を示す。その");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "他の断面については計算結果のみを示す。");

            #endregion
        }
    }
}
