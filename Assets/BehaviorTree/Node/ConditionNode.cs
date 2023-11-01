using Cysharp.Threading.Tasks;
using System;
namespace BehaviorTree.Node
{
    /// <summary>
    /// 条件节点
    /// 根据函数调用结果返回Success或Failed
    /// </summary>
    public class ConditionNode : NodeBase
    {
        public override int Type => NodeType.Condition;
        protected Func<bool> _func;
        public ConditionNode(Func<bool> func) : base()
        {
            _func = func;
        }
        public async override UniTask Visit()
        {
            await base.Visit();
            if(_func!=null && _func())
                SetStatus(NodeStatus.Success);
            else
                SetStatus(NodeStatus.Failed);
        }
    }
}