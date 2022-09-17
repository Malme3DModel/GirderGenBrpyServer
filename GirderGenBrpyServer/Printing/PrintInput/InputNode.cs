using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Printing.PrintInput
{
    internal class InputNode
    {
        public const string KEY = "node";

        Dictionary<string, Dictionary<string, int>> nodes = new Dictionary<string, Dictionary<string, int>>(){
  {"1", new Dictionary<string, int>(){{"x", 0},{"y", 0},{"z", 0}}},
  {"2", new Dictionary<string, int>(){{"x", 2},{"y", 0},{"z", 0}}},
  {"3", new Dictionary<string, int>(){{"x", 6},{"y", 0},{"z", 0}}},
};

    }

}

