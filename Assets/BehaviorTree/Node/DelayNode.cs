using Cysharp.Threading.Tasks;
using System;
using System.Diagnostics;

namespace BehaviorTree.Node
{
    /// <summary>
    /// 延迟节点
    /// 延迟 x ms执行节点，等待阶段返回Running，执行返回执行节点结果
    /// </summary>
    public class DelayNode : ConditionWaitNode
    {
        public override int Type => NodeType.Delay;
        private NodeBase _node;
        private double _startTime;
        private double _waitTime;

        private Stopwatch _stopwatch;
        public DelayNode(double waitTime, NodeBase node) : base(null)
        {
            _node = node;
            _waitTime = waitTime;

            _stopwatch = Stopwatch.StartNew();
            _startTime = _stopwatch.Elapsed.TotalMilliseconds;

            _func = CheckWait;
        }
        private bool CheckWait()
        {
            var timeOffset = _stopwatch.Elapsed.TotalMilliseconds - _startTime;
            return timeOffset >= _waitTime;
        }
        public async override UniTask Visit()
        {
            await base.Visit();
            if(Status == NodeStatus.Ready)
            {
                _stopwatch = Stopwatch.StartNew();
                _startTime = _stopwatch.Elapsed.TotalMilliseconds;
            }
            if(Status == NodeStatus.Success)
            {
                await _node.Visit();
                SetStatus(_node.Status);
            }
        }
    }
}