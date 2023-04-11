using System;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree.Node;

namespace BehaviorTree
{
    public class BehaviorTreeManager : MonoBehaviour
    {
        public static BehaviorTreeManager Instance { get { if (_instance == null) Create(); return _instance; } }
        private static BehaviorTreeManager _instance;

        List<BehaviorTree> _trees = new List<BehaviorTree>();
        List<BehaviorTree> _removeTrees = new List<BehaviorTree>();
        public static void Create()
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("BehaviorTree");
                _instance = go.AddComponent<BehaviorTreeManager>();
                GameObject.DontDestroyOnLoad(go);
            }
        }

        void Update()
        {
            foreach (var tree in _trees)
            {
                if(!_removeTrees.Contains(tree) && tree.InPlay) tree.Update();
            }
            foreach (var tree in _removeTrees)
            {
                if(_removeTrees.Contains(tree)) _removeTrees.Remove(tree);
            }
            _removeTrees.Clear();
        }

        public BehaviorTree Create(NodeBase root)
        {
            var tree = new BehaviorTree(root);
            return Create(tree);
        }

        public BehaviorTree Create(BehaviorTree tree)
        {
            tree.Play();
            if(!_trees.Contains(tree)) _trees.Add(tree);
            return tree;
        }

        public void Remove(BehaviorTree tree)
        {
            tree.Pause();
            if(_trees.Contains(tree) && !_removeTrees.Contains(tree)) _removeTrees.Add(tree);
        }

    }

}