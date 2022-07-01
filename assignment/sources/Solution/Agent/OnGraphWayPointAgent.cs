using GXPEngine;
using System;
using System.Collections.Generic;

/**
 * Very simple example of a nodegraphagent that walks directly to the node you clicked on,
 * ignoring walls, connections etc.
 */
class OnGraphWayPointAgent : NodeGraphAgent
{
    //Current target to move towards
    private Node _targetNode=null;
    private Node _currentNode = null;
    private Node _moveToNode = null;
    private Node _previosNode = null;
    private bool _didFindTarget = false;
    public OnGraphWayPointAgent(NodeGraph pNodeGraph) : base(pNodeGraph)
    {

        SetOrigin(width / 2, height / 2);
        //position ourselves on a random node
        if (pNodeGraph.nodes.Count > 0)
        {
            _currentNode = pNodeGraph.nodes[Utils.Random(0, pNodeGraph.nodes.Count)];
            jumpToNode(_currentNode);
        }

        //listen to nodeclicks
        pNodeGraph.OnNodeLeftClicked += onNodeClickHandler;
    }
    //on click; if target is empty
        //true; if the target is connected to the current node
            //true; move to target
            //false; choose random connection
                //if random connection has been visited before
                    //true; if other connection available
                        //true; move there
                        //false; move to the first chosen connection
                    //false; move there
                //END IF
            //END IF
        //false; ignore
    //END
    protected virtual void onNodeClickHandler(Node pNode)
    {
        if (_targetNode == null)
        {
            _didFindTarget = false;
            _targetNode = pNode;
            //if the target is connected to the current then move straight there
            if (_currentNode.connections.Contains(_targetNode))
            {
                Console.WriteLine("Morc has found the target");
                _didFindTarget = true;
            }
            else
            {
            //else move to a random node
                _moveToNode = _currentNode.connections[Utils.Random(0, _currentNode.connections.Count)];
                _previosNode = _currentNode;
            }
        }
    }

    protected override void Update()
    {
        //no target? Don't walk
        if (_targetNode == null) return;

        //Move towards the target node, if we reached it, clear the target
        if (_didFindTarget)
        {
            //move to final target and clear path
            if (moveTowardsNode(_targetNode))
            {                
                _previosNode = _currentNode;
                _currentNode = _targetNode;
                _targetNode = null;
            }
        }else if (moveTowardsNode(_moveToNode))
        {
            _previosNode = _currentNode;
            _currentNode = _moveToNode;
            //if the moveto is connected to the target move towards it
            if (_currentNode.connections.Contains(_targetNode))
            {
                _didFindTarget = true;
                Console.WriteLine("Morc has found the target");
            }
            else
            {
            //look for a node that it didnt just visit unless it has no other choice.
                int rVal = Utils.Random(0, _currentNode.connections.Count);
                do
                {
                    rVal = Utils.Random(0, _currentNode.connections.Count);
                } while (_currentNode.connections[rVal] == _previosNode);
                _moveToNode = _currentNode.connections[rVal];
            }
        }

        
    
    }
}
