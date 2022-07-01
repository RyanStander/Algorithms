using GXPEngine;
using System;
using System.Collections.Generic;

/**
 * Very simple example of a nodegraphagent that walks directly to the node you clicked on,
 * ignoring walls, connections etc.
 */
class PathFindingAgent : NodeGraphAgent
{
    //Current target to move towards
    private List<Node> _target = new List<Node>();
    private Node _current = null;
    private PathFinder pathFinder;

    public PathFindingAgent(NodeGraph pNodeGraph,PathFinder pPathFinder) : base(pNodeGraph)
    {
        pathFinder = pPathFinder;
        SetOrigin(width / 2, height / 2);
        //position ourselves on a random node
        if (pNodeGraph.nodes.Count > 0)
        {
            _current = pNodeGraph.nodes[Utils.Random(0, pNodeGraph.nodes.Count)];
            jumpToNode(_current);
        }

        //listen to nodeclicks
        pNodeGraph.OnNodeLeftClicked += onNodeClickHandler;
    }

    protected virtual void onNodeClickHandler(Node pNode)
    {
        //on click if route is empty 
        //true; use path finder to get shortest route
        //false; add from last item in route the extended list
        if (_target.Count > 0)
        {
            List<Node> tempList = pathFinder.Generate(_target[_target.Count - 1], pNode);
            tempList.Reverse();
            _target.AddRange(tempList);
        }
        else
        {
            List<Node> tempList = pathFinder.Generate(_current, pNode);
            tempList.Reverse();
            _target.AddRange(tempList);
        }
    }

    protected override void Update()
    {
        //no target? Don't walk
        if (_target == null) return;

        //Move towards the target node, if we reached it, clear the target
        if (_target.Count > 0)
        {
            if (moveTowardsNode(_target[0]))
            {
                _current = _target[0];
                _target.RemoveAt(0);
            }
        }
    }
}
