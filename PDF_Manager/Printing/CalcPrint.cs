using Printing.Calcrate;
using Printing.Comon;
using System;

namespace Printing
{
    public class CalcPrint
    {
        private GirderData.GirderData data;
        private PdfDocument mc;

        public CalcPrint(GirderData.GirderData inp)
        {
            this.data = inp;

            this.mc = new PdfDocument(this.data);
            calcBeam.printPDF(this.mc, this.data);
        }

        public void createPDF()
        {
            // PDFファイルを生成する
            this.mc.SavePDF();
        }

        public string getPdfSource()
        {
            // PDF を Byte型に変換
            var b = this.mc.GetPDFBytes();

            // Byte型配列をBase64文字列に変換
            string str = Convert.ToBase64String(b);

            // PDFファイルを生成する
            return str;
        }
    }
}
