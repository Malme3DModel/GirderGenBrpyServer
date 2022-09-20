namespace GirderGenTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Tester = new GirderGenBrpyServer.Calculate.Calc();
            Tester.Test();
        }
    }
}