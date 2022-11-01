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
    }
}
