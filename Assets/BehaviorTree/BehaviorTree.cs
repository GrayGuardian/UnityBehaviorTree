using BehaviorTree.Node;
namespace BehaviorTree
{
    public class BehaviorTree
    {
        public NodeBase Root;
        public bool InPlay { get { return _inPlay; } }
        private bool _inPlay = false;
        public BehaviorTree(NodeBase root)
        {
            Root = root;
            Root.SetTree(this);
        }
        public void Play()
        {
            _inPlay = true;
        }
        public void Pause()
        {
            _inPlay = false;
        }
        public void Reset(){
            Root.Reset(true);
        }
        public void Update()
        {
            if(!_inPlay) return;
            Root.Visit();
            Root.Step();
        }
    }
}