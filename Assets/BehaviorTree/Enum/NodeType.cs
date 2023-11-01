
namespace BehaviorTree.Node
{
    /// <summary>
    /// 节点类型
    /// 0-99为预留节点类型，扩展节点类型枚举请从100开始
    /// </summary>
    public static class NodeType
    {
        public const int None = 0;
        public const int Condition = 1;        // 条件节点
        public const int ConditionWait = 2;        // 条件等待节点
        public const int Action = 3;        // 动作节点
        public const int Delay = 4;        // 延迟节点
        public const int If = 5;        // If节点
        public const int Negation = 6;        // 取反节点
        public const int Sequence = 7;        // 序列节点
        public const int Selector = 8;        // 选择节点
        public const int ParallelSequence = 9;        // 并行序列节点
        public const int ParallelSelector = 10;       // 并行选择节点
        public const int Random = 11;       // 随机节点
        public const int End = 99;       // 结束节点
    }
}
