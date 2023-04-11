using System;
namespace BehaviorTree.Node
{
    /// <summary>
    /// 延迟节点
    /// 延迟执行节点，等待阶段返回Running，执行返回执行节点结果
    /// </summary>
    public class DelayNode : ConditionWaitNode
    {
        public override NodeType Type { get { return NodeType.Delay; } }
        private NodeBase _node;
        private float _startTime;
        private float _waitTime;
        private float _timeOffset;
        public DelayNode(float waitTime, NodeBase node) : base(null)
        {
            _node = node;
            _waitTime = waitTime;
            ResetParameter();
            _func = CheckWait;
        }
        private bool CheckWait()
        {
            float timeOffset = UnityEngine.Time.unscaledTime - _startTime;
            return timeOffset >= _waitTime;
        }

        protected override void ResetParameter()
        {
            _startTime = UnityEngine.Time.unscaledTime;
        }
        public override void Visit()
        {
            base.Visit();
            if(Status == NodeStatus.Success)
            {
                _node.Visit();
                SetStatus(_node.Status);
            }
        }
    }
}