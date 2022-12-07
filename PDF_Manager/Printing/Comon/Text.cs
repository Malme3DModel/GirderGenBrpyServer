using PdfSharpCore.Drawing;
using SixLabors.Fonts;
using System;

namespace Printing.Comon
{
    internal class Text
    {
        /// <summary>
        /// テキストを描画する
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <param name="align"></param>
        /// <param name="angle">テキストを描画する角度の指定(X軸の正方向から時計回りを正とする。単位は度)</param>
        static public void PrtText(PdfDocument mc, string str, XFont font = null, XStringFormat align = null, double angle = 0)
        {
            if (str == null)
                return;

            if (align == null)
                align = XStringFormats.BottomLeft; // 左下起点

            if (font == null)
                font = mc.font_mic; // 明朝

            mc.gfx.RotateAtTransform(angle, mc.currentPos);
            try
            {
                // 文字列描画
                mc.gfx.DrawString(str, font, XBrushes.Black, mc.currentPos, align);
            }
            finally
            {
                mc.gfx.RotateAtTransform(-angle, mc.currentPos);
            }
        }

        /// <summary>
        /// 下線付きテキストを描画する
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <param name="align"></param>
        /// <param name="underlinePenWidth">下線の太さ</param>
        static public void PrtUnderlinedText(PdfDocument mc, string str, XFont font = null, XStringFormat align = null,
            double underlinePenWidth = 0.1)
        {
            align ??= XStringFormats.BottomLeft;
            font ??= mc.font_mic;
            var size = mc.gfx.MeasureString(str, font, align);
            PrtText(mc, str, font, align);

            var bkup = mc.xpen.Width;
            try
            {
                Shape.DrawLines(mc, new[] { mc.currentPos, mc.currentPos + new XVector(size.Width, 0), }, underlinePenWidth);
            }
            finally
            {
                mc.xpen.Width = bkup;
            }
        }

        /// <summary>
        /// 水平な括線を持つ分数を描画する
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="vinculumPenWidth">括線の太さ</param>
        /// <param name="leftMargin">括線の左側に挿入する空白の幅</param>
        /// <param name="rightMargin">括線の右側に挿入する空白の幅</param>
        /// <param name="numeratorLeftMargin">括線の左端から分子の表示位置の左端までの最低幅</param>
        /// <param name="numeratorRightMargin">括線の右端から分子の表示位置の右端までの最低幅</param>
        /// <param name="denominatorLeftMargin">括線の左端から分母の表示位置の左端までの最低幅</param>
        /// <param name="denominatorRightMargin">括線の右端から分母の表示位置の右端までの最低幅</param>
        /// <param name="numeratorDistance">分子の下端と括線の間の隙間の幅</param>
        /// <param name="denominatorDistance">分母の上端と括線の間の隙間の幅</param>
        /// <returns>描画したテキストの幅</returns>
        static public double PrtFraction(PdfDocument mc, string numerator, string denominator,
            double vinculumPenWidth = 0.1,
            double leftMargin = 0, double rightMargin = 0,
            double numeratorLeftMargin = 6, double numeratorRightMargin = 6, double denominatorLeftMargin = 6, double denominatorRightMargin = 6,
            double numeratorDistance = 1, double denominatorDistance = 1)
        {
            var numeratorSize = mc.MeasureString(numerator);
            var denominatorSize = mc.MeasureString(denominator);

            var vinculumWidth = leftMargin +
                Math.Max(numeratorLeftMargin + numeratorSize.Width + numeratorRightMargin,
                    denominatorLeftMargin + denominatorSize.Width + denominatorRightMargin) + rightMargin;
            var vinclumStartP = mc.currentPos + new XVector(0, -mc.FontSize / 2);
            var vinclumEndP = mc.currentPos + new XVector(vinculumWidth, -mc.FontSize / 2);

            // 分子と分母の表示位置はそれぞれ中央寄せ固定
            var numeratorP = vinclumStartP + new XVector(numeratorLeftMargin + (vinculumWidth - numeratorLeftMargin - numeratorSize.Width - numeratorRightMargin) / 2, -numeratorDistance);
            var denominatorP = vinclumStartP + new XVector(denominatorLeftMargin + (vinculumWidth -denominatorLeftMargin - denominatorSize.Width - denominatorRightMargin) / 2, denominatorDistance + mc.FontSize);

            mc.gfx.DrawString(numerator, mc.font_mic, XBrushes.Black, numeratorP, XStringFormats.BottomLeft);
            var bkup = mc.xpen.Width;
            try
            {
                Shape.DrawLines(mc, new[] { vinclumStartP, vinclumEndP, }, _PenWidth: vinculumPenWidth);
            }
            finally
            {
                mc.xpen.Width = bkup;
            }
            mc.gfx.DrawString(denominator, mc.font_mic, XBrushes.Black, denominatorP, XStringFormats.BottomLeft);

            return leftMargin + vinculumWidth + rightMargin;
        }
    }

