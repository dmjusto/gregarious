using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
  public static PriorityQueue openList;
  public static HashSet<Node> closedList;


  private static float EstimateCost(Node currentNode, Node goalNode)
  {
    Vector2 vecCost = currentNode.pos - goalNode.pos;
    return vecCost.magnitude;
  }

  public static ArrayList FindPath(Node start, Node goal)
  {
    openList = new PriorityQueue();
    openList.Push(start);
    start.startToNodeCost = 0.0f;
    start.nodeToTargetCost = EstimateCost(start, goal);

    closedList = new HashSet<Node>();
    Node node = null;


    while(openList.Length != 0)
    {
      node = openList.First();
      //check if current node is target node
      if(node.pos == goal.pos)
      {
        return CalculatePath(node);
      }


      //create an arraylist to store the neighbor nodes
      ArrayList neighbors = new ArrayList();

      GridManager.instance.GetNeighbors(node, neighbors);

      for(int i = 0; i <neighbors.Count; i++)
      {
        Node neighborNode = (Node)neighbors[i];

        if (!closedList.Contains(neighborNode))
        {
          float cost = EstimateCost(node, neighborNode);

          float totalCost = node.startToNodeCost + cost;
          float neighborToGoalCost = EstimateCost(neighborNode, goal);

          neighborNode.startToNodeCost = totalCost;
          neighborNode.parent = node;
          neighborNode.nodeToTargetCost = totalCost + neighborToGoalCost;

          if (!openList.Contains(neighborNode))
          {
            openList.Push(neighborNode);
          }
        }
      }

      closedList.Add(node);
      openList.Remove(node);
    }

    if(node.pos != goal.pos)
    {
      Debug.LogError("Goal not found");
      return null;
    }

    return CalculatePath(node);
  }

  private static ArrayList CalculatePath(Node node)
  {
    ArrayList list = new ArrayList();
    while(node != null)
    {
      list.Add(node);
      node = node.parent;
    }

    list.Reverse();
    return list;
  }

}
