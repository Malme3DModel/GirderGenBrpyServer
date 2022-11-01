using System.Diagnostics;
using System.Text;

namespace GirderGenTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // memo: Shift-JIS���������߂̂��܂��Ȃ�
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // �ǂݍ��݂����e�L�X�g���J��
            using (StreamReader st = new StreamReader(@"../../../TestData/test001.json", Encoding.GetEncoding("shift-jis")))
            {
                // �e�L�X�g�t�@�C����String�^�œǂݍ��݃R���\�[���ɕ\��
                String line = st.ReadToEnd();

                // �f�[�^�̓ǂݍ���
                var inp = new GirderData.GirderData(line);

                var calc = new Printing.CalcPrint(inp);
                calc.createPDF();

                // PDF ��\������
                var oProc = new Process();
                oProc.StartInfo.FileName = Path.GetFullPath("../../../TestData/Test.pdf");
                oProc.StartInfo.UseShellExecute = true;
                oProc.Start();
                oProc.WaitForExit();

                // MessageBox.Show("����_(^o^)�^");
            }

            /*
            var Tester = new FrameData.Calc();
            Tester.Test();
            */
        }
    }
}