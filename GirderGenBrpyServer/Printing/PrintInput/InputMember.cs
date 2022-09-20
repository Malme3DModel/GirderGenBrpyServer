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