    internal class TextJoiningContext : IDisposable
    {
        /// <summary>
        /// テキストを描画するごとにmc.currentPos.Xが自動更新されるコンテキストを生成する。
        /// Dispose()呼び出しにより、コンテキスト生成前の状態が復元される
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="angle">テキストを描画する角度の指定(X軸の正方向から時計回りを正とする。単位は度)</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TextJoiningContext(PdfDocument mc, double angle = 0)
        {
            if (mc is null) throw new ArgumentNullException(nameof(mc));

            this.mc = mc;
            origPos = new XPoint(mc.currentPos.X, mc.currentPos.Y);
            this.angle = angle;

            mc.gfx.RotateAtTransform(angle, origPos);
        }

        /// <summary>
        /// テキストを描画する。
        /// 描画後にmc.currentPos.Xが更新される
        /// </summary>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <param name="align"></param>
        public void PrtText(string str, XFont font = null, XStringFormat align = null)
        {
            align ??= XStringFormats.BottomLeft;
            font ??= mc.font_mic;
            var size = mc.gfx.MeasureString(str, font, align);
            Text.PrtText(mc, str, font, align);

            mc.addCurrentX(size.Width);
        }

        /// <summary>
        /// 下線付きテキストを描画する。
        /// 描画後にmc.currentPos.Xが更新される
        /// </summary>
        /// <param name="str"></param>
        /// <param name="font"></param>
        /// <param name="align"></param>
        /// <param name="underlinePenWidth">下線の太さ</param>
        public void PrtUnderlinedText(string str, XFont font = null, XStringFormat align = null,
            double underlinePenWidth = 0.1)
        {
            align ??= XStringFormats.BottomLeft;
            font ??= mc.font_mic;
            var size = mc.gfx.MeasureString(str, font, align);
            Text.PrtUnderlinedText(mc, str, font, align);

            var bkup = mc.xpen.Width;
            try
            {
                Shape.DrawLines(mc, new[] { mc.currentPos, mc.currentPos + new XVector(size.Width, 0), }, underlinePenWidth);
            }
            finally
            {
                mc.xpen.Width = bkup;
            }

            mc.addCurrentX(size.Width);
        }

        /// <summary>
        /// 水平な括線を持つ分数を描画する。
        /// 描画後にmc.currentPos.Xが更新される
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="vinculumPenWidth">括線の太さ</param>
        /// <param name="leftMargin">括線の左側に挿入する空白の幅</param>
        /// <param name="rightMargin">括線の右側に挿入する空白の幅</param>
        /// <param name="numeratorLeftMargin">括線の左端から分子の表示位置の左端までの最低幅</param>
        /// <param name="numeratorRightMargin">括線の右端から分子の表示位置の右端までの最低幅</param>
        /// <param name="denominatorLeftMargin">括線の左端から分母の表示位置の左端までの最低幅</param>
        /// <param name="denominatorRightMargin">括線の右端から分母の表示位置の右端までの最低幅</param>
        /// <param name="numeratorDistance">分子の下端と括線の間の隙間の幅</param>
        /// <param name="denominatorDistance">分母の上端と括線の間の隙間の幅</param>
        /// <returns>描画したテキストの幅</returns>
        public double PrtFraction(string numerator, string denominator,
            double vinculumPenWidth = 0.1,
            double leftMargin = 0, double rightMargin = 0,
            double numeratorLeftMargin = 6, double numeratorRightMargin = 6, double denominatorLeftMargin = 6, double denominatorRightMargin = 6,
            double numeratorDistance = 1, double denominatorDistance = 1)
        {
            var width = Text.PrtFraction(mc, numerator, denominator,
                vinculumPenWidth,
                leftMargin, rightMargin,
                numeratorLeftMargin, numeratorRightMargin, denominatorLeftMargin, denominatorRightMargin,
                numeratorDistance, denominatorDistance);

            mc.addCurrentX(width);

            return width;
        }

        private readonly PdfDocument mc;
        private readonly XPoint origPos;
        private readonly double angle;

        /// <summary>
        /// 後始末
        /// </summary>
        public void Dispose()
        {
            mc.currentPos = origPos;

            mc.gfx.RotateAtTransform(-angle, origPos);
        }
    }
}

