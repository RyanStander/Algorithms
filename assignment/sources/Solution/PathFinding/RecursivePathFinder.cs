using GXPEngine;
using System;
using System.Collections.Generic;

class RecursivePathFinder : PathFinder
{

    //private List<NodeGraph> _paths = new List<NodeGraph>();
    List<List<Node>> _paths = new List<List<Node>>();
    public RecursivePathFinder(NodeGraph pGraph) : base(pGraph) { }
    //Main
    //FOREACH connect node to the start node
    //function Search for Path
    //END
    //Find path with smallest node count
    //Function Search for path needs current node, end node and the list of the path its taken to here.
    //Add current node to path
    //If current node same as ending node?
    //>If true, add current path to valid paths list and return to end void
    //FOREACH connection node, 
    //if connection was not explored
    //>If true, start function with new parameters
    //If false, skip over and check next connection in the foreach
    //END

    protected override List<Node> generate(Node pFrom, Node pTo)
    {
        //--------------------------
        //          
        //--------------------------
        _paths.Clear();
        List<Node> _correctPath = new List<Node>();
        //at this point you know the FROM and TO node and you have to write an 
        //algorithm which finds the path between them 
        int i = 0;
        List<Node> tempPath = new List<Node>();
        tempPath.Add(pFrom);
        //--------------------------
        //    Initialise search
        //--------------------------
        foreach (Node node in pFrom.connections)
        {
            searchPath(pFrom, pTo, tempPath);
            i++;
        }
        int smallestCount = int.MaxValue;
        //--------------------------
        //   Choose shortest path    
        //--------------------------
        foreach (List<Node> nodes in _paths)
        {
            //if smallest count is larget than current checked path then select that path
            if (nodes.Count < smallestCount)
            {
                _correctPath = nodes;
                smallestCount = nodes.Count;
            }
            //Console.WriteLine("Checking nodes count " + nodes.Count);
        }
        //Console.WriteLine("current path count "+_paths.Count);
        //Console.WriteLine("correct path count " + (_correctPath.Count-1));
        _correctPath.Reverse();
        return _correctPath;



        //return new List<Node>() { pFrom, pTo };
    }
    void searchPath(Node currentNode, Node endingNode, List<Node> previouslyExplored)
    {
        //--------------------------
        //   Search for paths     
        //--------------------------
        List<Node> temporaryPreviousPath = new List<Node>(previouslyExplored);
        temporaryPreviousPath.Add(currentNode);
        if (currentNode == endingNode)
        {
            _paths.Add(temporaryPreviousPath);
        }
        foreach (Node node in currentNode.connections)
        {
            if (!temporaryPreviousPath.Contains(node))
            {
                searchPath(node, endingNode, temporaryPreviousPath);
            }
        }
    }

}

/*//distance of start node from start node = 0
//let distance of all other nodes from start= infinity

//WHILE nodes remain unvisited
//visit unvisited nodes with smallest known distance from current node
// FOR each unvisited connect of the current node
//Calculate the distance from start node
//If the calculated distance of this node is less than the known distance
//update shortest distance to this node
//update the previous node with the current node
//END IF
//NEXT unvisited neighbour
//Add the current node to the list of visited nodes
//END WHILE

//--------------------------
//          setup
//--------------------------
//nodes that have not been checked
NodeGraph univisited = _graph;
//nodes that have been checked
NodeGraph visited = null;
//the current node we check for shortest path
Node currentNode = pFrom;
//distance of each node
int[] distances = new int[_graph.nodes.Count - 1];
        //sets all distances to largest value posible so that it is assumed they are all extremely far away.
        for (int i = 0; i<distances.Length-1; i++)
        {
            distances[i] = int.MaxValue;
        }

        //this sets the distance of our first node to 0 since it is the start point
        distances[int.Parse(pFrom.id)] = 0;
        

        //------------------------
        //     Recursive search
        //------------------------
        do
        {
            //sets itself to the closes node that has not been explored
            int smallestDistance = int.MaxValue;
            foreach (Node node in univisited.nodes)
            {
                if (distances[int.Parse(node.id)] < smallestDistance)
                {
                    currentNode = node;
                    smallestDistance = distances[int.Parse(node.id)];
                }
            }

            for (int i = 0; i<currentNode.connections.Count - 1; i++)
            {
                if (distances[int.Parse(currentNode.id)] + 1 < distances[int.Parse(currentNode.connections[i].id)])
                    distances[int.Parse(currentNode.connections[i].id)] = distances[int.Parse(currentNode.id)] + 1;

            }
            visited.nodes.Add(currentNode);
            univisited.nodes.Remove(currentNode);
        } while (univisited != null);
        return _correctPath;*/

