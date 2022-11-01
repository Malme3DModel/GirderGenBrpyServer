using PdfSharpCore.Drawing;
using Printing.Comon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing.Calcrate
{
    internal class calcBeam
    {
        internal static void printPDF(PdfDocument mc)
        {
            Text.PrtText(mc, "てすと");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "てすとてすとてすとてすと");

            mc.NewPage();
            Text.PrtText(mc, "２ページ目てすとてすとてすとてすと");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "２ページ目てすとてすとてすとてすと");
            mc.addCurrentY(printManager.LineSpacing2);
            Text.PrtText(mc, "２ページ目てすとてすとてすとてすと");
            mc.addCurrentY(printManager.LineSpacing2);


            XPoint _pt1 = new XPoint(mc.currentPos.X, mc.currentPos.Y);
            XPoint _pt2 = new XPoint(mc.currentPos.X + 12, mc.currentPos.Y);
            Shape.DrawLine(mc, _pt1, _pt2);

        }
    }
}
