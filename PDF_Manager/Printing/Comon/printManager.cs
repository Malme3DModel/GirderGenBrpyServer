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


        /// <summary>
        /// 節見出し(X. ・・・)の行インデント
        ///  pt ポイント
        /// </summary>
        public static double SectionIndent => 0;

        /// <summary>
        /// 小節見出し(X.Y ・・・)の行インデント
        ///  pt ポイント
        /// </summary>
        public static double SubsectionIndent => SectionIndent + LineSpacing3;

        /// <summary>
        /// 小々節見出し(X.Y.Z ・・・)の行インデント
        ///  pt ポイント
        /// </summary>
        public static double SubsubsectionIndent => SubsectionIndent + LineSpacing3;

        /// <summary>
        /// 本文段落の行インデント
        ///  pt ポイント
        /// </summary>
        public static double ParagraphIndent => SubsubsectionIndent + LineSpacing3;

        /// <summary>
        /// 本文段落の先頭行の行インデント
        ///  pt ポイント
        /// </summary>
        public static double BeginningOfParagraphIndent => ParagraphIndent + LineSpacing3;

    }
}
