using GXPEngine;
using System;
using System.Collections.Generic;

/**
 * Very simple example of a nodegraphagent that walks directly to the node you clicked on,
 * ignoring walls, connections etc.
 */
class OffGraphWayPointAgent : NodeGraphAgent
{
    //Current target to move towards
    private List<Node> _target = new List<Node>();
    private Node _current = null;
    public OffGraphWayPointAgent(NodeGraph pNodeGraph) : base(pNodeGraph)
    {

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
    //on click; if the queue list is empty
        //true; if the target is connected to the current node
            //true; add target to queue
            //false; continue
        //false; if target is connectect to the last item in queue
            //true; add node
            //false;continue
        //END
    protected virtual void onNodeClickHandler(Node pNode)
    {
        //if clicked, use path finder to enter
        if (_target.Count > 0)
        {
            if (pNode.connections.Contains(_target[_target.Count - 1]))
                _target.Add(pNode);
        }
        else if (_current.connections.Contains(pNode))
        {
            _target.Add(pNode);
        }



    }

    protected override void Update()
    {
        //no target? Don't walk
        if (_target == null) return;

        //Move towards the target node, if we reached it, clear the first queued target
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
