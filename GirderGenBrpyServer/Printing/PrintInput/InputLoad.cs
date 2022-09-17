using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Printing.PrintInput
{
    public class LoadMember
    {
        public string m1;
        public string m2;
        public string direction;
        public string mark;
        public string L1;
        public double L2;
        public double P1;
        public double P2;
    }

    public class LoadNode
    {
        public string n;
        public double tx;
        public double ty;
        public double tz;
        public double rx;
        public double ry;
        public double rz;
    }

    public class LoadName
    {
        public double rate;
        public string symbol;
        public string Actual_load;
        public string name;
        public int fix_node;
        public int element;
        public int fix_member;
        public int joint;
    }

    public class Load
    {
        public LoadMember[] load_member;
        public LoadNode[] load_node;
        public LoadName[] loadname;
    }
}
