using UnityEngine;
using System.Collections.Generic;

public class AStarSearch {

    //store a reference to our graph
	private Graph graph;
    //store a list of all reachable node from the start node
	private List<Node> reachable;
    //store a list of all explored node so far in the graph
	private List<Node> explored;
    //store the nodes of our path if we find one
	private List<Node> path;
    //our goal node
	private Node goalNode;
    //how many iterations we did until reach the goal
	private int iterations;
    //has the search finished
    private bool finished;

    //contructor to the search class gets a graph to search into it
    public AStarSearch(Graph graph)
	{
		this.graph = graph;
	}

    //this is our main public method where we start the search and return the final result
    public List<Node> FindPath(Node start, Node goal)
    {
        //this method initialise the search and trigger it
        Start(start, goal);

        //this while loop will perfom one step (iteration) everytime
        while (!finished)
        {
            //do one iteration to explore the current adjacent node
            Step();
        }

       // return the path or an empty list if no path is found
        return path;
    }
    
    //this function return the number of iteration when requested
    public int GetIterations()
    {
        return iterations;
    }

	private void Start(Node start, Node goal)
	{
        //initialise our reachable nodes, and add the source to it
		reachable = new List<Node>();
		reachable.Add(start);
		
        //store a reference to our goal node
		goalNode = goal;
		
        //initialise a list to the explored lists
		explored = new List<Node>();
		path = new List<Node>();
		
        //reset the iterations to 0
        iterations = 0;
		
        //reset all nodes if any search was previously done on the graph
		for(var i=0; i < graph.nodes.Length; i++)
		{
			graph.nodes[i].Clear();
		}
		
	}
	private void Step()
	{
        //if we have already found the path return
		if(path.Count > 0)
		{
			return;
		}
		
        //if there is no more reachable nodes then finish the search and return
		if(reachable.Count == 0)
		{
			finished = true;
			return;
		}
		
        //else increase iterations by one
		iterations ++;
		
        //select one of the adjacent nodes
		var node = ChoseNode ();

        //if it is the goal node then we are done, we need to find our way back to the source
		if(node == goalNode)
		{
            //as long as the previous is not null, only source node has its previous set to null through the path
			while(node != null)
			{
                //insert the current node at the begining of the list
				path.Insert(0,node);
                //go back to the previous node and repeat
				node = node.previous;
			}
            //as we have found the goal finish the search and return
			finished = true;
			return;
		}

        //if we did not reach the goal, remove the node from the reachable nodes as we already explored it
		reachable.Remove(node);
        //add it to the explored node list
		explored.Add(node);
		
        //now for each adjacent node to the current add them to the reachable node list
		for (var i = 0; i < node.adjacent.Count; i++)
		{
			AddAdjacent(node, node.adjacent[i]);
		}
	}

    //this method add a node to the reachable list if the node was not explored yet or if the node is not already in the reachable list
	private void AddAdjacent(Node node, Node adjacent)
	{
		if(FindNode(adjacent, explored) || FindNode(adjacent, reachable))
		{
			return;
		}
        //we need to store from which node we came to this one
		adjacent.previous = node;
        reachable.Add(adjacent);
	}
    //this method return true if the node is in the list or false if not
	private bool FindNode(Node node, List<Node> list)
	{
        return LinearSearch(node, list) >= 0;
	}
	//this is a simple linear search algorithm it retuens the index of the key if found otherwise returns -1
	private int LinearSearch(Node node, List<Node> list)
	{
		for (var i = 0; i < list.Count; i++)
		{
			if(node == list[i])
			{
				return i;
			}
		}
		
		return -1;
	}
    //this method randomly select a node from the reachable node list
	private Node ChoseNode()
	{
		return reachable[Random.Range(0,reachable.Count)];
	}
}
