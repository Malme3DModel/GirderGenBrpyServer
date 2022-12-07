using PdfSharpCore.Drawing;
using System;

namespace Printing.Comon
{
    internal static class PdfDocumentExtensions
    {
        /// <summary>
        /// mc.currentPos.Xから用紙右端までの幅をconponentsで均等分割して多段組みを行う。
        /// 個々の構成要素を生成するメソッドは、それぞれが生成する領域のサイズ(少なくとも有意な高さが設定されていることが必要)を返却すること。
        /// 処理後のmc.currentPosには、多段組みされた領域の高さが反映される。
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="components"></param>
        /// <returns>描画した範囲のサイズ</returns>
        /// <exception cref="ArgumentException"></exception>
        public static XSize Nup(this PdfDocument mc, params Func<PdfDocument, XSize>[] components)
        {
            if (components is null || components.Length < 1)
                throw new ArgumentException($"{nameof(components)}.Length must be greater than 0.");

            var totalTopLeft = mc.currentPos;
            var totalWidth = mc.currentPage.Width - mc.Margine.Right - totalTopLeft.X;
            var eachWidth = totalWidth / components.Length;

            var totalHeight = 0.0;
            for (var i = 0; i < components.Length; ++i)
            {
                var component = components[i];

                mc.currentPos = totalTopLeft + new XVector(eachWidth, 0) * i;

                var size = component(mc);

                totalHeight = Math.Max(totalHeight, size.Height);
            }

            mc.currentPos = totalTopLeft + new XVector(0, totalHeight);

            return new XSize(totalWidth, totalHeight);
        }
    }
}
