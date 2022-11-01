using Printing.Calcrate;
using Printing.Comon;
using System;

namespace Printing
{
    public class CalcPrint
    {
        private GirderData.GirderData data;

        public CalcPrint(GirderData.GirderData inp)
        {
            this.data = inp;
        }

        public void createPDF()
        {
            //  PDF出力のためのclassの呼び出し
            //  整形したデータを送る
            // PDF ページを準備する
            var mc = new PdfDocument(this.data);
            calcBeam.printPDF(mc);

            // PDFファイルを生成する
            mc.SavePDF();
        }

        public string getPdfSource()
        {
            //  PDF出力のためのclassの呼び出し
            //  整形したデータを送る
            var mc = new PdfDocument(this.data);
            calcBeam.printPDF(mc);

            // PDF を Byte型に変換
            var b = mc.GetPDFBytes();

            // Byte型配列をBase64文字列に変換
            string str = Convert.ToBase64String(b);

            // PDFファイルを生成する
            return str;
        }
    }
}
