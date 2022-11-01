using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FrameData.InputData
{
    public class Element
    {
        public double E;
        public double G;
        public double Xp;
        public double A;
        public double J;
        public double Iy;
        public double Iz;
    }

    internal class InputElement
    {
        public const string KEY = "element";
        public Dictionary<string, Dictionary<string, Element>> element = new Dictionary<string, Dictionary<string, Element>>();

        public InputElement()
        {
            var e = new Dictionary<string, Element>();
            var ee = new Element();
            ee.E = 20000000;
            ee.G = 770000;
            ee.Xp = 0.00001;
            ee.A = 1.0;
            ee.J = 1.0;
            ee.Iy = 1.0;
            ee.Iz = 1.0;

            e.Add("1", ee);
            this.element.Add("1", e);
        }
    }
}
