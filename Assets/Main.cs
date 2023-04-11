using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree.Node;
using BehaviorTree;

public class Main : MonoBehaviour
{
    BehaviorTree.BehaviorTree tree;
    // Start is called before the first frame update
    void Start()
    {
        var node1 = new SequenceNode(new NodeBase[]{
            new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点1"); return true; }),
            new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点2"); return true; }),
            new DelayNode(1f,new DelayNode(2f,new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点3"); return true; }))),
            new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点4"); return true; }),
            new ActionNode(()=>{UnityEngine.Debug.Log("行为树结束");tree.Pause();})
        });

        var node2 = new SelectorNode(new NodeBase[]{
            new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点1"); return false; }),
            new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点2"); return false; }),
            new DelayNode(1f,new DelayNode(2f,new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点3"); return false; }))),
            new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点4"); return false; }),
            new ActionNode(()=>{UnityEngine.Debug.Log("行为树结束");tree.Pause();})
        });

        var node3 = new SequenceNode(new NodeBase[]{
            new IfNode(()=>{return true;},new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点1"); return true; })),
            new IfNode(()=>{return true;},new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点2"); return true; })),
            new ActionNode(()=>{UnityEngine.Debug.Log("行为树结束");tree.Pause();})
        });

        var node4 = new SequenceNode(new NodeBase[]{
            new ParallelSequenceNode(new NodeBase[]{
                new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点1"); return true; }),
                new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点2"); return true; }),
                new DelayNode(1f,new DelayNode(2f,new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点3"); return true; }))),
                new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点4");  return true; }),
            }),
            new ActionNode(()=>{UnityEngine.Debug.Log("行为树结束");tree.Pause();})
        });

        var node5 = new SequenceNode(new NodeBase[]{
            new ParallelSelectorNode(new NodeBase[]{
                new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点1"); return false; }),
                new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点2"); return false; }),
                new DelayNode(1f,new DelayNode(2f,new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点3"); return true; }))),
                new ConditionNode(()=>{ UnityEngine.Debug.Log("行为树节点4");  return false; }),
            }),
            new ActionNode(()=>{UnityEngine.Debug.Log("行为树结束");tree.Pause();})
        });

        var node6 = new RandomNode(new NodeBase[]{
            new DelayNode(1f,new ActionNode(()=>{UnityEngine.Debug.Log("行为树节点1");})),
            new DelayNode(1f,new ActionNode(()=>{UnityEngine.Debug.Log("行为树节点2");})),
            new DelayNode(1f,new ActionNode(()=>{UnityEngine.Debug.Log("行为树节点3");})),
            new DelayNode(1f,new ActionNode(()=>{UnityEngine.Debug.Log("行为树节点4");})),
        },new int[]{10,1,1,10});

        // tree = new BehaviorTree.Tree(node6);
        // tree.Play();
        tree = BehaviorTreeManager.Instance.Create(node5);
    }
}
