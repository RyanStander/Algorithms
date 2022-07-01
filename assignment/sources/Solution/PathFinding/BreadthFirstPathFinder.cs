using GXPEngine;
using System;
using System.Collections.Generic;

class BreadthFirstPathFinder : PathFinder	{
    readonly IDictionary<Node, Node> nodeParents = new Dictionary<Node, Node>();//dictionary is a great way to backtrack the bfs search
    public BreadthFirstPathFinder(NodeGraph pGraph) : base(pGraph){}

    //get start position and end position
        //initialise function requires start node and end node
            //if start node is same as end node
                //true; return null
                //false; continue
            //check connections from start node
            //check if connections have not been explored
                //true; 
                    //add connection node to explored
                    //add previous node as parent of current node
                    //add node to path
                //false; continue
            //if nothing else is found return start node.
    protected override List<Node> generate(Node pFrom, Node pTo)
	{
        nodeParents.Clear();
        List<Node> path = new List<Node>();
        Node endNode = BFSPathfinder(pFrom,pTo);
        //if the start and end node are the same then dont return anything
        if (endNode == pFrom)
        {
            return null;
        }

        Node currentNode = endNode;
        //if the current node is the same as the starting node then add it to the path and create a dictionary pathway of it.
        while (currentNode != pFrom)
        {
            path.Add(currentNode);
            currentNode = nodeParents[currentNode];            
        }
        //if no other things are wrong add the current node to path and return a successful path.
        path.Add(currentNode);
        return path;
    }
    Node BFSPathfinder(Node startNode, Node endNode)
    {
        //---------------------
        //      Setup    
        //---------------------
        List<Node> path = new List<Node>();
        List<Node> exploredNodes = new List<Node>();
        path.Add(startNode);
        //---------------------
        //     Find path     
        //---------------------
        //while the current path is not empty, if it is it returns nothing
        while (path.Count != 0)
        {
            Node currentNode = path[0];
            path.Remove(path[0]);
            //if the current node is the same as the end node return it
            if (currentNode == endNode)
            {
                return currentNode;
            }
            List<Node> nodes = currentNode.connections;

            foreach (Node node in nodes)
            {
                //if the node has not been explored add it to the list, create a dictionary of it and add it to the current path.
                if (!exploredNodes.Contains(node))
                {
                    exploredNodes.Add(node);

                    nodeParents.Add(node, currentNode);

                    path.Add(node);
                }
            }
        }

        return startNode;
    }

}
