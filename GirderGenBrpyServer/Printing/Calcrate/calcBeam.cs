using Printing.Comon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing.Calcrate
{
    internal class calcBeam
    {
        internal static void printPDF(PdfDocument mc)
        {
            mc.NewPage();
            Text.PrtText(mc, "てすと");



        }
    }
}
