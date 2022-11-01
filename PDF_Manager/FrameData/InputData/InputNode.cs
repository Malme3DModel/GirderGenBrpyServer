using System.Collections.Generic;

namespace FrameData.InputData
{
    public class Vector3
    {
        public double x;
        public double y;
        public double z;
    }

    internal class InputNode
    {
        public const string KEY = "node";
        public Dictionary<string, Vector3> nodes = new Dictionary<string, Vector3>();

        public InputNode()
        {
            var n  =new Vector3();
            n.x = 0;
            n.y = 0;
            n.z = 0;
            this.nodes.Add("1",n);

            n = new Vector3();
            n.x = 2;
            n.y = 0;
            n.z = 0;
            this.nodes.Add("2",n);

            n = new Vector3();
            n.x = 6;
            n.y = 0;
            n.z = 0;
            this.nodes.Add("3",n);
        }
    }
}

