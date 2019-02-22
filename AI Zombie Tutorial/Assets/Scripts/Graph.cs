using UnityEngine;
using System.Collections;

public class Graph 
{

    public int rows = 0;
    public int cols = 0;
    public Node[] nodes;

    public Graph(int[,] grid)
    {
        rows = grid.GetLength(0);
        cols = grid.GetLength(1);

        nodes = new Node[grid.Length];
        for (var i = 0; i < nodes.Length; i++)
        {
            var node = new Node();
            node.label = i.ToString();
            nodes[i] = node;
        }

        //this for loop will link the various grid cells together so each node will know its adjacents
        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                var node = nodes[cols * r + c];

                //closed tile do not link it and just continue to the next one
                if (grid[r, c] == 1)
                {
                    continue;
                }

                //up
                if (r > 0)
                {
                    node.adjacent.Add(nodes[cols * (r - 1) + c]);
                }
                //down
                if (r < rows - 1)
                {
                    node.adjacent.Add(nodes[cols * (r + 1) + c]);
                }
                //right
                if (c < cols - 1)
                {
                    node.adjacent.Add(nodes[cols * r + c + 1]);
                }
                //left
                if (c > 0)
                {
                    node.adjacent.Add(nodes[cols * r + c - 1]);
                }
            }
        }
    }
	
}
