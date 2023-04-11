
namespace BehaviorTree.Node
{
    public enum NodeType {
        None,
        Condition,                      // 条件节点
        ConditionWait,                  // 条件等待节点
        Action,                         // 动作节点
        Delay,                          // 延迟节点
        If,                             // If节点
        Negation,                       // 取反节点
        Sequence,                       // 序列节点
        Selector,                       // 选择节点
        ParallelSequence,               // 并行序列节点
        ParallelSelector,               // 并行选择节点
        Random,                         // 随机节点
    }
}
