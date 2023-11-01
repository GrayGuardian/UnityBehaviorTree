namespace BehaviorTree.Node
{
    /// <summary>
    /// 并行序列节点
    /// 从头到尾，执行所有节点
    /// 如果子节点状态为Success或Running，则执行下一个子节点
    /// 如果子节点状态为Failed，则返回Failed
    /// 如果所有节点都为Success，则返回Success，否则返回Running
    /// </summary>
    public class ParallelSequenceNode : NodeBase
    {
        public override int Type => NodeType.Sequence;

        public ParallelSequenceNode(NodeBase[] childrens) : base()
        {
            SetChildrens(childrens);
        }
        public override void Visit()
        {
            base.Visit();
            bool allSuceess = true;
            foreach (var node in Childrens)
            {
                if(node.Status != NodeStatus.Success)
                {
                    node.Visit();
                    if(node.Status == NodeStatus.Failed)
                    {
                        StopAllRuningNode();
                        SetStatus(NodeStatus.Failed);
                        return;
                    }
                }
                if(node.Status == NodeStatus.Running)
                    allSuceess = false;
            }
            if(allSuceess)
            {
                StopAllRuningNode();
                SetStatus(NodeStatus.Success);
            }   
            else
                SetStatus(NodeStatus.Running);
        }
        private void StopAllRuningNode()
        {
            foreach (var node in Childrens)
            {
                if (node.Status == NodeStatus.Running) node.SetStatus(NodeStatus.Failed);
            }
        }
    }
}