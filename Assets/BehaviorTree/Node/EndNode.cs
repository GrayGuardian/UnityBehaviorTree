using Cysharp.Threading.Tasks;
using System;
namespace BehaviorTree.Node
{
    /// <summary>
    /// 结束节点
    /// 停止所在的行为树
    /// </summary>
    public class EndNode : NodeBase
    {
        public override int Type => NodeType.End; 
        public EndNode() : base()
        {

        }
        public async override UniTask Visit()
        {
            await base.Visit();
            Tree.Disposal();
            SetStatus(NodeStatus.Success);
        }
    }
}