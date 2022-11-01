using PdfSharpCore.Drawing;

namespace Printing.Comon
{
    internal class Text
    {
        static public void PrtText(PdfDocument mc, string str, XFont font = null, XStringFormat align = null)
        {
            if (str == null)
                return;

            if (align == null)
                align = XStringFormats.BottomLeft; // 左下起点

            if (font == null)
                font = mc.font_mic; // 明朝

            // 文字列描画
            mc.gfx.DrawString(str, font, XBrushes.Black, mc.currentPos, align);

        }

    }
}

