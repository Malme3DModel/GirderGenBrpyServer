﻿using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using System;
using System.IO;
using System.Reflection;

namespace Printing.Comon
{
    internal class PdfDocument
    {
        private PdfSharpCore.Pdf.PdfDocument document;
        public TrimMargins Margine;     // マージン
        public PdfPage currentPage;     // 現在のページ

        public XGraphics gfx;   // 描画するための

        // 文字に関する情報
        public XFont font_mic;  // 明朝フォント
        public XFont font_got;  // ゴシックフォント

        // 図形に関する情報
        public XPen xpen = new XPen(XColors.Black, 1);

        private PageSize pageSize;
        private PageOrientation pageOrientation;

        private string title;   // 全ページ共通 左上に印字するタイトル
        // private string casename;    //  左上に印字するケース名

        /// <summary>
        /// 現在の座標
        /// </summary>
        public XPoint currentPos;


        public double FontSize = 10;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="pd">印刷設定が記録されている</param>
        public PdfDocument(GirderData.GirderData pd)
        {
            //　新規ドキュメントの作成
            document = new PdfSharpCore.Pdf.PdfDocument();
            document.Info.Title = "FrameWebForJS";

            // フォントリゾルバーのグローバル登録
            var FontResolver = GlobalFontSettings.FontResolver.GetType();
            if (FontResolver.Name != "JapaneseFontResolver")
                GlobalFontSettings.FontResolver = new JapaneseFontResolver();

            // フォントの設定
            font_mic = new XFont("MS Mincho", printManager.FontSize, XFontStyle.Regular);
            font_got = new XFont("MS Gothic", printManager.FontSize, XFontStyle.Regular);

            title = pd.title;

            // 新しいページを作成する
            this.NewPage(pd.pageSize, pd.pageOrientation);

        }

        /// <summary>
        /// 用紙の印刷可能な範囲の大きさ
        /// </summary>
        public XSize currentPageSize
        {
            get
            {
                double height = currentPage.Height;
                height -= Margine.Top;
                height -= Margine.Bottom;

                double width = currentPage.Width;
                width -= Margine.Left;
                width -= Margine.Right;

                return new XSize(width, height);
            }
        }


        /// <summary>
        /// 改行する
        /// </summary>
        /// <param name="LF"></param>
        public void addCurrentY(double LF)
        {
            currentPos.Y += LF;
        }
        /// <summary>
        /// 改行する
        /// </summary>
        /// <param name="LI"></param>
        public void addCurrentX(double LI)
        {
            currentPos.X += LI;
        }
        public void setCurrentX(double X)
        {
            currentPos.X = Margine.Left;
            currentPos.X += X;
        }

        private int Target;           // これから描く図が, 紙面のどの位置なのか

        public int currentArea
        {
            get
            {
                return this.Target;
            }
            set
            {
                this.Target = value;
            }
        }

        private XPoint[] _Center = new XPoint[2] { new XPoint(0, 0), new XPoint(0, 0) };  // 描く図の紙面における中心位置
        public void printText(double _x1, double _y1, string str, double angle = 90, XFont font = null)
        {

            var centerPos = this._Center[this.currentArea];

            var x1 = centerPos.X + _x1;
            var y1 = centerPos.Y + _y1;

            this.currentPos.X = x1;
            this.currentPos.Y = y1;

            //var angle = radian * (180 / Math.PI);

            this.gfx.RotateAtTransform(angle, this.currentPos);
            Text.PrtText(this, str, font);
            this.gfx.RotateAtTransform(-angle, this.currentPos);

        }

        /// <summary>
        /// 改ページ
        /// </summary>
        public void NewPage(string pageSize, string pageOrientation)
        {
            // 用紙の大きさ
            switch (pageSize)
            {
                case "A4":
                    this.pageSize = PageSize.A4;
                    break;
                case "A3":
                    this.pageSize = PageSize.A3;
                    break;
            }
            //ページの向き
            switch (pageOrientation)
            {
                case "Vertical":
                    this.pageOrientation = PageOrientation.Portrait;
                    break;
                case "Horizontal":
                    this.pageOrientation = PageOrientation.Landscape;
                    break;
            }

            setDefaultMargine();

            NewPage();
        }

        /// <summary>
        /// マージンを設定する
        /// </summary>
        private void setDefaultMargine()
        {
            Margine = new TrimMargins();
            Margine.Top = 25;
            Margine.Left = 35;
            Margine.Right = 25;
            Margine.Bottom = 25;
        }

        /// <summary>
        /// 改ページ
        /// </summary>
        public void NewPage()
        {
            // 白紙をつくる（Ａ４縦）
            currentPage = document.AddPage();
            currentPage.Size = pageSize;
            currentPage.Orientation = pageOrientation;
            currentPage.TrimMargins = Margine;

            // XGraphicsオブジェクトを取得
            gfx = XGraphics.FromPdfPage(currentPage);

            // 初期位置を設定する
            currentPos = new XPoint(Margine.Left, Margine.Top);
            currentPos.Y += printManager.titlePos.Y;
            currentPos.X += printManager.titlePos.X;

            // タイトルの印字
            if (title != null)
            {
                Text.PrtText(this, string.Format("TITLE : {0}", title));
            }
            currentPos.Y += printManager.FontHeight;
            currentPos.Y += printManager.LineSpacing2;

        }

        /// <summary>
        /// PDF を Byte型に変換
        /// </summary>
        /// <returns></returns>
        public byte[] GetPDFBytes()
        {
            // Creates a new Memory stream
            MemoryStream stream = new MemoryStream();

            // Saves the document as stream
            document.Save(stream);
            document.Close();

            // Converts the PdfDocument object to byte form.
            byte[] docBytes = stream.ToArray();

            return docBytes;
        }

        /// <summary>
        /// PDF を保存する
        /// </summary>
        /// <param name="filename"></param>
        public void SavePDF(string filename = @"../../../TestData/Test.pdf")
        {
            // PDF保存（カレントディレクトリ）
            document.Save(filename);
        }

        /// <summary>
        /// 文字の大きさを取得する
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal XSize MeasureString(string text)
        {
            XSize result = gfx.MeasureString(text, font_mic);
            return result;
        }
    }
}


// 日本語フォントのためのフォントリゾルバー
internal class JapaneseFontResolver : IFontResolver
{
    public string DefaultFontName => throw new NotImplementedException();

    // MS 明朝
    public static readonly string MS_MINCHO_TTF = "PDF_Manager.fonts.MS Mincho.ttf";


    // MS ゴシック
    private static readonly string MS_GOTHIC_TTF = "PDF_Manager.fonts.MS Gothic.ttf";


    public byte[] GetFont(string faceName)
    {
        switch (faceName)
        {
            case "MsMincho#Medium":
                return LoadFontData(MS_MINCHO_TTF);

            case "MsGothic#Medium":
                return LoadFontData(MS_GOTHIC_TTF);

        }
        return null;
    }

    public FontResolverInfo ResolveTypeface(
                string familyName, bool isBold, bool isItalic)
    {
        var fontName = familyName.ToLower();

        switch (fontName)
        {
            case "ms gothic":
                return new FontResolverInfo("MsGothic#Medium");
        }

        // デフォルトのフォント
        return new FontResolverInfo("MsMincho#Medium");
    }

    // 埋め込みリソースからフォントファイルを読み込む
    private byte[] LoadFontData(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
                throw new ArgumentException("No resource with name " + resourceName);

            int count = (int)stream.Length;
            byte[] data = new byte[count];
            stream.Read(data, 0, count);
            return data;
        }
    }
}




