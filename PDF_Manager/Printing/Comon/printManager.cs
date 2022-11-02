using PdfSharpCore.Drawing;

namespace Printing.Comon
{
    class printManager
    {
        /// <summary>
        /// デフォルトのフォントサイズ 
        /// pt ポイント　（1pt = 1/72 インチ)
        /// </summary>
        public static double FontSize = 11;

        /// <summary>
        /// デフォルトフォントの高さ 
        /// pt ポイント
        /// </summary>
        public static double FontHeight = printManager.FontSize;

        /// <summary>
        /// タイトルを印字する位置（ページのマージンを考慮した位置）
        ///  pt ポイント
        /// </summary>
        public static XPoint titlePos = new XPoint(0, printManager.FontHeight);

        /// <summary>
        /// 大きめ（テーブルの間など）の改行高さ
        ///  pt ポイント
        /// </summary>
        public static double LineSpacing1 = printManager.FontHeight + 15;

        /// <summary>
        /// 中くらい（タイトル後など）の改行高さ
        ///  pt ポイント
        /// </summary>
        public static double LineSpacing2 = printManager.FontHeight * 1.5;

        /// <summary>
        /// インデント（1文字）
        ///  pt ポイント
        /// </summary>
        public static double LineSpacing3 = printManager.FontSize;

        /// <summary>
        /// インデント（3文字）
        ///  pt ポイント
        /// </summary>
        public static double LineSpacing4 = printManager.FontSize * 3.0;



    }
}
