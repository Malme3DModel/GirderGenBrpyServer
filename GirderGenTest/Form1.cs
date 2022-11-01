using System.Diagnostics;
using System.Text;

namespace GirderGenTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // memo: Shift-JISを扱うためのおまじない
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 読み込みたいテキストを開く
            using (StreamReader st = new StreamReader(@"../../../TestData/test001.json", Encoding.GetEncoding("shift-jis")))
            {
                // テキストファイルをString型で読み込みコンソールに表示
                String line = st.ReadToEnd();

                // データの読み込み
                var inp = new GirderData.GirderData(line);

                var calc = new Printing.CalcPrint(inp);
                calc.createPDF();

                // PDF を表示する
                var oProc = new Process();
                oProc.StartInfo.FileName = Path.GetFullPath("../../../TestData/Test.pdf");
                oProc.StartInfo.UseShellExecute = true;
                oProc.Start();
                oProc.WaitForExit();

                // MessageBox.Show("ｵﾜﾀ＼(^o^)／");
            }

            /*
            var Tester = new FrameData.Calc();
            Tester.Test();
            */
        }
    }
}