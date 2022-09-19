using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Printing.PrintInput
{
    //public class LoadMember
    //{
    //    public string m1;
    //    public string m2;
    //    public string direction;
    //    public string mark;
    //    public string L1;
    //    public double L2;
    //    public double P1;
    //    public double P2;
    //}

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
        public string name;
        public double LL_pitch;
        public int fix_node;
        public int element;
        public int fix_member;
        public int joint;
        public Array load_node;
    }

    //public class Load
    //{
    //    public LoadName[] loadNames;
    //    public ActualLoad[] actualLoads;
    //}

    internal class InputLoad
    {
                public const string KEY = "load";
        public Dictionary<string, object> load = new Dictionary<string, object>();

        public InputLoad() { 
            var l = new LoadName();

            var LoadN = new List<LoadNode>();
            var ln = new LoadNode();
            ln.n = "1";
            ln.tx = 0;
            ln.ty = 0;
            ln.tz = 2;
            ln.rx = 0;
            ln.ry = 0;
            ln.rz = 0;
            LoadN.Add(ln);
            l.load_node = LoadN.ToArray();
            //lo.Add("load_node",LoadN.ToArray());

            l.fix_node = 1;
            l.element = 1;
            l.LL_pitch = 0.1;
            //this.loadnames.Add("1",l);

            this.load.Add("1", l);
        }
}
}
