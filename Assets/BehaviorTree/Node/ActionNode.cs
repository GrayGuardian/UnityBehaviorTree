using Cysharp.Threading.Tasks;
using System;
namespace BehaviorTree.Node
{
    /// <summary>
    /// 动作节点
    /// 调用函数，返回Success
    /// </summary>
    public class ActionNode : NodeBase
    {
        public override int Type => NodeType.Action;
        private Action _action;
        public ActionNode(Action action) : base()
        {
            _action = action;
        }
        public async override UniTask Visit()
        {
            await base.Visit();
            if (_action != null)
                _action();
            SetStatus(NodeStatus.Success);
        }
    }
}