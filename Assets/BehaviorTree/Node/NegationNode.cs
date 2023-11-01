using Cysharp.Threading.Tasks;
using System;
namespace BehaviorTree.Node
{
    /// <summary>
    /// 取反节点
    /// 当子节点状态为Running时，返回Running
    /// 当子节点状态为Success时，返回Failed
    /// 当子节点状态为Failed时，返回Success
    /// </summary>
    public class NegationNode : NodeBase
    {
        public override int Type => NodeType.Negation;
        private NodeBase _node;
        public NegationNode(NodeBase node) : base()
        {
            _node = node;
            SetChildrens(new NodeBase[] { _node });
        }
        public async override UniTask Visit()
        {
            await base.Visit();
            await _node.Visit();
            switch(_node.Status)
            {
                case NodeStatus.Running:
                    SetStatus(NodeStatus.Running);
                    break;
                case NodeStatus.Success:
                    SetStatus(NodeStatus.Failed);
                    break;
                case NodeStatus.Failed:
                    SetStatus(NodeStatus.Success);
                    break;
            }
        }
    }
}