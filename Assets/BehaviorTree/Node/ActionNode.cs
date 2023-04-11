using System;
namespace BehaviorTree.Node
{
    /// <summary>
    /// 动作节点
    /// 调用函数，返回Success
    /// </summary>
    public class ActionNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.Action; } }
        private Action _action;
        public ActionNode(Action action) : base()
        {
            _action = action;
        }
        public override void Visit()
        {
            base.Visit();
            if (_action != null)
                _action();
            SetStatus(NodeStatus.Success);
        }
    }
}