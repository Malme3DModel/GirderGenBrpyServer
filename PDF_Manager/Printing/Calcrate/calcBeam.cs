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
    internal class calcBeam
    {
        internal static void printPDF(PdfDocument mc, GirderData.GirderData data)
        {
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

        }
    }
}
