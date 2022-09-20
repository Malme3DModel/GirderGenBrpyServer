using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Printing;
using Printing.PrintInput;

namespace GirderGenBrpyServer.Printing
{
    public class PrintData
    {
        // classをまとめてここに代入する．
        public Dictionary<string,object> printDatas = new Dictionary<string,object>();

        //public PrintData(Dictionary<string, object> data)
        public PrintData()
        {
            //// 2次元か3次元かを記憶
            //if (data.ContainsKey("dimension"))
            //    printDatas.Add("dimension", int.Parse(data["dimension"].ToString()));
            //else
            //    printDatas.Add("dimension", 3);

            //// 言語を記憶
            //if (data.ContainsKey("language"))
            //    printDatas.Add("language", data["language"].ToString());
            //else
            //    printDatas.Add("language", "ja");

            //// ペーパサイズ
            //if (data.ContainsKey("pageSize"))
            //    printDatas.Add("pageSize", data["pageSize"].ToString());
            //else
            //    printDatas.Add("pageSize", "A4");

            //// ペーパ向き
            //if (data.ContainsKey("pageOrientation"))
            //    printDatas.Add("pageOrientation", data["pageOrientation"].ToString());
            //else
            //    printDatas.Add("pageOrientation", "Vertical"); // or Horizontal

            //// タイトル
            //if (data.ContainsKey("title"))
            //    printDatas.Add("title", data["title"].ToString());
            //else
            //    printDatas.Add("title", null);

            // node
            printDatas.Add(InputNode.KEY,new InputNode().nodes);
            // element
            printDatas.Add(InputElement.KEY,new InputElement().element);
            // member
            printDatas.Add(InputMember.KEY,new InputMember().member);
            //// fixnode
            printDatas.Add( InputFixNode.KEY,new InputFixNode().fix_node);
            //// joint
            //printDatas.Add(InputJoint.KEY, new InputJoint(data));
            //// notice_points
            //printDatas.Add(InputNoticePoints.KEY, new InputNoticePoints(data));
            //// fixmember
            //printDatas.Add(InputFixMember.KEY, new InputFixMember(data));
            //// shell
            //printDatas.Add(InputShell.KEY, new InputShell(data));
            //// load
            //基本荷重
            //new InputLoadName();
            //実荷重
            printDatas.Add(InputLoad.KEY,new InputLoad().load);
            //// define
            //printDatas.Add(InputDefine.KEY, new InputDefine(data));
            //// combine 
            //printDatas.Add(InputCombine.KEY, new InputCombine(data));
            //// pickup
            //printDatas.Add(InputPickup.KEY, new InputPickup(data));

            //// disg
            //this.printDatas.Add(ResultDisg.KEY, new ResultDisg(data));
            //// disgcombine
            //this.printDatas.Add(ResultDisgCombine.KEY, new ResultDisgCombine(data));
            //// disgPickup
            //this.printDatas.Add(ResultDisgPickup.KEY, new ResultDisgPickup(data));
            //// fsec
            //this.printDatas.Add(ResultFsec.KEY, new ResultFsec(data));
            //// fseccombine
            //this.printDatas.Add(ResultFsecCombine.KEY, new ResultFsecCombine(data));
            //// fsecPickup
            //this.printDatas.Add(ResultFsecPickup.KEY, new ResultFsecPickup(data));
            //// reac
            //this.printDatas.Add(ResultReac.KEY, new ResultReac(data));
            //// reaccombine
            //this.printDatas.Add(ResultReacCombine.KEY, new ResultReacCombine(data));
            //// reacPickup
            //this.printDatas.Add(ResultReacPickup.KEY, new ResultReacPickup(data));

            //// 荷重図
            //if (data.ContainsKey(DiagramInput.KEY))
            //    this.printDatas.Add(DiagramInput.KEY, new DiagramInput(data));
            //// 断面力図
            //if (data.ContainsKey(DiagramResult.KEY))
            //    this.printDatas.Add(DiagramResult.KEY, new DiagramResult(data));

            //user
            printDatas.Add(InputUser().KEY,InputUser().uid);

        }
    }
}
