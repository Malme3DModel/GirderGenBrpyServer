using GirderGenBrpyServer.Printing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace Printing.PrintInput
{
    internal class Member
    {
        public string ni; // 節点番号
        public string nj;
        public string e;  // 材料番号
        public double cg = 0; // コードアングル

        // 他のモジュールで使う変数
        public double L = double.NaN;       // 要素の長さ
        public double[, ] t = null;         // 座標変換マトリックス
        public double radian = double.NaN;   // 角度
    }

    internal class InputMember
    {
                public const string KEY = "member";
        public Dictionary<string,Member> member = new Dictionary<string, Member>();
        
        public InputMember()
        {
            var m = new Member();
            m.ni = "1";
            m.nj = "2";
            m.e = "1";
            this.member.Add("1",m);

            m = new Member();
            m.ni = "2";
            m.nj = "3";
            m.e = "1";
            this.member.Add("2",m);
        }
    }
}