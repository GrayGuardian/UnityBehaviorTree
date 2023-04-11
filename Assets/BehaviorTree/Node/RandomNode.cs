namespace BehaviorTree.Node
{
    /// <summary>
    /// 随机节点
    /// 按照权重随机抽取一个子节点执行，并返回子节点状态
    /// </summary>
    public class RandomNode : NodeBase
    {
        public override NodeType Type { get { return NodeType.Random; } }
        private NodeBase _curNode;
        private int _weightsCount;
        private int[] _maxWeights;
        public RandomNode(NodeBase[] childrens,int[] weights = null) : base()
        {
            SetChildrens(childrens);
            // 计算权重信息
            _weightsCount = 0;
            _maxWeights = new int[Childrens.Length];
            NodeBase node;
            int weight;
            for (int i = 0; i < Childrens.Length; i++)
            {
                node = Childrens[i];
                weight = weights == null || weights.Length <= i || weights[i] <= 0 ? 1 : weights[i];
                _weightsCount += weight;
                _maxWeights[i] = _weightsCount;
            }
        }
        public override void Visit()
        {
            base.Visit();
            if(_curNode == null)
            {
                int num = UnityEngine.Random.Range(0, _weightsCount);
                NodeBase node;
                int maxWeight;
                for (int i = 0; i < Childrens.Length; i++)
                {
                    node = Childrens[i];
                    maxWeight = _maxWeights[i];
                    if(num < maxWeight)
                    {
                        _curNode = node;
                        break;
                    }
                }
            }
            if(_curNode != null)
            {
                _curNode.Visit();
                SetStatus(_curNode.Status);
                if(_curNode.Status != NodeStatus.Running)
                    _curNode = null;
                return;
            }

        }
    }

}