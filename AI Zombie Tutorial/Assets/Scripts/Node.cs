using UnityEngine;
using System.Collections.Generic;

public class Node {

    //store all the adjacents cells to this node
	public List<Node> adjacent = new List<Node>();
	public Node previous = null;
    //this lable is to identify each node by a meaningful name through its position in the grid
	public string label="";
	
	public void Clear()
	{
		previous = null;
	}
}
