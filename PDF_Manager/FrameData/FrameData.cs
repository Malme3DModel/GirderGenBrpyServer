using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameData.InputData;

namespace FrameData
{
    public class FrameData
    {
        // classをまとめてここに代入する．
        public Dictionary<string,object> frameDatas = new Dictionary<string,object>();

        //public PrintData(Dictionary<string, object> data)
        public FrameData()
        {
 
            // node
            frameDatas.Add(InputNode.KEY,new InputNode().nodes);
            // element
            frameDatas.Add(InputElement.KEY,new InputElement().element);
            // member
            frameDatas.Add(InputMember.KEY,new InputMember().member);
            //// fixnode
            frameDatas.Add( InputFixNode.KEY,new InputFixNode().fix_node);
            //// joint
            //frameDatas.Add(InputJoint.KEY, new InputJoint(data));
            //// notice_points
            //frameDatas.Add(InputNoticePoints.KEY, new InputNoticePoints(data));
            //// fixmember
            //frameDatas.Add(InputFixMember.KEY, new InputFixMember(data));
            //// shell
            //frameDatas.Add(InputShell.KEY, new InputShell(data));
            //// load
            //基本荷重
            //new InputLoadName();
            //実荷重
            frameDatas.Add(InputLoad.KEY,new InputLoad().load);
            //// define
            //frameDatas.Add(InputDefine.KEY, new InputDefine(data));
            //// combine 
            //frameDatas.Add(InputCombine.KEY, new InputCombine(data));
            //// pickup
            //frameDatas.Add(InputPickup.KEY, new InputPickup(data));

            //// disg
            //this.frameDatas.Add(ResultDisg.KEY, new ResultDisg(data));
            //// disgcombine
            //this.frameDatas.Add(ResultDisgCombine.KEY, new ResultDisgCombine(data));
            //// disgPickup
            //this.frameDatas.Add(ResultDisgPickup.KEY, new ResultDisgPickup(data));
            //// fsec
            //this.frameDatas.Add(ResultFsec.KEY, new ResultFsec(data));
            //// fseccombine
            //this.frameDatas.Add(ResultFsecCombine.KEY, new ResultFsecCombine(data));
            //// fsecPickup
            //this.frameDatas.Add(ResultFsecPickup.KEY, new ResultFsecPickup(data));
            //// reac
            //this.frameDatas.Add(ResultReac.KEY, new ResultReac(data));
            //// reaccombine
            //this.frameDatas.Add(ResultReacCombine.KEY, new ResultReacCombine(data));
            //// reacPickup
            //this.frameDatas.Add(ResultReacPickup.KEY, new ResultReacPickup(data));

            //// 荷重図
            //if (data.ContainsKey(DiagramInput.KEY))
            //    this.frameDatas.Add(DiagramInput.KEY, new DiagramInput(data));
            //// 断面力図
            //if (data.ContainsKey(DiagramResult.KEY))
            //    this.frameDatas.Add(DiagramResult.KEY, new DiagramResult(data));

            //user
            frameDatas.Add(InputUser.KEY, InputUser.uid);

        }
    }
}
