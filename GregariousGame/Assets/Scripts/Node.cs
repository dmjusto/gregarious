using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{

  public class Node
  {
    public int gridX;
    public int gridY;
    public Vector2 pos;


    public List<Node> neighborNodes = new List<Node>();


    public Node(int _gridX, int _gridY,Vector2 _position)
    {
      gridX = _gridX;
      gridY = _gridY;
      pos = _position;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
  }
}
