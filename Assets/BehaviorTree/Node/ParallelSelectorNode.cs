namespace BehaviorTree.Node
{
    /// <summary>
    /// 并行选择节点
    /// 从头到尾，执行所有节点
    /// 如果子节点状态为Failed或Running，则执行下一个子节点
    /// 如果子节点状态为Success，则返回Success
    /// 如果所有节点都为Failed，则返回Failed，否则返回Running
    /// </summary>
    public class ParallelSelectorNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.Sequence; } }

        public ParallelSelectorNode(NodeBase[] childrens) : base()
        {
            SetChildrens(childrens);
        }
        public override void Visit()
        {
            base.Visit();
            bool allFailed = true;
            foreach (var node in Childrens)
            {
                if(node.Status != NodeStatus.Failed)
                {
                    node.Visit();
                    if(node.Status == NodeStatus.Success)
                    {
                        SetStatus(NodeStatus.Success);
                        return;
                    }
                }
                if(node.Status == NodeStatus.Running)
                    allFailed = false;
            }
            if(allFailed)
                SetStatus(NodeStatus.Failed);
            else
                SetStatus(NodeStatus.Running);
        }
    }
}