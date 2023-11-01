using BehaviorTree.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree.Const
{
    public static class FrameNodeConst
    {

        /// <summary>
        /// 节点队列
        /// 无论成功与否只按顺序执行一遍
        /// 如果子节点状态为Running，则返回Running，下次会继续执行接下来的子节点并等待返回其他结果（Success或Failed）
        /// 如果子节点状态为Success，则执行成功节点 SuccessNode
        /// 如果子节点状态为Failed，则执行失败节点 FailedNode
        /// </summary>
        public static NodeBase QueueNode(NodeBase[] childrens,NodeBase successNode, NodeBase failedNode)
        {
            var nodes = new NodeBase[childrens.Length+2];
            Array.Copy(childrens,nodes, childrens.Length);
            nodes[nodes.Length - 2] = successNode;
            nodes[nodes.Length - 1] = new EndNode();
            return new SelectorNode(new NodeBase[] {
                new SequenceNode(nodes),
                new SequenceNode(new NodeBase[]{
                    failedNode,
                    new EndNode(),
                }),
            }).SetType(NodeType.Queue);
        }
    }
}
