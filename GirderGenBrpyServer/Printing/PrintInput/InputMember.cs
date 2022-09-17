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
        public double cg; // コードアングル

        // 他のモジュールで使う変数
        public double L = double.NaN;       // 要素の長さ
        public double[, ] t = null;         // 座標変換マトリックス
        public double radian = double.NaN;   // 角度
    }

    internal class InputMember
    {
        public const string KEY = "member";

        Dictionary<string, Dictionary<string, string>> members = new Dictionary<string, Dictionary<string, string>>(){
  {"1", new Dictionary<string, string>(){{"ni", "1"},{"nj", "2"},{"e", "1"}}},
  {"2", new Dictionary<string, string>(){{"ni", "2"},{"nj", "3"},{"e", "1"}}}
};

    }
}