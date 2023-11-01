
using Cysharp.Threading.Tasks;

namespace BehaviorTree.Node
{
    /// <summary>
    /// 节点基类
    /// </summary>
    public abstract class NodeBase
    {
        public virtual int Type { get; set; }
        public Tree Tree;
        public NodeStatus Status { get; private set; }
        public NodeBase Parent;
        public NodeBase[] Childrens;
        public NodeBase()
        {
            SetStatus(NodeStatus.Ready);
        }

        public NodeBase SetType(int type)
        {
            Type = type;
            return this;
        }

        // 设置行为树
        public NodeBase SetTree(Tree tree)
        {
            Tree = tree;
            if (Childrens != null)
            {
                foreach (var node in Childrens)
                {
                    if (node != null) node.SetTree(tree);
                }
            }
            return this;
        }

        // 设置子节点
        public NodeBase SetChildrens(NodeBase[] childrens)
        {
            if (childrens != null)
            {
                Childrens = childrens;
                foreach (var node in Childrens)
                {
                    if (node != null) node.SetParent(this);
                }
            }
            return this;
        }
        // 设置父节点
        public NodeBase SetParent(NodeBase parent)
        {
            if (Parent != null) Parent = parent;
            return this;
        }

        // 节点事件
        public async virtual UniTask Visit()
        {

        }

        // 节点状态修改事件
        protected virtual void OnStatusChange(NodeStatus status)
        {

        }

        // 步进 主要用于重置节点状态
        public virtual void Step()
        {
            // 当节点状态不是执行中，则重置状态等待下次执行
            if (Status != NodeStatus.Running)
            {
                Reset();
            }
            // 步进子节点
            if (Childrens != null)
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
            if (isForce) SetStatus(NodeStatus.Ready);          // 强制重置为准备状态
            // 当节点状态不是执行中，则重置状态
            if (Status != NodeStatus.Running)
            {
                SetStatus(NodeStatus.Ready);
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

        public void SetStatus(NodeStatus status)
        {
            Status = status;
            OnStatusChange(status);
        }
    }
}



