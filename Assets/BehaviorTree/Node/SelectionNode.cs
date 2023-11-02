using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree.Node
{
    /// <summary>
    /// 选择节点
    /// 如果子节点状态为Success，则执行SuccessNode，若SuccessNode为Success时，则返回Success，否则执行FailedNode
    /// 如果子节点状态为Running，则返回Running，下次会继续执行接下来的子节点并等待返回其他结果（Success或Failed）
    /// 如果子节点状态为Failed，则执行FailedNode，若FailedNode为Success时，则返回Failed，否则返回Success
    /// </summary>
    public class SelectionNode : NodeBase
    {
        public override int Type => NodeType.Selection;
        private NodeBase _node;
        public SelectionNode(NodeBase conditionNode, NodeBase successNode = null, NodeBase failedNode = null) : base()
        {
            _node = new NegationNode(new SequenceNode(new NodeBase[] {
                new NegationNode(new SequenceNode(new NodeBase[] { conditionNode, successNode })),
                failedNode
            }));
            SetChildrens(new NodeBase[] { _node });
        }
        public async override UniTask Visit()
        {
            await base.Visit();
            await _node.Visit();
            SetStatus(_node.Status);
        }
    }
}
