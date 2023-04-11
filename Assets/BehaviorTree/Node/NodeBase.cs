
namespace BehaviorTree.Node
{
    /// <summary>
    /// 节点基类
    /// </summary>
    public abstract class NodeBase
    {
        public abstract NodeType Type { get; }
        public BehaviorTree Tree;
        public NodeStatus Status { get; private set;}
        public NodeBase Parent;
        public NodeBase[] Childrens;
        public NodeBase()
        {
            SetStatus(NodeStatus.Ready);
        }

        // 设置行为树
        public void SetTree(BehaviorTree tree)
        {
            Tree = tree;
            if (Childrens != null)
            {
                foreach (var node in Childrens)
                {
                    if (node != null) node.SetTree(tree);
                }
            }
        }

        // 设置子节点
        public void SetChildrens(NodeBase[] childrens)
        {
            if(childrens!=null){
                Childrens = childrens;
                foreach (var node in Childrens)
                {
                    if(node != null) node.SetParent(this);
                }
            }
        }
        // 设置父节点
        public void SetParent(NodeBase parent){
            if(Parent!=null) Parent = parent;
        }

        // 节点事件
        public virtual void Visit() {
            if (Status != NodeStatus.Running)
            {
                // 主要防止节点嵌套时，父节点Running状态可能会导致子节点无法通过Reset重置参数
                ResetParameter();
            }
        }

        // 步进 主要用于重置节点状态
        public virtual void Step()
        {
            // 当节点状态不是执行中，则重置状态等待下次执行
            if(Status != NodeStatus.Running)
            {
                Reset();
            }
            // 步进子节点
            if(Childrens!=null)
            {
                foreach (var node in Childrens)
                {
                    if (node != null) node.Step();
                }
            }
        }

        // 重置节点状态 非强制重置状态下，Running不生效
        public virtual void Reset(bool isForce = false)
        {
            if(isForce) SetStatus(NodeStatus.Ready);          // 强制重置为准备状态
            // 当节点状态不是执行中，则重置状态
            if(Status != NodeStatus.Running)
            {
                SetStatus(NodeStatus.Ready);
                ResetParameter();
            }
            // 重置子节点
            if (Childrens != null)
            {
                foreach (var node in Childrens)
                {
                    if (node != null) node.Reset(isForce);
                }
            }
        }

        public void SetStatus(NodeStatus status){
            Status = status;
        }

        /// <summary>
        /// 重置节点临时参数
        /// 每次进入Running之前重置
        /// </summary>
        protected virtual void ResetParameter() { }
    }
}



