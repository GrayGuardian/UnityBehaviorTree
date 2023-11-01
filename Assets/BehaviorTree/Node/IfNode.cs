using System;
namespace BehaviorTree.Node
{
    /// <summary>
    /// If节点
    /// If节点，根据条件函数判断是否执行子节点，并返回子节点状态
    /// 当条件函数不满足时，返回Failed
    /// 当条件函数满足时，返回子节点状态
    /// </summary>
    public class IfNode : ConditionNode
    {
        public override int Type => NodeType.If; 
        private NodeBase _node;
        public IfNode(Func<bool> func, NodeBase node) : base(func)
        {
            _node = node;
        }
        public override void Visit()
        {
            base.Visit();
            if (Status == NodeStatus.Success)
            {
                _node.Visit();
                SetStatus(_node.Status);
            }
        }
    }
}