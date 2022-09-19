using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Printing.PrintInput
{
    public class FixNode
    {
        public string n;    // 節点番号
        public double tx;
        public double ty;
        public double tz;
        public double rx;
        public double ry;
        public double rz;
    }

    internal class InputFixNode
    {
        public const string KEY = "fix_node";
        public Dictionary<string, List<FixNode>> fix_node = new Dictionary<string, List<FixNode>>();    

        public InputFixNode()
        {
            var ff = new List<FixNode>();
            var f = new FixNode();
            f.n = "2";
            f.tx = 1;
            f.ty = 1;
            f.tz = 1;
            f.rx = 0;
            f.ry = 0;
            f.rz = 0;
            ff.Add(f);

            f = new FixNode();
            f.n = "2";
            f.tx = 1;
            f.ty = 1;
            f.tz = 1;
            f.rx = 0;
            f.ry = 0;
            f.rz = 0;
            ff.Add(f);

            this.fix_node.Add("1", ff);
        }
    }
}
