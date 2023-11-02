using BehaviorTree.Node;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviorTree
{
    public class Tree
    {
        public NodeBase Root;

        private CancellationTokenSource _cts;

        public bool InRun { get { return _cts != null && !_cts.IsCancellationRequested; } }

        public bool IsPause { get; private set; }

        public Tree(NodeBase root)
        {
            Root = root;
            Root.SetTree(this);
        }
        public async UniTask Run()
        {
            IsPause = false;
            _cts = new CancellationTokenSource();
            try { await OnStepEvent().ToUniTask(PlayerLoopTiming.Update, _cts.Token); } catch { }
        }
        public void Disposal()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }
        public void Play()
        {
            if (IsPause) IsPause = false;
        }

        public void Pause()
        {
            if (!IsPause) IsPause = true;
        }

        public void Reset()
        {
            Root.Reset(true);
        }

        IEnumerator OnStepEvent()
        {
            while (InRun)
            {
                if (IsPause) continue;
                Root.Visit().GetAwaiter().GetResult();
                Root.Step();
                yield return 0;
            }
        }

        public static async UniTask<Tree> Run(NodeBase root)
        {
            var tree = new Tree(root);
            await tree.Run();
            return tree;
        }
    }
}